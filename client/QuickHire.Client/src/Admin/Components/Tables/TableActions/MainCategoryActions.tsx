import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { CategoriesActionButton } from "../CategoriesActionButton";
import { EditMainCategoryModal } from "../../Modals/Edit/EditMainCategoryModal";
import { DeactivateMainCategoryModal } from "../../Modals/Deactivate/DeactivateMainCategoryModal";
export interface MainCategoryActionsProps {
    category: { id: number };
    details: { name: string; description: string };
    onEditSuccess: (id: number, name: string, description: string) => void;
    onDeactivateSuccess: (id: number) => void;
}

export function MainCategoryActions ({category, onEditSuccess, onDeactivateSuccess, details}: MainCategoryActionsProps) {
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const {id} = category;
    const navigate = useNavigate();

    const handleEditModalVisibility = () => setShowEditModal(!showEditModal);
    const handleDeactivateModalVisibility= () => setShowDeactivateModal(!showDeactivateModal);
    const handleSeeButtonClick = () => navigate(`/admin/main-categories/${id}`);

    return (
        <>
        <CategoriesActionButton onSeeButtonClick={handleSeeButtonClick} onEditModalVisibility={handleEditModalVisibility} onDeactivateModalVisibility={handleDeactivateModalVisibility}></CategoriesActionButton>
        {showEditModal && (<EditMainCategoryModal initialName={details.name} initialDescription={details.description} id={id} showModal={showEditModal} onClose={handleEditModalVisibility} onEditSuccess={onEditSuccess} /> )} 
        {showDeactivateModal && (<DeactivateMainCategoryModal id={id} showModal={showDeactivateModal} onClose={handleDeactivateModalVisibility} onDeactivateSuccess={onDeactivateSuccess}/>)}
        </>
    );
};
