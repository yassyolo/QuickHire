import { useState } from "react";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { ButtonDropdownContainer } from "../../Dropdowns/Common/ButtonDropdownContainer";
import { PriceRangePopulate } from "../../Dropdowns/Populate/PriceRangePopulate";

interface BuyerGigsFilterProps {
    setSelectedPriceRangeId: (id: number | undefined) => void;
    selectedPriceRangeId: number | undefined;
}

export function BuyerGigsFilter({ setSelectedPriceRangeId, selectedPriceRangeId }: BuyerGigsFilterProps) {
    const [showPriceRangeDropdown, setShowPriceRangeDropdown] = useState(false);
    const handlePriceRangeDropdownVisibility = () => {
        setShowPriceRangeDropdown(!showPriceRangeDropdown);
    }
    return (
                    <ButtonDropdownContainer>  
                    <ActionButton text={<>Price Range <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handlePriceRangeDropdownVisibility} className={"dropdown-button price-range"} ariaLabel={"Dropdown Price Range Button"} />
                    <div className={`filter-dropdown ${showPriceRangeDropdown ? 'show' : ''}`}>
                     {showPriceRangeDropdown && <PriceRangePopulate setShow={setShowPriceRangeDropdown} show={showPriceRangeDropdown} setSelectedId={setSelectedPriceRangeId} selectedId={selectedPriceRangeId} /> }
                    </div>
                    </ButtonDropdownContainer>
    );
}