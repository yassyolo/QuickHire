import { useState } from "react";
import { ActionButton } from "../../../Shared/Buttons/ActionButton";
import './CategoriesActionButton.css';

interface CategoriesActionButtonProps {
    onSeeButtonClick: () => void;
    onEditModalVisibility?: () => void;
    onDeactivateModalVisibility: () => void;
}

export function CategoriesActionButton({ onSeeButtonClick, onEditModalVisibility, onDeactivateModalVisibility}: CategoriesActionButtonProps) {
    const [showActionsDropdown, setShowActionsDropdown] = useState(false);

    const handleActionsDropdownVisibility = () => setShowActionsDropdown(!showActionsDropdown);

    return (
        <div className="actions-container">
            <ActionButton text={<i className="bi bi-caret-down-fill"></i>} onClick={handleActionsDropdownVisibility} className={"actions-dropdown-visibility-button"}ariaLabel={"ActionsDropdownVisibility"}/>
            {showActionsDropdown && (
                <div className="actions-dropdown">
                    <div className="actions-dropdown-item" onClick={onSeeButtonClick}>Preview</div>
                    {onEditModalVisibility && (
                        <div className="actions-dropdown-item" onClick={onEditModalVisibility}>Edit</div>
                    )}
                    <div className="actions-dropdown-item" onClick={onDeactivateModalVisibility}>Deactivate</div>
                </div>
            )}
        </div>
    );
}
