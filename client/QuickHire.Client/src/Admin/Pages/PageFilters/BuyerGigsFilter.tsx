import { useState } from "react";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { ButtonDropdownContainer } from "../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";
import { PriceRangePopulate } from "../../../Shared/Dropdowns/Populate/PriceRange/PriceRangePopulate";
import { DeliveryTimePopulate } from "../../../Shared/Dropdowns/Populate/DeliveryTime/DeliveryTimePopulate";
import { SellerPopulateDropdown } from "../../../Shared/Dropdowns/Populate/Seller/SellerPopulate";
import { SelectedOption, ServiceIncludesDropdown } from "../../../Shared/Dropdowns/Populate/ServiceIncludes/ServiceIncludes";

interface BuyerGigsFilterProps {
    setSelectedPriceRangeId: (id: number | undefined) => void;
    selectedPriceRangeId: number | undefined;
    setSelectedDeliveryTimeId: (id: number | undefined) => void;
    selectedDeliveryTimeId: number | undefined;
    selectedCountryIds: number[];
    selectedLanguageIds: number[];
    handleOnSellerFiltersApply: (filters: {
        selectedCountryIds: number[];
        selectedLanguageIds: number[];
    }) => void;
    selectedOptions?: SelectedOption[]; 
    setSelectedOptions?: (options: SelectedOption[]) => void;
    subSubCategoryId?: number;
    showServiceIncludesFilter?: boolean;
}

export function BuyerGigsFilter({showServiceIncludesFilter,subSubCategoryId, selectedOptions, setSelectedOptions, handleOnSellerFiltersApply, selectedCountryIds, selectedLanguageIds, setSelectedPriceRangeId, selectedPriceRangeId, setSelectedDeliveryTimeId, selectedDeliveryTimeId }: BuyerGigsFilterProps) {
    const [showPriceRangeDropdown, setShowPriceRangeDropdown] = useState(false);
    const [showDeliveryTimeDropdown, setShowDeliveryTimeDropdown] = useState(false);
    const [showSellerDropdown, setShowSellerDropdown] = useState(false);
    const [showServiceIncludes, setShowServiceIncludes] = useState(false);
    const handleServicerDropdownVisibility = () => {
        setShowServiceIncludes(!showServiceIncludes);
    }

    const handleSellerDropdownVisibility = () => {
        setShowSellerDropdown(!showSellerDropdown);
    }
    const handleShowDeliveryTimeDropdown = () => {
        setShowDeliveryTimeDropdown(!showDeliveryTimeDropdown);
    }
    const handlePriceRangeDropdownVisibility = () => {
        setShowPriceRangeDropdown(!showPriceRangeDropdown);
    }
    return (
        <div className="page-filter-section" aria-label="buyer-gigs-filter">
            {showServiceIncludesFilter && <ButtonDropdownContainer>
                <ActionButton text={<>Service includes <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleServicerDropdownVisibility} className={"dropdown-button service-includes"} ariaLabel={"Dropdown Price Range Button"} />
               <div className={`filter-dropdown ${showServiceIncludes ? 'show' : ''}`}>
                 {showServiceIncludes && <ServiceIncludesDropdown  subSubCategoryId={subSubCategoryId} selectedOptions={selectedOptions ?? []} onApply={setSelectedOptions ?? (() => {})} />}
               </div>
            </ButtonDropdownContainer>}
            
            <ButtonDropdownContainer>
                <ActionButton text={<>Seller details <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleSellerDropdownVisibility} className={"dropdown-button price-range"} ariaLabel={"Dropdown Price Range Button"} />
               <div className={`filter-dropdown ${showSellerDropdown ? 'show' : ''}`}>
                 {showSellerDropdown && <SellerPopulateDropdown selectedCountryIds={selectedCountryIds} selectedLanguageIds={selectedLanguageIds} onApply={handleOnSellerFiltersApply} />}
               </div>
            </ButtonDropdownContainer>
            <ButtonDropdownContainer>
               <ActionButton text={<>Price Range <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handlePriceRangeDropdownVisibility} className={"dropdown-button price-range"} ariaLabel={"Dropdown Price Range Button"} />
               <div className={`filter-dropdown ${showPriceRangeDropdown ? 'show' : ''}`}>
                 {showPriceRangeDropdown && <PriceRangePopulate setShow={setShowPriceRangeDropdown} show={showPriceRangeDropdown} setSelectedId={setSelectedPriceRangeId} selectedId={selectedPriceRangeId} />}
               </div>
            </ButtonDropdownContainer>
            <ButtonDropdownContainer>
                <ActionButton text={<>Delivery time <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleShowDeliveryTimeDropdown} className={"dropdown-button price-range"} ariaLabel={"Dropdown Price Range Button"} />
                <div className={`filter-dropdown ${showDeliveryTimeDropdown ? 'show' : ''}`}>
                    {showDeliveryTimeDropdown && <DeliveryTimePopulate setShow={setShowDeliveryTimeDropdown} show={showDeliveryTimeDropdown} setSelectedId={setSelectedDeliveryTimeId} selectedId={selectedDeliveryTimeId} />}
                </div>
            </ButtonDropdownContainer>
            

            
        </div>
    );
}