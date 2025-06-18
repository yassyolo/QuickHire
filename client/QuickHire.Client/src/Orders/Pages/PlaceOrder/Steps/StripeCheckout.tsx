import { useStripe, useElements, PaymentElement } from "@stripe/react-stripe-js";
import { useState } from "react";
import axios from "../../../../axiosInstance";
import { useNavigate } from "react-router-dom";

interface StripePaymentFormProps {
  orderId: number;
}

export default function StripePaymentForm({ orderId }: StripePaymentFormProps) {
  const stripe = useStripe();
  const elements = useElements();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const Navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!stripe || !elements) return;

    setLoading(true);
    setError(null);

    try {
      const { error: stripeError, paymentIntent } = await stripe.confirmPayment({
        elements,
        confirmParams: {
          payment_method_data: {
          },
          return_url: window.location.href,
        },
        redirect: "if_required",
      });

      if (stripeError) {
        setError(stripeError.message ?? "Payment failed");
        setLoading(false);
        return;
      }

      if (!paymentIntent) {
        setError("PaymentIntent not found after confirmation.");
        setLoading(false);
        return;
      }

      if (paymentIntent.status === "succeeded") {
        const paymentIntentOrderId =  orderId;

        if (!paymentIntentOrderId) {
          setError("Order ID metadata missing from payment intent.");
          setLoading(false);
          return;
        }

        await axios.post(
          "https://localhost:7267/mark-paid",
          { orderId: paymentIntentOrderId },
          { headers: { "Content-Type": "application/json" } }
        );
        Navigate(`/buyer/orders`);

      } else {
        setError(`Payment status: ${paymentIntent.status}`);
      }
    } catch (backendError) {
      console.error(backendError);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <PaymentElement />
      <button type="submit" disabled={!stripe || loading} style={{ marginTop: "20px", padding: "10px 20px", backgroundColor: "black", color: "#fff", border: "none", borderRadius: "5px !important" }}>
        {loading ? "Processing..." : "Pay now"}
      </button>
      {error && <div style={{ color: "red" }}>{error}</div>}
    </form>
  );
}
