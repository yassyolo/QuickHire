import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton";
import './AddDetailsForm.css';

export interface AddDetailsModalProps {
    children: React.ReactNode;
    onSave: () => void;
}

export function AddDetailsModal({ children, onSave }: AddDetailsModalProps) {
    return (
        <div className="add-details-modal">
            <div className="add-details-modal-content">{children}</div>
            <ActionButton text={"+ Add"} onClick={onSave} className={"add-details-button"} ariaLabel={"Add Details Button"}></ActionButton>
        </div>
    );
}