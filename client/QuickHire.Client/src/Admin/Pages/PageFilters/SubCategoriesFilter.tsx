import { useState } from "react";
import { SearchById } from "../../../Shared/Forms/SearchInputs/SearchById/SearchById";
import { SearchByKeyword } from "../../../Shared/Forms/SearchInputs/SearchByKeyword/SearchByKeyword";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { CategoriesPopulate } from "../../../Shared/Dropdowns/Populate/Categories/CategoriesPopulate";
import { ButtonDropdownContainer } from "../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";

interface SubCategoriesFilterProps {
    setId(id: number| undefined): void;
    setKeyword(keyword: string): void;
    setSelectedMainCategoryId(id: number | undefined): void;
    selectedMainCategoryId: number | undefined;
}


export function SubCategoriesFilter ({setId, setKeyword, setSelectedMainCategoryId, selectedMainCategoryId} : SubCategoriesFilterProps) {
    const [showDropdown, setShowDropdown] = useState(false);

    const handleDropdownVisibility = () => setShowDropdown(!showDropdown);

    return(
        <>
        <div className="page-filter-section" aria-label="Sub-categories-filter">
            <SearchById setId={setId} />
            <SearchByKeyword setKeyword={setKeyword} />
            <ButtonDropdownContainer>
               <ActionButton text={<>Main Categories <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Main Categories Button"} />
               <div className={`filter-dropdown ${showDropdown ? 'show' : ''}`}>
                 {showDropdown && <CategoriesPopulate setShow={setShowDropdown} show={showDropdown} setSelectedId={setSelectedMainCategoryId} type="Main" selectedId={selectedMainCategoryId}/>}
               </div>
            </ButtonDropdownContainer>
        </div>
        </>
    );
}

