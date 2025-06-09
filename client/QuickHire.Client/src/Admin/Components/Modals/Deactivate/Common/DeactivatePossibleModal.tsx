import './DeactivatePossibleModal.css';
import { ActionButton } from '../../../../../Shared/Buttons/ActionButton/ActionButton';
import { Modal } from '../../Common/Modal';
import { FormGroup } from '../../../../../Shared/Forms/FormGroup';
import { ChangeEvent, useCallback } from 'react';
import { ModalActions } from '../../Common/ModalActions';
import { useTooltip } from '../../../../../Shared/Tooltip/Tooltip';

export interface DeactivateModalProps {   
    id: number | string;
    reason: string;
    onClose: () => void;
    onDeactivateSuccess: (id?: number) => void;
    setReason: (reason: string) => void; 
    error: string[];
}

export function DeactivateModal({id, onClose, onDeactivateSuccess, setReason, reason, error } : DeactivateModalProps) {
    const [showReasonTooltip, handleShowReasonTooltip] = useTooltip();

    const handleReasonChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {setReason(event.target.value)}, [setReason]);

    return (
      <Modal>
            <div aria-label="deactivate-modal" className="modal-title">Are you sure you want to deactivate item with Id: {id}</div>
            <div id="modal-info" className="modal-info">This action is permanent and cannot be undone. Please provide a reason for deactivation.</div>
            <div id="modal-body" className="modal-body">
                <FormGroup error={error} id={"deactivation-reason"} label={"Reason"} tooltipDescription={"Briefly explain why this item is being deactivated (e.g., duplicated, reported or regulated guidelines)."} type={"text"} value={reason} onChange={handleReasonChange} placeholder={"Enter Reason"} ariaDescribedBy={"reason-help"} onShowTooltip={handleShowReasonTooltip} showTooltip={showReasonTooltip}></FormGroup>           
            </div>
            <ModalActions id={"deactivate-main-category-actions"}>
                <ActionButton text={"Back"} onClick={onClose} className={"back-button"} ariaLabel={"Back Button"} />
                <ActionButton text={"Continue"} onClick={onDeactivateSuccess} className={"continue-button"} ariaLabel={"Continue Button"} />
            </ModalActions>               
      </Modal>
    )
}