import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton/ActionButton";
import { Modal } from "../../../Common/Modal";
import './DeactivateNotPossibleModal.css';

export interface DeactivateModalNotPossibleProps {
    id: number;
    affectedItems: string;
    gigs?: number;
    onClose: () => void;
    children: React.ReactNode;
}

export function DeactivateModalNotPossible({ gigs, id, affectedItems, onClose, children }: DeactivateModalNotPossibleProps) {
    return (
        <Modal>
            <div aria-label="cannot-deactivate-modal" className="modal-title">Cannot deactivate item with Id: {id}</div>
            {gigs && gigs > 0 && <div className="modal-gigs">Affected Gigs count: {gigs}</div>}
            <div id="modal-info" className="modal-info modal-info-not-possible">List of affected {affectedItems}:</div>
                <div id="modal-body" className="modal-body modal-body-not-possible"> {children} </div>  
                <div id="modal-actions" className="modal-actions modal-actions-not-possible">
                    <ActionButton onClick={onClose} className="back-button" text={"Back"} ariaLabel={"Back Button"}/>
                </div>
        </Modal>
    );
}