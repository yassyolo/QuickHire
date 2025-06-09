import './Checkbox.css';

interface SelectableCheckboxProps<TId extends string | number> {
  id: TId;
  label: string;
  isSelected: boolean;
  description?: string;
  onChange: (id: TId) => void;
}

export function Checkbox<TId extends string | number>({
  id,
  label,
  isSelected,
  onChange,
  description,
}: SelectableCheckboxProps<TId>) {
  const handleCheckboxChange = () => onChange(id);

  const handleKeyDown = (event: React.KeyboardEvent<HTMLInputElement>) => {
    if (event.key === 'Enter' || event.key === ' ') {
      event.preventDefault();
      handleCheckboxChange();
    }
  };

  return (
    <div className="checkbox-container" aria-label="checkbox-container">
      <label htmlFor={label} className="checkbox-label">
        <input
          type="checkbox"
          value={id.toString()}
          checked={isSelected}
          onKeyDown={handleKeyDown}
          onChange={handleCheckboxChange}
        />
        {label}
        {description && <span className="checkbox-description">{description}</span>}
      </label>
    </div>
  );
}
