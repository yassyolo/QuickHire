import { DeactivateModalNotPossible } from "./Common/DeactivateNotPossibleModal";
import { SubSubCategoriesInSubCategory } from "./DeactivateSubCategoryModal";

export interface DeactivateSubCategoryNotPossibleProps {
    id: number;
    subSubCategories: SubSubCategoriesInSubCategory[];
    onClose: () => void;
}

export function DeactivateSubCategoryNotPossible({id, subSubCategories, onClose }: DeactivateSubCategoryNotPossibleProps) {
    return (
        <DeactivateModalNotPossible id={id} affectedItems={"Sub sub categories"} onClose={onClose}>
            <div className="deactivate-modal-affected-items-list">
                {subSubCategories.map((subSubCategory) => (
                    <div key={subSubCategory.id ?? 0} className="deactivate-modal-affected-items-list-item"> {subSubCategory.name} </div>
                ))}
            </div>
        </DeactivateModalNotPossible> 
    );
}