import React from "react";
import "./WizardList.css";
import { WizardListItem } from "./WizardListItem";

interface WizardStep {
  title: string;
  content: React.ReactNode;
  isValid: boolean;
}

interface WizardListProps {
  steps: WizardStep[];
  activeStep: number;
  onStepChange: (step: number) => void;
}

export function WizardList({ steps, activeStep, onStepChange}: WizardListProps) {
  return (
    <div className="wizard-container">
      <div className="wizard-header" style={{ display: "flex", alignItems: "center", gap: "8px" }}>
        {steps.map((step, index) => {
          const canClick =
            index <= activeStep - 1 || 
            steps.slice(0, index).every((s) => s.isValid); 

          return (
            <div key={index} style={{ display: "flex", alignItems: "center" }}>
              <WizardListItem
                isValid={step.isValid}
                title={step.title}
                step={index + 1}
                isActive={index + 1 === activeStep}
                onClick={onStepChange}
                canClick={canClick}
              />
              {index < steps.length - 1 && (
                <span className="chevron">{'>'}</span>
              )}
            </div>
          );
        })}
      </div>

      <div className="wizard-content">
        {steps[activeStep - 1]?.content}
      </div>
    </div>
  );
}
