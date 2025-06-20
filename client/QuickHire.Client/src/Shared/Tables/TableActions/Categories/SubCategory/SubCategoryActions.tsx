import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { CategoriesActionButton } from "../Common/CategoriesActionButton";
import { EditSubCategoryModal } from "../../../../Modals/Edit/SubCategory/EditSubCategoryModal";
import { DeactivateSubCategoryModal } from "../../../../Modals/Deactivate/SubCategory/DeactivateSubCategoryModal";

export interface SubCategoryActionsProps {
    category: {id: number};
    details: { name: string; imageUrl: string; };
    onEditSuccess: (id: number, newName: string, newImage: string) => void;
    onDeactivateSuccess: (id: number) => void;
}

export function SubCategoryActions({category, onEditSuccess, onDeactivateSuccess, details}: SubCategoryActionsProps) {
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const {id} = category;
    const navigate = useNavigate();

    const handleEditModalVisibility = () => setShowEditModal(!showEditModal);
    const handleDeactivateModalVisibility = () => setShowDeactivateModal(!showDeactivateModal);
    const handleSeeButtonClick = () => { navigate(`/admin/sub-categories/${id}`); }

    return (
        <>
            <CategoriesActionButton onSeeButtonClick={handleSeeButtonClick} onEditModalVisibility={handleEditModalVisibility} onDeactivateModalVisibility={handleDeactivateModalVisibility}></CategoriesActionButton>
            {showEditModal && (<EditSubCategoryModal name={details.name} imageUrl={details.imageUrl} id={id} showModal={showEditModal} onClose={handleEditModalVisibility} onEditSuccess={onEditSuccess} />)}
            {showDeactivateModal && (<DeactivateSubCategoryModal id={id} showModal={showDeactivateModal} onClose={handleDeactivateModalVisibility} onDeactivateSuccess={onDeactivateSuccess}/>)}
        </>
    )
}