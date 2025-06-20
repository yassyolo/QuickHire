import { useState } from "react";
import axios from "../../../../../axiosInstance";
import { Modal } from "../../../../../Shared/Modals/Common/Modal";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { ModalActions } from "../../../../../Shared/Modals/Common/ModalActions";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { isAxiosError } from "axios";

interface ReportModalProps {
    gigId?: number;
    userId?: number;
    onClose: () => void;
}

export function ReportModal({ gigId, userId, onClose }: ReportModalProps) {
    const [reason, setReason] = useState<string>("");
    const [showReasonError, setShowReasonError] = useState<string[]>([]);
    const [showReasonTooltip, handleShowReasonTooltip] = useTooltip();

    const handleReasonChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setReason(e.target.value);
        setShowReasonError([]); 
    };

    const handleDeactivate = async () => {
        try {
            const url = `https://localhost:7267/admin/report`;
            await axios.put(url, { gigId, SellerId: userId, reason });
            onClose(); 
        } catch (error: unknown) {
            if (isAxiosError(error) && error.response?.status === 400) {
                setShowReasonError(error.response.data.errors?.Reason || []);
            } else {
                console.error("Error reporting gig:", error);
            }
        }
    };

    return (
        <Modal>
            <div aria-label="deactivate-modal" className="modal-title">
                Report
            </div>
            <div id="modal-info" className="modal-info">
    Please provide a reason for reporting this item. Your report will be reviewed by our moderation team and appropriate action will be taken.
            </div>
            <div id="modal-body" className="modal-body">
                <FormGroup
                    error={showReasonError}
                    id="deactivation-reason"
                    label="Reason"
                    tooltipDescription="Briefly explain why this item is being deactivated (e.g., duplicated, reported or violates guidelines)."
                    type="text"
                    value={reason}
                    onChange={handleReasonChange}
                    placeholder="Enter reason"
                    ariaDescribedBy="reason-help"
                    onShowTooltip={handleShowReasonTooltip}
                    showTooltip={showReasonTooltip}
                />
            </div>
            <ModalActions id="deactivate-main-category-actions">
                <ActionButton
                    text="Back"
                    onClick={onClose}
                    className="back-button"
                    ariaLabel="Back Button"
                />
                <ActionButton
                    text="Continue"
                    onClick={handleDeactivate}
                    className="continue-button"
                    ariaLabel="Continue Button"
                />
            </ModalActions>
        </Modal>
    );
}
