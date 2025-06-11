import { useState } from "react";
import { SearchById } from "../../../Shared/SearchInputs/SearchById/SearchById";
import { SearchByKeyword } from "../../../Shared/SearchInputs/SearchByKeyword/SearchByKeyword";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { ButtonDropdownContainer } from "../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";
import { RolePopulate } from "../../../Shared/Dropdowns/Populate/Roles/RolePopulate";

interface UserFilterProps {
    setId(id: number| undefined): void;
    setKeyword(keyword: string| undefined): void;
    setSelectedRoleId(id: string | undefined): void;
    selectedRoleId: string | undefined;
}


export function UsersFilter ({setId, setKeyword, setSelectedRoleId, selectedRoleId} : UserFilterProps) {
    const [showRoleDropdown, setShowRoleDropdown] = useState(false);

    const handleRoleDropdownVisibility = () => setShowRoleDropdown(!showRoleDropdown);

    return(
        <div className="page-filter-section" aria-label="users-filter">
            <SearchById setId={setId} />
            <SearchByKeyword setKeyword={setKeyword} />   
            <ButtonDropdownContainer>
                <ActionButton text={<>Roles <i className="bi bi-chevron-down"></i></>} onClick={handleRoleDropdownVisibility} className={"dropdown-button role"} ariaLabel={"Dropdown Role Button"} />
                <div className={`filter-dropdown ${showRoleDropdown ? 'show' : ''}`}>
                  {showRoleDropdown && <RolePopulate setShow={handleRoleDropdownVisibility} show={showRoleDropdown} setSelectedId={setSelectedRoleId} selectedId={selectedRoleId} /> }
                </div>                  
            </ButtonDropdownContainer>          
        </div>
    );
}

