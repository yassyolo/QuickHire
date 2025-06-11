import { DeactivateModalNotPossible } from "../Common/DeactivateNotPossibleModal/DeactivateNotPossibleModal";
import { FiltersInSubSubCategory } from "./DeactivateSubSubCategoryModal";

export interface DeactivateSubSubCategoryNotPossibleProps {
    id: number;
    filters: FiltersInSubSubCategory[];
    onClose: () => void;
}

export function DeactivateSubSubCategoryNotPossible({id, filters, onClose }: DeactivateSubSubCategoryNotPossibleProps) {
    return (
        <DeactivateModalNotPossible id={id} affectedItems={"filters"} onClose={onClose}>
            <div className="deactivate-modal-affected-items-list">
                {filters.map((filter) => (                 
                    <>
                        <div key={filter.id} className="deactivate-modal-affected-items-list-item"> {filter.title} </div>
                    </>
                ))}
            </div>
        </DeactivateModalNotPossible> 
    );
}