import { useState } from "react";
import "./NewAddedItemsActions.css";

interface NewAddedItemsActionsProps {
    onEdit: () => void;
    onRemove: () => void;
}

export function NewAddedItemsActions({ onEdit, onRemove }: NewAddedItemsActionsProps) {
    const [isDropdownVisible, setIsDropdownVisible] = useState(false);
    const handleDropdownVisibility = () => {
        setIsDropdownVisible(!isDropdownVisible);
    }
    return (
       <div className="new-added-items-actions-container">
          <div className="new-added-items-actions-dots" onClick={handleDropdownVisibility}>...</div>
            {isDropdownVisible && (
                <div className="new-added-items-actions-dropdown">
                    <div className="new-added-items-action-item" onClick={onEdit}>Edit</div>
                    <div className="new-added-items-action-item" onClick={onRemove}>Remove</div>
                </div>
            )}
       </div>
    );
}