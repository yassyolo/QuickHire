import React from "react";
import { IconButton } from "../../../../../../Shared/Buttons/IconButton";
import './AddOrEditDetailsModal.css';

export interface DetailsModalProps {
    title: string;
    children: React.ReactNode;
    onClose: () => void;
    show: boolean;
    onSave?: () => void;
}

export function AddOrEditDetailsModal({ title, children, onClose, show }: DetailsModalProps) {
    if (!show) return null;

    return (
        <div className="details-modal-overlay">
            <div className="details-modal">
                <div className="details-modal-header">
                    <div className="details-modal-title">{title}</div>
                    <IconButton icon={<i className="fa-solid fa-xmark"></i>} onClick={onClose} className={"details-modal-close-button"} ariaLabel={"Close details modal button"}/>
                </div>
                <div className="details-modal-content">{children}</div>
            </div>
        </div>
    );
}
