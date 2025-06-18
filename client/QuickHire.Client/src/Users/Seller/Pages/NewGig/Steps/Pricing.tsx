import { useState } from "react";
import { PaymentPlan } from "../../../../../Gigs/Pages/GigPreview/GigPreview";
import { CreatePackagePlansForm } from "../../../../../Gigs/Common/PaymentPlans/CreatePaymentPlan/CreatePaymentPlanForm";

export function PricingStep({
  onNextStep,
  onPlansChange,
}: {
  onNextStep: () => void;
  onPlansChange: (plans: PaymentPlan[]) => void;
}) {
  const [validationErrors, setValidationErrors] = useState<string | null>(null);

  const handlePlansSubmit = (newPlans: PaymentPlan[]) => {
    const hasInvalidPlan = newPlans.some(
      (p) => !p.name.trim() || p.price <= 0 || p.deliveryTimeInDays <= 0
    );
    if (hasInvalidPlan) {
      setValidationErrors(
        "Please complete all required fields and ensure prices and delivery days are greater than 0."
      );
      return;
    }

    setValidationErrors(null);
    onPlansChange(newPlans);
    onNextStep();
  };

  return (
    <div>
      {validationErrors && (
        <div className="text-red-500 mb-2">{validationErrors}</div>
      )}

      <CreatePackagePlansForm onSubmit={handlePlansSubmit} />
    </div>
  );
}
