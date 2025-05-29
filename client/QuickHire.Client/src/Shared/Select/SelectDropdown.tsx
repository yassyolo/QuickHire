import React, { useCallback } from 'react';
import './SelectDropdown.css';
import { FormLabel } from '../Forms/FormLabel';
interface SelectDropdownProps<T> {
    id: string;
    label: string;
    options: T[];
    value: number | undefined;
    onChange: (value: number | undefined) => void;
    getOptionLabel: (option: T) => string;
    getOptionValue: (option: T) => number | undefined;
    tooltipDescription: string;
    showTooltip: boolean;
    ariaDescribedBy: string;
    onShowTooltip: () => void;
}

export function SelectDropdown<T>({ onShowTooltip, ariaDescribedBy, tooltipDescription, showTooltip, id, label, options, value, onChange, getOptionLabel, getOptionValue}: SelectDropdownProps<T>) {
    const handleSelectChange = useCallback((event: React.ChangeEvent<HTMLSelectElement>) => {onChange(Number(event.target.value));}, [onChange]);

    return (
        <div className="form-group">
            <FormLabel  id={id} label={label} tooltipDescription={tooltipDescription} onShowTooltip={onShowTooltip} showTooltip={showTooltip} ariaDescribedBy={ariaDescribedBy} />           
            <select id={id} className="form-select" value={value} onChange={handleSelectChange} aria-label={label} >
                {options.map((option) => {
                    const val = getOptionValue(option);
                    const label = getOptionLabel(option);
                    return (
                        <option key={val} value={val}>{label}</option>
                    );
                })}
            </select>
        </div>
    );
}
