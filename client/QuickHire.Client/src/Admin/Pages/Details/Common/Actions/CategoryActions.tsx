import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import './CategoryActions.css';

interface CategoryActionsProps {
    onEditModalVisibility: () => void;
    onDeactivateModalVisibility: () => void;
}
export function CategoryActions({ onEditModalVisibility, onDeactivateModalVisibility }: CategoryActionsProps) {
    const handleEditMainCategoryModalVisibility = () => onEditModalVisibility();
    const handleDeactivateMainCategoryModalVisibility = () => onDeactivateModalVisibility();
    
    return(
        <div className="category-actions">
            <div className="category-actions-header">Actions</div>
                <div className="d-flex flex-row justify-content-between">
                    <IconButton icon={<i className="bi bi-pencil" style={{fontSize: "18px"}}></i>} onClick={handleEditMainCategoryModalVisibility} className="faq-edit-button" ariaLabel="Edit Category Button" />
                    <IconButton icon={<i className="bi bi-x" style={{fontSize: "20px", color: "red"}}></i>} onClick={handleDeactivateMainCategoryModalVisibility} className="faq-delete-button" ariaLabel="Deactivate Category Button" />
            </div>                                             
        </div>
    );
}