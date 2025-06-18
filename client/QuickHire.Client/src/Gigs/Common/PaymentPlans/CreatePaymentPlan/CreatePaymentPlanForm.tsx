import { useState } from "react";
import { PaymentPlan } from "../../../Pages/GigPreview/GigPreview";
import "./CreatePackagePlansForm.css";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";

interface CreatePackagePlansFormProps {
  onSubmit: (plans: PaymentPlan[]) => void;
}

export function CreatePackagePlansForm({onSubmit}: CreatePackagePlansFormProps
) {
  const [plans, setPlans] = useState<PaymentPlan[]>([
    { id: 1, name: "", price: 0, description: "", deliveryTimeInDays: 1, revisions: 0, inclusions: [] },
    { id: 2, name: "", price: 0, description: "", deliveryTimeInDays: 1, revisions: 0, inclusions: [] },
    { id: 3, name: "", price: 0, description: "", deliveryTimeInDays: 1, revisions: 0, inclusions: [] },
  ]);
  const [features, setFeatures] = useState<string[]>([]);
  const [newFeature, setNewFeature] = useState("");

  const updatePlan = (id: number, field: keyof PaymentPlan, value: unknown) => {
    setPlans(prev =>
      prev.map(p =>
        p.id === id ? { ...p, [field]: value } : p
      )
    );
  };

  const toggleFeature = (planId: number, feature: string, checked: boolean) => {
    setPlans(prev =>
      prev.map(p => {
        if (p.id !== planId) return p;
        const otherInclusions = p.inclusions.filter(i => i.name !== feature);
        return {
          ...p,
          inclusions: [...otherInclusions, { name: feature, value: checked ? "true" : "false" }],
        };
      })
    );
  };

  const addFeature = () => {
    if (!newFeature.trim()) return;
    const cleaned = newFeature.trim();
    if (features.includes(cleaned)) return;
    setFeatures([...features, cleaned]);
    setNewFeature("");
  };

  const handleSubmit = () => {
    onSubmit(plans);
  };

  return (
    <div style={{ overflowX: "auto" }}>
      <table className="compare-table">
        <thead>
          <tr className="header-row">
            <th>Field</th>
            {plans.map(p => (
              <th key={p.id}>Plan {p.id}</th>
            ))}
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>Name</td>
            {plans.map(p => (
              <td key={p.id}>
                <input
                  type="text"
                  value={p.name}
                  onChange={e => updatePlan(p.id, "name", e.target.value)}
                    className="delivery-time-input"

                />
              </td>
            ))}
          </tr>
          <tr>
            <td>Price (€)</td>
            {plans.map(p => (
              <td key={p.id}>
                <input
                  type="number"
                  value={p.price}
                  onChange={e => updatePlan(p.id, "price", parseFloat(e.target.value))}
                    className="delivery-time-input"

                />
              </td>
            ))}
          </tr>
          <tr>
            <td>Description</td>
            {plans.map(p => (
              <td key={p.id}>
                <textarea
                  value={p.description}
                  onChange={e => updatePlan(p.id, "description", e.target.value)}
                    className="delivery-time-input"

                />
              </td>
            ))}
          </tr>
          <tr>
            <td>Revisions</td>
            {plans.map(p => (
              <td key={p.id}>
                <input
                  type="number"
                  value={p.revisions}
                  onChange={e => updatePlan(p.id, "revisions", parseInt(e.target.value))}
                    className="delivery-time-input"

                />
              </td>
            ))}
          </tr>
          <tr>
            <td>Delivery Days</td>
            {plans.map(p => (
              <td key={p.id}>
                <input
                  type="number"
                  value={p.deliveryTimeInDays}
                  onChange={e => updatePlan(p.id, "deliveryTimeInDays", parseInt(e.target.value))}
                    className="delivery-time-input"

                />
              </td>
            ))}
          </tr>

          {features.map(feature => (
            <tr key={feature}>
              <td>{feature}</td>
              {plans.map(plan => {
                const checked = plan.inclusions.find(i => i.name === feature)?.value === "true";
                return (
                  <td key={plan.id}>
                    <input
                      type="checkbox"
                      checked={checked}
                      onChange={e => toggleFeature(plan.id, feature, e.target.checked)}
                        className="delivery-time-input"

                    />
                  </td>
                );
              })}
            </tr>
          ))}

          <tr>
            <td>
              <input
                type="text"
                placeholder="New feature"
                value={newFeature}
                onChange={e => setNewFeature(e.target.value)}
              />
            </td>
            <td colSpan={plans.length}>
              <button onClick={addFeature}>Add Feature</button>
            </td>
          </tr>
        </tbody>
      </table>
<div className="save-wrapper" style={{marginTop: '20px'}}><ActionButton
                  text="Save and continue"
                  onClick={handleSubmit}
                  className="save-and-continue-button"
                  ariaLabel="Save personal info and go to next step" /></div>

     
    </div>
  );
}
