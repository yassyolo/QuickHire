import { PaymentPlan } from '../../../Pages/GigPreview/GigPreview';
import './ComparePackagesTable.css';

interface Props {
  plans: PaymentPlan[];
}

export function ComparePackagesTable({ plans }: Props) {
  const allFeatures = [...new Set(plans.flatMap(x => x.inclusions.map(i => i.name)))];

  const getDisplayValue = (plan: PaymentPlan, feature: string) => {
    const value = plan.inclusions.find(x => x.name === feature)?.value.toLowerCase();
    if (value === "true") return <i className="bi bi-check-lg check-icon included"></i>;
    if (value === "false") return <i className="bi bi-check-lg check-icon excluded"></i>;
    return value ?? "";
  };

  return (
    <table className="compare-table">
      <thead>
        <tr className="header-row">
          <th className="package-title package">Package</th>
          {plans.map(p => (
            <th key={p.id} className="package-cell">
              <div className="package-price">€{p.price.toFixed(2)}</div>
              <div className="package-name">{p.name}</div>
              <div className="package-description">{p.description}</div>
            </th>
          ))}
        </tr>
      </thead>
      <tbody>
        {allFeatures.map(feature => (
          <tr key={feature} className="feature-row">
            <td className="feature-name">{feature}</td>
            {plans.map(plan => (
              <td key={plan.id} className="feature-value">{getDisplayValue(plan, feature)}</td>
            ))}
          </tr>
        ))}
        <tr className="feature-row">
          <td className="feature-name package" style={{borderTop: "1px solid #ddd"}}><strong>Revisions</strong></td>
          {plans.map(p => (
            <td key={p.id} className="feature-value" style={{borderTop: "1px solid #ddd"}}>
              {p.revisions === -1 ? "Unlimited" : p.revisions}
            </td>
          ))}
        </tr>
        <tr className="feature-row">
          <td className="feature-name package"><strong>Delivery Time</strong></td>
          {plans.map(p => (
            <td key={p.id} className="feature-value">
              {p.deliveryTimeInDays} days
            </td>
          ))}
        </tr>
      </tbody>
    </table>
  );
}
