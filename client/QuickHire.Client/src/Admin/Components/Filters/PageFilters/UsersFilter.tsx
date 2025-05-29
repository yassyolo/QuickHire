import { useState } from "react";
import { SearchById } from "../Inputs/SearchById";
import { SearchByKeyword } from "../Inputs/SearchByKeyword";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton";
import { CountriesPopulate } from "../../Dropdowns/Populate/CountriesPopulate";
import { ButtonDropdownContainer } from "../../Dropdowns/Common/ButtonDropdownContainer";
import { RolePopulate } from "../../Dropdowns/Populate/RolePopulate";

interface UserFilterProps {
    setId(id: number| undefined): void;
    setKeyword(keyword: string| undefined): void;
    setSelectedRoleId(id: number | undefined): void;
    setSelectedCountryId(id: number | undefined): void;
    selectedRoleId: number;
    selectedCountryId: number;
}


export function UsersFilter ({setId, setKeyword, setSelectedRoleId, setSelectedCountryId, selectedRoleId, selectedCountryId} : UserFilterProps) {
    const [showRoleDropdown, setShowRoleDropdown] = useState(false);
    const [showCountryDropdown, setShowCountryDropdown] = useState(false);

    const handleRoleDropdownVisibility = () => setShowRoleDropdown(!showRoleDropdown);
    const handleCountryDropdownVisibility = () => setShowCountryDropdown(!showCountryDropdown);

    return(
        <div className="page-filter-section" aria-label="users-filter">
            <SearchById setId={setId} />
            <SearchByKeyword setKeyword={setKeyword} />   
            <ButtonDropdownContainer>
                <ActionButton text={<>Roles <i className="bi bi-chevron-down" style={{ fontSize: "14 !important" }}></i></>} onClick={handleRoleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Role Button"} />
                <div className={`filter-dropdown ${showRoleDropdown ? 'show' : ''}`}>
                  {showRoleDropdown && <RolePopulate setShow={handleRoleDropdownVisibility} show={showRoleDropdown} setSelectedId={setSelectedRoleId} selectedId={selectedRoleId} /> }
                </div>                  
            </ButtonDropdownContainer>
            <ButtonDropdownContainer>
                <ActionButton text={<>Country <i className="bi bi-chevron-down" style={{ fontSize: "14 !important" }}></i></>} onClick={handleCountryDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Country Button"} />
                <div className={`filter-dropdown ${showCountryDropdown ? 'show' : ''}`}>
                  {showCountryDropdown && <CountriesPopulate setShow={handleCountryDropdownVisibility} show={showCountryDropdown} setSelectedId={setSelectedCountryId} selectedId={selectedCountryId} /> }
                </div>          
            </ButtonDropdownContainer>   
        </div>
    );
}

