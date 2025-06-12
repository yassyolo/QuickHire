import { useState } from "react";
import { SearchById } from "../../../Shared/Forms/SearchInputs/SearchById/SearchById";
import { SearchByKeyword } from "../../../Shared/Forms/SearchInputs/SearchByKeyword/SearchByKeyword";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { CategoriesPopulate } from "../../../Shared/Dropdowns/Populate/Categories/CategoriesPopulate";
import { ButtonDropdownContainer } from "../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";

interface SubSubCategoriesFilterProps {
    setId(id: number| undefined): void;
    setKeyword(keyword: string): void;
    setSelectedSubCategoryId(id: number | undefined): void;
    selectedSubCategoryId: number | undefined;
}


export function SubSubCategoriesFilter ({setId, setKeyword, setSelectedSubCategoryId, selectedSubCategoryId} : SubSubCategoriesFilterProps) {
    const [showDropdown, setShowDropdown] = useState(false);

    const handleDropdownVisibility = () => setShowDropdown(!showDropdown);

    return(
        <>
        <div className="page-filter-section" aria-label="sub-sub-categories-filter">
            <SearchById setId={setId} />
            <SearchByKeyword setKeyword={setKeyword} />
            <ButtonDropdownContainer>         
              <ActionButton text={<>Sub Categories <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Sub Categories Button"} />
              <div className={`filter-dropdown ${showDropdown ? 'show' : ''}`}>
               {showDropdown && <CategoriesPopulate setShow={setShowDropdown} show={showDropdown} setSelectedId={setSelectedSubCategoryId} type={"Sub"} selectedId={selectedSubCategoryId} /> }
              </div>
            </ButtonDropdownContainer>
        </div>
        </>
    );
}

