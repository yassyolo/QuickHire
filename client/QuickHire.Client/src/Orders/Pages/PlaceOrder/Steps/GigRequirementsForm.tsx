import { useState } from "react";
import { FormGroup } from "../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { GigRequirement } from "../OrderForm/OrderForm";

interface GigRequirementsFormProps {
  gigRequirements: GigRequirement[];
  onChange: (answers: { [requirementId: number]: string }) => void;
}

export function GigRequirementsForm({ gigRequirements, onChange }: GigRequirementsFormProps) {
  const [answers, setAnswers] = useState<{ [requirementId: number]: string }>({});
  const [showTooltip, setShowTooltip] = useTooltip();

  const handleInputChange = (requirementId: number, value: string) => {
    const updatedAnswers = { ...answers, [requirementId]: value };
    setAnswers(updatedAnswers);
    onChange(updatedAnswers); 
  };

  return (
    <div className="gig-requirements-form">
      {gigRequirements.map((requirement) => (
        <FormGroup
          key={requirement.id}
          id={`gig-requirement-${requirement.id}`}
          label={requirement.question}
          tooltipDescription={"Provide a clear answer to this requirement."}
          showTooltip={showTooltip}
          onShowTooltip={setShowTooltip}
          type="text"
          value={answers[requirement.id] || ""}
          onChange={(e) => handleInputChange(requirement.id, e.target.value)}
          placeholder={"Enter your answer..."}
          ariaDescribedBy={`gig-requirement-${requirement.id}-help`}
        />
      ))}
    </div>
  );
}
