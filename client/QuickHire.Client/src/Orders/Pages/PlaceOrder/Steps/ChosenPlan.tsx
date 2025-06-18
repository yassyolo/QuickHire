import "../../../../Gigs/Common/PaymentPlans/ChoosePaymentPlan/PaymentPlansCard.css";
import { PaymentPlan } from "../../../../Gigs/Pages/GigPreview/GigPreview";

interface ChosenPlanProps {
  selectedPlan: PaymentPlan;
}

export default function ChosenPlan({ selectedPlan }: ChosenPlanProps) {
  

  return (
    <div className="payment-plans-card">
    
      <div className="plan-details">
        <div className="plan-details-price">${selectedPlan?.price}</div>
        <div>{selectedPlan?.description}</div>
        <div className="d-flex flex-row justify-content-between">
          <div className="plan-details-info"><i className="bi bi-clock-history"></i>{selectedPlan?.deliveryTimeInDays}-day delivery</div>
        <div className="plan-details-info"><i className="bi bi-arrow-repeat"></i>{selectedPlan?.revisions} revisions</div>
        </div>
        

        <div>
          <div className="whats-included-container" style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
            <div className="whats-included">What's Included:</div>
            
          </div>
          
           <ul className="whats-included-list">
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
          </ul>
        </div>
      </div>
    </div>
  );
}
