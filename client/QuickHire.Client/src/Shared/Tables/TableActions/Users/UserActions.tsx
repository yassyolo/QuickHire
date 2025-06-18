import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { CategoriesActionButton } from "../Categories/Common/CategoriesActionButton";
import { DeactivateUser } from "../../../Modals/Deactivate/User/DeactivateUser";

export interface UserActionsProps {
    user: { id: string };
    onDeactivateSuccess: (id: string) => void;
}

export function UserActions ({user, onDeactivateSuccess}: UserActionsProps) {
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const {id} = user;
    const navigate = useNavigate();
    const handleDeactivateModalVisibility = () => setShowDeactivateModal(!showDeactivateModal);
    const handleSeeButtonClick = () => navigate(`/admin/users/${id}`);

    return (
        <>
            <CategoriesActionButton onSeeButtonClick={handleSeeButtonClick} onDeactivateModalVisibility={handleDeactivateModalVisibility}></CategoriesActionButton>           
            {showDeactivateModal && (<DeactivateUser id={id} onClose={handleDeactivateModalVisibility} deactivateUser={onDeactivateSuccess} />)}
        </>
    );
};
