import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { EditSubSubCategoryModal } from "../../Modals/Edit/EditSubSubCategoryModal";
import { DeactivateSubSubCategoryModal } from "../../Modals/Deactivate/SubSubCategory/DeactivateSubSubCategoryModal";
import { CategoriesActionButton } from "../CategoriesActionButton";

export interface SubSubCategoryActionsProps {
    category: {id: number};
    details: { name: string; };
    onEditSuccess: (id: number, newName: string) => void;
    onDeactivateSuccess: (id: number) => void;
}

export function SubSubCategoryActions({category, details, onEditSuccess, onDeactivateSuccess}: SubSubCategoryActionsProps) {
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const {id} = category;
    const navigate = useNavigate();

    const handleEditModalVisibility = () => setShowEditModal(!showEditModal);
    const handleDeactivateModalVisibility = () => setShowDeactivateModal(!showDeactivateModal);
    const handleSeeButtonClick = () => { navigate(`/admin/sub-sub-categories/${id}`); }

    return (
        <>
            <CategoriesActionButton onSeeButtonClick={handleSeeButtonClick} onEditModalVisibility={handleEditModalVisibility} onDeactivateModalVisibility={handleDeactivateModalVisibility}></CategoriesActionButton>
            {showEditModal && (<EditSubSubCategoryModal name={details.name} id={id} showModal={showEditModal} onClose={handleEditModalVisibility} onEditSuccess={onEditSuccess} />)}
            {showDeactivateModal && (<DeactivateSubSubCategoryModal id={id} showModal={showDeactivateModal} onClose={handleDeactivateModalVisibility} onDeactivateSuccess={onDeactivateSuccess}/>)}
        </>
    )
}