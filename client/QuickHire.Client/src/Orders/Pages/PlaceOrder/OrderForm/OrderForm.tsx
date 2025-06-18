import { useEffect, useState } from "react";
import { loadStripe } from "@stripe/stripe-js";
import "./OrderForm.css";
import { Elements } from "@stripe/react-stripe-js";
import { PaymentPlan } from "../../../../Gigs/Pages/GigPreview/GigPreview";
import axios from "../../../../axiosInstance";

import { WizardList } from "../../../../Shared/Wizard/WizardList";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { GigRequirementsForm } from "../Steps/GigRequirementsForm";
import ChosenPlan from "../Steps/ChosenPlan";
import { OrderBillingDetailsForm } from "../Steps/OrderBillingDetailsForm";
import StripePaymentForm from "../Steps/StripeCheckout";

const stripePromise = loadStripe(
  "pk_test_51RZ5AwAZgUvFAlsxGKFtC4aULaniL3QSSuxnlxrWX05jNrhWhzbejnRXU87zA5QCXJu9frQ8drWiD2v7OpWAMqh000cEVVq2OX"
);

interface OrderForm {
  gigRequirements: GigRequirement[];
  billingDetails: BillingInfoDetails;
}

export interface GigRequirement {
  id: number;
  question: string;
}

export interface BillingInfoDetails {
  id: number;
  fullName: string;
  companyName: string;
  city: string;
  street: string;
  zipCode: string;
  country: string;
  countryId: number;
}

interface OrderFormProps {
  chosenPlan: PaymentPlan;
  chosenPlanId: number;
  gigId: number;
  onBack: () => void;
}

interface CreateOrderResponse {
  clientSecret: string;
  orderId: number;
}

export function OrderForm({ chosenPlan, chosenPlanId, gigId, onBack }: OrderFormProps) {
  const [orderForm, setOrderForm] = useState<OrderForm | null>(null);
  const [requirementAnswers, setRequirementAnswers] = useState<{ [id: number]: string }>({});
  const [activeStep, setActiveStep] = useState(1);
  const [clientSecret, setClientSecret] = useState<string | null>(null);
  const [orderId, setOrderId] = useState<number | null>(null);

  useEffect(() => {
    const fetchOrderForm = async () => {
      try {
        const params = new URLSearchParams();
        params.append("GigId", gigId.toString());
        const url = `https://localhost:7267/orders/form?${params.toString()}`;
        const response = await axios.get<OrderForm>(url);
        if (response.status === 200) setOrderForm(response.data);
      } catch (error) {
        console.error("Error fetching order form:", error);
      }
    };
    fetchOrderForm();
  }, [gigId, chosenPlanId]);

  const handleOnBillingInfoUpdate = (billingInfo: BillingInfoDetails) => {
    setOrderForm((prev) => (prev ? { ...prev, billingDetails: billingInfo } : prev));
  };

  const handleOnAddBillingInfo = (billingInfo: BillingInfoDetails) => {
    setOrderForm((prev) => (prev ? { ...prev, billingDetails: billingInfo } : prev));
  };

  const onNextStep = () => {
    setActiveStep((prev) => (prev < steps.length ? prev + 1 : prev));
  };

  // Updated: Expect both clientSecret and orderId
  const createOrderAndGetClientSecret = async (): Promise<CreateOrderResponse | null> => {
    if (!orderForm) return null;

    const payload = {
      gigId,
      paymentPlanId: chosenPlanId,
      billingDetailsId: orderForm.billingDetails.id,
      requirements: Object.entries(requirementAnswers).map(([requirementId, answer]) => ({
        requirementId: parseInt(requirementId),
        answer,
      })),
    };

    try {
      const response = await axios.post<CreateOrderResponse>("https://localhost:7267/orders", payload);
      if (response.status === 200 || response.status === 201) {
        return response.data; // { clientSecret, orderId }
      }
    } catch (error) {
      console.error("Order creation failed:", error);
    }
    return null;
  };

  const steps = [
    {
      title: "Chosen plan",
      isValid: true,
      content: (
        <div className="chosen-plan-container">
          <ChosenPlan selectedPlan={chosenPlan} />

          <div className="mt-3" style={{ fontSize: "15px", color: "#555" }}>
            <p style={{ color: "black", fontWeight: "bold" }}>Note:</p>
            <ul>
              <li>
                Tax: <strong>15%</strong> of the subtotal
              </li>
              <li>
                Service Fee: <strong>$5</strong> (fixed)
              </li>
            </ul>
          </div>

          <div className="d-flex flex-row justify-content-between mt-3">
            <ActionButton
              text="Back"
              onClick={onBack}
              className="back-button"
              ariaLabel="On Back button"
            />
            <ActionButton
              text="Next"
              onClick={onNextStep}
              className="add-new-button"
              ariaLabel="Save and continue to next step"
            />
          </div>
        </div>
      ),
    },
    {
      title: "Gig requirements",
      isValid:
        requirementAnswers &&
        Object.keys(requirementAnswers).length === (orderForm?.gigRequirements?.length || 0),
      content: (
        <div className="gig-requirements-container">
          <GigRequirementsForm
            gigRequirements={orderForm?.gigRequirements || []}
            onChange={(answers) => setRequirementAnswers(answers)}
          />
          <ActionButton
            text="Next"
            onClick={onNextStep}
            className="add-new-button"
            ariaLabel="Save and continue to next step"
          />
        </div>
      ),
    },
    {
      title: "Billing details",
      isValid:
        requirementAnswers &&
        Object.keys(requirementAnswers).length === (orderForm?.gigRequirements?.length || 0) &&
        orderForm?.billingDetails?.id !== undefined,
      content: (
        <>
          {orderForm && (
            <div className="billing-details-container">
              <OrderBillingDetailsForm
                billingInfo={orderForm.billingDetails}
                onBillingInfoUpdate={handleOnBillingInfoUpdate}
                onAddBillingInfo={handleOnAddBillingInfo}
              />
              <ActionButton
                text="Proceed to payment"
                onClick={async () => {
                  const paymentData = await createOrderAndGetClientSecret();
                  if (paymentData) {
                    setClientSecret(paymentData.clientSecret);
                    setOrderId(paymentData.orderId);
                    onNextStep();
                  } else {
                    alert("Failed to initialize payment.");
                  }
                }}
                className="proceed-to-payemt-button"
                ariaLabel="Confirm and initialize payment"
              />
            </div>
          )}
        </>
      ),
    },
    {
      title: "Payment",
      isValid: !!clientSecret,
      content: (
        <>
          {clientSecret ? (
            <div className="card-container">
              <Elements stripe={stripePromise} options={{ clientSecret, appearance: {} }}>
                {orderId !== null && <StripePaymentForm orderId={orderId} />}
              </Elements>
            </div>
          ) : (
            <p>Initializing payment...</p>
          )}
        </>
      ),
    },
  ];

  return (
    <div className="order-form-container">
      <WizardList steps={steps} activeStep={activeStep} onStepChange={setActiveStep} />
    </div>
  );
}
