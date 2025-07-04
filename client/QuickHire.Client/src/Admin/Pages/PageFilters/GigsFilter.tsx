import { useState } from "react";
import { SearchById } from "../../../Shared/Forms/SearchInputs/SearchById/SearchById";
import { SearchByKeyword } from "../../../Shared/Forms/SearchInputs/SearchByKeyword/SearchByKeyword";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { CategoriesPopulate } from "../../../Shared/Dropdowns/Populate/Categories/CategoriesPopulate";
import { ButtonDropdownContainer } from "../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";

interface GigFilterProps {
    setId(id: number | undefined): void;
    setKeyword(keyword: string): void;
    setSelectedSubCategoryId(id: number | undefined): void;
    setSelectedSubSubCategoryId(id: number | undefined): void;
    selectedSubCategoryId: number | undefined;
    selectedSubSubCategoryId: number | undefined;
}


export function GigsFilter ({setId, setKeyword, setSelectedSubCategoryId, setSelectedSubSubCategoryId, selectedSubCategoryId, selectedSubSubCategoryId} : GigFilterProps) {
    const [showSubSubCategoryDropdown, setShowSubSubCategoryDropdown] = useState(false);
    const [showSubCategoryDropdown, setShowSubCategoryDropdown] = useState(false);

    const handleSubSubCategoryDropdownVisibility = () => setShowSubSubCategoryDropdown(!showSubSubCategoryDropdown);
    const handleSubCategoryDropdownVisibility = () => setShowSubCategoryDropdown(!showSubCategoryDropdown);

    return(
        <>
        <div className="page-filter-section" aria-label="gigs-filter">
            <SearchById setId={setId} />
            <SearchByKeyword setKeyword={setKeyword} />     
            <ButtonDropdownContainer>  
            <ActionButton text={<>Sub Categories <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleSubCategoryDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Sub Categories Button"} />
            <div className={`filter-dropdown ${showSubCategoryDropdown ? 'show' : ''}`}>
             {showSubCategoryDropdown && <CategoriesPopulate setShow={handleSubCategoryDropdownVisibility} show={showSubCategoryDropdown} setSelectedId={setSelectedSubCategoryId} type={"Sub"} selectedId={selectedSubCategoryId} /> }
            </div>
            </ButtonDropdownContainer>
            <ButtonDropdownContainer>  
            <ActionButton text={<>Sub Sub Categories <i className="bi bi-chevron-down" style={{ fontSize: "520 !important", width: "190px !important" }}></i></>} onClick={handleSubSubCategoryDropdownVisibility} className={"dropdown-button sub-sub"} ariaLabel={"Dropdown Sub Categories Button"} />
            <div className={`filter-dropdown ${showSubSubCategoryDropdown ? 'show' : ''}`}>
             {showSubSubCategoryDropdown && <CategoriesPopulate setShow={handleSubSubCategoryDropdownVisibility} show={showSubSubCategoryDropdown} setSelectedId={setSelectedSubSubCategoryId} type={"Sub Sub"} selectedId={selectedSubSubCategoryId} /> }
            </div>
            </ButtonDropdownContainer> 
        </div>
        </>
    );
}

