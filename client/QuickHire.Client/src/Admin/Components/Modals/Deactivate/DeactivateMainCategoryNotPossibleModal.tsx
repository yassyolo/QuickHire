import { SubCategoriesInMainCategory } from "./DeactivateMainCategoryModal";
import { DeactivateModalNotPossible } from "./Common/DeactivateNotPossibleModal";

export interface DeactivateModalNotPossibleProps {
    id: number;
    subCategories: SubCategoriesInMainCategory[];
    onClose: () => void;
}

export function DeactivateMainCategoryNotPossible({id, subCategories, onClose }: DeactivateModalNotPossibleProps) {
    return (
        <DeactivateModalNotPossible id={id} affectedItems={"Sub Categories"} onClose={onClose}>
            <div className="deactivate-modal-affected-items-list">
                {subCategories.map((subCategory) => (
                    <div key={subCategory.id} className="deactivate-modal-affected-items-list"> {subCategory.name} </div>
                ))}
            </div>
        </DeactivateModalNotPossible> 
    );
}