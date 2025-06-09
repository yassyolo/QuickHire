import './WizardListItem.css';

interface WizardListItemProps {
  title: string;
  step: number;
  isActive: boolean;
  isValid: boolean;
  onClick: (step: number) => void;
  canClick: boolean;
}

export function WizardListItem({ title, step, isActive, onClick, isValid, canClick }: WizardListItemProps) {
  const shouldShowCheck = isValid && !isActive;

  const handleClick = () => {
    if (canClick) {
      onClick(step);
    }
  };

  return (
    <div
      className={`wizard-list-item-step-title ${isActive ? "active" : ""} ${isValid ? "valid" : "invalid"} ${!canClick ? "disabled" : ""}`}
      onClick={handleClick}
      style={{ cursor: canClick ? "pointer" : "not-allowed" }}
    >
      <div className={`wizard-list-item-step ${isActive ? "active" : ""}`}>
        {shouldShowCheck ? "✔" : step}
      </div>
      <span className="wizard-list-item-title">{title}</span>
    </div>
  );
}

