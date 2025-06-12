import { CardElement, useStripe, useElements } from "@stripe/react-stripe-js";
import axios from "../axiosInstance"; // your configured Axios instance

export default function CheckoutForm() {
    const stripe = useStripe();
    const elements = useElements();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const { data: clientSecret } = await axios.post("/payment/create-payment-intent", {
            amount: 5000 // e.g., 50.00 USD = 5000 cents
        });

        if (!stripe || !elements) {
            console.error("Stripe.js has not loaded yet.");
            return;
        }

        const cardElement = elements.getElement(CardElement);
        if (!cardElement) {
            console.error("CardElement not found.");
            return;
        }

        const result = await stripe.confirmCardPayment(clientSecret, {
            payment_method: {
                card: cardElement
            }
        });

        if (result?.paymentIntent?.status === "succeeded") {
            console.log("Payment successful!");
        } else {
            console.error(result?.error?.message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <CardElement />
            <button type="submit" disabled={!stripe}>Pay</button>
        </form>
    );
}
