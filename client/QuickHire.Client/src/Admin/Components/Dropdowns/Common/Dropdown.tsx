import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import './Dropdown.css';

interface DropdownProps {
    onClearAll: () => void;
    onApply: () => void;
    children: React.ReactNode;
}

export function Dropdown({ onClearAll, onApply, children }: DropdownProps) {
    return(
        <div className="filter-container">
            <div className="filter-contents">{children}</div>
            <div className="filter-actions">
                <ActionButton text="Clear all" onClick={onClearAll} className="clear-all-button" ariaLabel="Clear All Button"/>
                <ActionButton text="Apply" onClick={onApply} className="apply-button" ariaLabel="Apply Filters Button"/>
            </div>
        </div>
    );
}