import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton";

interface DetailsModalButtonsProps {
    onSave: () => void;
    onClear: () => void;
}

export function DetailsModalButtons({onSave, onClear}: DetailsModalButtonsProps) {
    return (
        <div className="details-modal-buttons">
            <ActionButton text={"Clear"} onClick={onClear} className={"details-modal-save-button"} ariaLabel={"Clear details modal button"}     />
            <ActionButton text={"Save"} onClick={onSave} className={"details-modal-save-button"} ariaLabel={"Save details modal button"}     />
        </div>
    );
}
