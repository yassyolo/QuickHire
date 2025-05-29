import React from "react";
import './AddModal.css';
import { Modal } from "../../Common/Modal";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton";
import { ModalActions } from "../../Common/ModalActions";

export interface AddModalProps {
    title: string;
    onClose: () => void;
    onContinue : () => void;
    children: React.ReactNode;
}

export function AddModal({ title, onClose, onContinue, children }: AddModalProps) {
    return (
        <Modal>
            <div aria-label="Add modal" className="modal-title">Add {title}</div>
            <div id="modal-body" className="modal-body">{children}</div>
            <ModalActions id={"add-main-category-actions"}>
                <ActionButton text="Back" onClick={onClose} className="back-button" ariaLabel="Back Button"/>
                <ActionButton text="Continue" onClick={onContinue} className="continue-button" ariaLabel="Continue Button"/>
            </ModalActions>           
        </Modal>
    );
}


