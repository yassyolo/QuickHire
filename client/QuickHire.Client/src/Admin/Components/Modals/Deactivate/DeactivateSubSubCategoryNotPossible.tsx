import { DeactivateModalNotPossible } from "./Common/DeactivateNotPossibleModal";
import { FiltersInSubSubCategory } from "./DeactivateSubSubCategoryModal";
import "./DeactivateSubSubCategoryNotPossible.css";

export interface DeactivateSubSubCategoryNotPossibleProps {
    id: number;
    filters: FiltersInSubSubCategory[];
    onClose: () => void;
}

export function DeactivateSubSubCategoryNotPossible({id, filters, onClose }: DeactivateSubSubCategoryNotPossibleProps) {
    return (
        <DeactivateModalNotPossible id={id} affectedItems={"Filters"} onClose={onClose}>
            <div className="deactivate-modal-affected-items-list">
                {filters.map((filter) => (                 
                    <>
                        <div key={filter.id} className="deactivate-modal-affected-items-list-item title"> {filter.title}: </div>
                        {filter.items && filter.items.map((item) => (
                            <div key={item.id} className="deactivate-modal-affected-items-list-item"> {item.value} </div>
                        ))}
                    </>
                ))}
            </div>
        </DeactivateModalNotPossible> 
    );
}