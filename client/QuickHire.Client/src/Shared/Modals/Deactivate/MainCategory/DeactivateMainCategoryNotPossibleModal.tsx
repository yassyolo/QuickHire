import { SubCategoriesInMainCategory } from "./DeactivateMainCategoryModal";
import { DeactivateModalNotPossible } from "../Common/DeactivateNotPossibleModal/DeactivateNotPossibleModal";

export interface DeactivateModalNotPossibleProps {
    id: number;
    subCategories: SubCategoriesInMainCategory[];
    onClose: () => void;
}

export function DeactivateMainCategoryNotPossible({id, subCategories, onClose }: DeactivateModalNotPossibleProps) {
    return (
        <DeactivateModalNotPossible id={id} affectedItems={"sub categories"} onClose={onClose}>
            <div className="deactivate-modal-affected-items-list">
                {subCategories.map((subCategory) => (
                    <div key={subCategory.id} className="deactivate-modal-affected-items-list"> {subCategory.name} </div>
                ))}
            </div>
        </DeactivateModalNotPossible> 
    );
}