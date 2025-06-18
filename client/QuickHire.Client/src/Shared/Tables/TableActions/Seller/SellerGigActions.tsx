import { useState } from "react";
import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";
import axios from "../../../../axiosInstance";
import { DeleteGigModal } from "../../../Modals/Delete/Gig/DeleteGigModal";
import { GigPreview } from "../../../../Gigs/Pages/GigPreview/GigPreview";
import { GigStatisticsPage } from "../../../../Gigs/Pages/GigStatistics/GigStatisticsPage";

export interface GigActionsProps {
    gig: { id: number };
    paused: boolean;
    onActivateGigSuccess: (id: number) => void;
    onDeactivateGigSuccess: (id: number) => void;
    onDeleteSuccess: (id: number) => void;
}

export function SellerGigActions ({gig, paused, onActivateGigSuccess, onDeactivateGigSuccess, onDeleteSuccess}: GigActionsProps) {
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const [showActionsDropdown, setShowActionsDropdown] = useState(false);
    const [onEditModalVisibility, setShowEditModalVisibility] = useState(false);
    const [onPreviewModalVisibility, setShowPreviewModalVisibility] = useState(false);
    const [showStatisticsModal, setShowStatisticsModal] = useState(false);

    const handleStatisticsModalVisibility = () => {
        setShowStatisticsModal(!showStatisticsModal);
        setShowActionsDropdown(false);
    };

    const handlePreviewModalVisibility = () => {
        setShowPreviewModalVisibility(!onPreviewModalVisibility);
        setShowActionsDropdown(false);
    };

    const handleActivateGig = () => {
    const url = `https://localhost:7267/seller/gigs/activate`;
    const params = {
        Id: gig.id,
        Paused: paused, 
    };

    axios.put(url, null, { params })
        .then(() => {
            if (paused) {
                onActivateGigSuccess(gig.id);
            } else {
                onDeactivateGigSuccess(gig.id);
            }
        })
        .catch(error => {
            console.error("Error activating gig:", error);
        });
};

    const handleEditModalVisibility = () => setShowEditModalVisibility(!onEditModalVisibility);

    const handleActionsDropdownVisibility = () => setShowActionsDropdown(!showActionsDropdown);
    const {id} = gig;

    const handleDeactivateModalVisibility = () => setShowDeactivateModal(!showDeactivateModal);

    return (
        <>
            <div className="actions-container">
                <ActionButton text={<i className="bi bi-caret-down-fill"></i>} onClick={handleActionsDropdownVisibility} className={"actions-dropdown-visibility-button"} ariaLabel={"ActionsDropdownVisibility"} />
                {showActionsDropdown && (
                    <div className="actions-dropdown">
                        <div className="actions-dropdown-item" onClick={handlePreviewModalVisibility}>Preview</div>
                        <div className="actions-dropdown-item" onClick={handleStatisticsModalVisibility}>Analytics</div>
                        <div className="actions-dropdown-item" onClick={handleEditModalVisibility}>Edit</div>
                        {paused ? (
                            <div className="actions-dropdown-item" onClick={handleActivateGig}>Activate</div>
                        ) : (
                            <div className="actions-dropdown-item" onClick={handleActivateGig}>Pause</div>
                        )}
                        <div className="actions-dropdown-item" onClick={handleDeactivateModalVisibility}>Delete</div>
                        { showDeactivateModal && <DeleteGigModal onClose={handleDeactivateModalVisibility} onDeactivateSuccess={onDeleteSuccess} showModal={true} id={id}/>}
                    </div>
                )}
            </div>

            {onPreviewModalVisibility && <GigPreview gigId={id} onGigPreviewClose={handlePreviewModalVisibility}/>}
            {showStatisticsModal && <GigStatisticsPage id={id} onGigPreviewClose={handleStatisticsModalVisibility} />}

        </>
    );
};
