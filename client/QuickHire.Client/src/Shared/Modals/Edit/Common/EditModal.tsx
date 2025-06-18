import { ActionButton } from '../../../Buttons/ActionButton/ActionButton';
import './EditModal.css'; 
import { Modal } from '../../Common/Modal';
import { ModalActions } from '../../Common/ModalActions';

export interface EditModalProps{
    onClose: () => void;
    onContinue: () => void;
    children: React.ReactNode;
    id: number;
    showId?: boolean;
}

export function EditModal({onClose, onContinue, id, children, showId } : EditModalProps) {
    return (
        <Modal>
            {showId ? (
                <div aria-label="Edit modal" className="modal-title"> Are you sure you want to edit item </div>
            ) : (
                <div aria-label="Edit modal" className="modal-title"> Are you sure you want to edit item with Id: {id} </div>
            )}
                <div id="modal-info" className="modal-info">You're about to update this item. Please review the changes before continuing. This action cannot be undone.</div>
                <div id="modal-body" className="modal-body"> {children}</div>
                <ModalActions id={"edit-main-category-actions"}>
                     <ActionButton text="Back" onClick={onClose} className={"back-button"} ariaLabel={"Back Button"} />
                  <ActionButton text="Continue" onClick={onContinue} className={"continue-button"} ariaLabel={"Continue Button"} />
                </ModalActions>                 
        </Modal>

    );
}