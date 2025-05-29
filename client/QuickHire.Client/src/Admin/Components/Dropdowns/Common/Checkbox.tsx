import './Checkbox.css';

interface SelectableCheckboxProps {
  id: number;
  label: string;
  isSelected: boolean;
  description?: string;
  onChange: (id: number) => void;
}

export function Checkbox({id, label, isSelected, onChange, description}: SelectableCheckboxProps) {
  const handleCheckboxChange = () => onChange(id);

  const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if(event.key === 'Enter' || event.key === ' ') {
      event.preventDefault();
      handleCheckboxChange();
    }
  }

  return (
    <div className="checkbox-container" aria-label="checkbox-container">
      <label htmlFor={label} className="checkbox-label">
        <input type="checkbox" value={id} checked={isSelected} onKeyDown={handleKeyDown} onChange={handleCheckboxChange} /> {label} {description && (<span className="checkbox-description">{description}</span>)}
      </label>
    </div>
  );
}
 