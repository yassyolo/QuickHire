import { useState } from "react";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { WithdrawProjectBrief } from "../../Modals/Deactivate/WithdrawProjectBrief";
import { ProjectBriefPreview } from "../../../../Gigs/ProjectBriefs/ProjectBriefPreview";
import { SendCustomOfferModal } from "../../../../Gigs/CustomOffers/Send/SendCustomOfferModal";

export interface GigActionsProps {
    project: { id: number };
    onWithdrawSuccess?: (id: number) => void;
    onSendCustomOfferSuccess?: (id: number) => void;
    showNuyerInfo: boolean;
}

export function ProjectBriefActions ({project, onWithdrawSuccess, onSendCustomOfferSuccess, showNuyerInfo}: GigActionsProps) {
    const [showWithdrawModal, setShowWithdrawModal] = useState(false);
    const [showCustomOfferModal, setShowCustomOfferModal] = useState(false);
    const [showActionsDropdown, setShowActionsDropdown] = useState(false);
    const [onPreviewModalVisibility, setShowPreviewModalVisibility] = useState(false);

    const handleShowWithdrawModal = () => {
        setShowWithdrawModal(!showWithdrawModal);
        setShowActionsDropdown(false);
    };

    const handleShowCustomOfferModal = () => {
        setShowCustomOfferModal(!showCustomOfferModal);
        setShowActionsDropdown(false);
    };

    const handlePreviewModalVisibility = () => {
        setShowPreviewModalVisibility(!onPreviewModalVisibility);
        setShowActionsDropdown(false);
    };

    

    const handleActionsDropdownVisibility = () => setShowActionsDropdown(!showActionsDropdown);
    const {id} = project;


    return (
        <>
            <div className="actions-container">
                <ActionButton text={<i className="bi bi-caret-down-fill"></i>} onClick={handleActionsDropdownVisibility} className={"actions-dropdown-visibility-button"} ariaLabel={"ActionsDropdownVisibility"} />
                {showActionsDropdown && (
                    <div className="actions-dropdown">
                        <div className="actions-dropdown-item" onClick={handlePreviewModalVisibility}>Preview</div>
                        { onWithdrawSuccess && (
                            <div className="actions-dropdown-item" onClick={handleShowWithdrawModal}>Withdraw</div>
                        )}

                        { onSendCustomOfferSuccess && (
                            <div className="actions-dropdown-item" onClick={handleShowCustomOfferModal}>Send custom offer</div>
                        )}
                    </div>
                )}
            </div>

            {onPreviewModalVisibility && <ProjectBriefPreview id={id} onClose={handlePreviewModalVisibility} showBuyerInfo={showNuyerInfo} />}
            {showWithdrawModal && onWithdrawSuccess && <WithdrawProjectBrief id={id} onClose={handleShowWithdrawModal} onDeactivateSuccess={onWithdrawSuccess} showModal={true} />}
            {showCustomOfferModal && onSendCustomOfferSuccess && <SendCustomOfferModal id={id} onClose={handleShowCustomOfferModal} onSendCustomOfferSuccess={onSendCustomOfferSuccess} />}
        </>
    );
};
