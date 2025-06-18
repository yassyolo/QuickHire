import { useState } from "react";
import "./PaymentPlansCard.css";
import { PaymentPlan } from "../../../Pages/GigPreview/GigPreview";
import { OrderForm } from "../../../../Orders/Pages/PlaceOrder/OrderForm/OrderForm";
interface PaymentPlansCardProps {
  plans: PaymentPlan[];
  gigId: number;
}

export default function PaymentPlansCard({ plans, gigId }: PaymentPlansCardProps) {
  const [selectedPlan, setSelectedPlan] = useState<PaymentPlan>(plans[0]);
  const [showWatsIncluded, setShowWhatsIncluded] = useState(false);
  const [showOrderForm, setShowOrderForm] = useState(false);
  const handleContinue = () => {
    setShowOrderForm(true);
  }

  const toggleWhatsIncluded = () => {
    setShowWhatsIncluded(!showWatsIncluded);
  };

  return (
    <div className="payment-plans-card">
      <div className="payment-plans-header">
  {plans.map((plan) => (
    <div
      key={plan.id}
      onClick={() => setSelectedPlan(plan)}
      className={`plan-choice ${selectedPlan?.id === plan.id ? "active" : ""}`}
    >
      {plan.name}
    </div>
  ))}
</div>

      <div className="plan-details">
        <div className="plan-details-price">${selectedPlan?.price}</div>
        <div>{selectedPlan?.description}</div>
        <div className="d-flex flex-row justify-content-between">
          <div className="plan-details-info"><i className="bi bi-clock-history"></i>{selectedPlan?.deliveryTimeInDays}-day delivery</div>
        <div className="plan-details-info"><i className="bi bi-arrow-repeat"></i>{selectedPlan?.revisions} revisions</div>
        </div>
        

        <div>
          <div className="whats-included-container" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }} onClick={toggleWhatsIncluded}>
            <div className="whats-included">What's Included:</div>
            {showWatsIncluded ? (
              <i className="bi bi-chevron-up"></i>) : (
              <i className="bi bi-chevron-down"></i>)}
          </div>
          
          {showWatsIncluded && <ul className="whats-included-list">
            {selectedPlan.inclusions.map((include) => (
              <li key={include.name} className="whats-included-item">
                <span className="whats-included-icon">
                  {include.value === "true" ? (
                    <i className="bi bi-check check-icon included"></i>
                  ) : (
                    <i className="bi bi-check check-icon" style={{color:'#95979D'}}></i>
                  )}
                </span>{include.name}
              </li>
            ))}
          </ul>}
          <button className="order-now-button" onClick={handleContinue}>Continue</button>

          {showOrderForm && <OrderForm chosenPlan={selectedPlan} chosenPlanId={selectedPlan.id} gigId={gigId}/>}
        </div>
      </div>
    </div>
  );
}
