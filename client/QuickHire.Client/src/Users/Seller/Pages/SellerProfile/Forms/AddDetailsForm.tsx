import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import './AddDetailsForm.css';

export interface AddDetailsModalProps {
    children: React.ReactNode;
    onSave: () => void;
}

export function AddDetailsModal({ children, onSave }: AddDetailsModalProps) {
    return (
        <div className="add-details-modal">
            <div className="add-details-modal-content">{children}</div>
            <div className="add-button-wrapper">
                <ActionButton text={"+ Add"} onClick={onSave} className={"add-details-button"} ariaLabel={"Add Details Button"}></ActionButton>
            </div>
        </div>
    );
}