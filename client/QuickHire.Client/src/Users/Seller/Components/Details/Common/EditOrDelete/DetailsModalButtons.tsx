import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton";

interface DetailsModalButtonsProps {
    onSave: () => void;
    onClear: () => void;
}

export function DetailsModalButtons({onSave, onClear}: DetailsModalButtonsProps) {
    return (
        <div className="details-modal-buttons justify-content-between d-flex flex-row">
            <ActionButton text={"Clear"} onClick={onClear} className={"details-modal-clear-button"} ariaLabel={"Clear details modal button"}     />
            <ActionButton text={"Save"} onClick={onSave} className={"details-modal-save-button"} ariaLabel={"Save details modal button"}     />
        </div>
    );
}
