import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton/ActionButton";
import './DetailsModalButtons.css';

interface DetailsModalButtonsProps {
    onSave: () => void;
    onClear: () => void;
}

export function DetailsModalButtons({onSave, onClear}: DetailsModalButtonsProps) {
    return (
        <div className="details-modal-buttons">
            <ActionButton text={"Clear"} onClick={onClear} className={"details-modal-clear-button"} ariaLabel={"Clear details modal button"}     />
            <ActionButton text={"Save"} onClick={onSave} className={"details-modal-save-button"} ariaLabel={"Save details modal button"}     />
        </div>
    );
}
