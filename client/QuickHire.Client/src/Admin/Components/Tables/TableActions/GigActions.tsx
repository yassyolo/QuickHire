import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { CategoriesActionButton } from "../CategoriesActionButton";
import { DeactivateGig } from "../../Modals/Deactivate/DeactivateGig";

export interface GigActionsProps {
    gig: { id: number };
    onDeactivateSuccess: (id: number) => void;
}

export function GigActions ({gig, onDeactivateSuccess}: GigActionsProps) {
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const {id} = gig;
    const navigate = useNavigate();

    const handleDeactivateModalVisibility = () => setShowDeactivateModal(!showDeactivateModal);
    const handleSeeButtonClick = () => navigate(`/admin/gigs/${id}`);

    return (
        <>
            <CategoriesActionButton onSeeButtonClick={handleSeeButtonClick} onDeactivateModalVisibility={handleDeactivateModalVisibility}></CategoriesActionButton>           
            {showDeactivateModal && (<DeactivateGig id={id} onClose={handleDeactivateModalVisibility} deactivateGig={onDeactivateSuccess} />)}
        </>
    );
};
