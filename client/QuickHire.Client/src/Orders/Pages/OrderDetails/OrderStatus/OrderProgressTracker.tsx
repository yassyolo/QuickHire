import "./OrderProgressTracker.css";

export interface OrderStatusStep {
  name: string;
  status: string;
  isCompleted: boolean;
}

interface OrderStatusProgressProps {
  steps: OrderStatusStep[];
}



export function OrderProgressTracker({ steps}: OrderStatusProgressProps) {
  return (
    <div className="order-progress-tracker">
      {steps.map((step, index) => (
        <div key={step.status} className={`step ${step.isCompleted ? "completed" : ""}`}>
          <div className="circle">{index + 1}</div>
          <span className="label">{step.name}</span>
          {index !== steps.length - 1 && <div className="line" />}
        </div>
      ))}
    </div>
  );
}
