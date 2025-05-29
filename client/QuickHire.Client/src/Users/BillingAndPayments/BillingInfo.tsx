import { useEffect, useState } from "react";
import { FormGroup } from "../../Shared/Forms/FormGroup";
import { useTooltip } from "../../Shared/Tooltip/Tooltip";
import { ActionButton } from "../../Shared/Buttons/ActionButton";
import axios from "axios";
import './BillingInfo.css';
import { SelectDropdown } from "../../Shared/Select/SelectDropdown";
import { Item } from "../../Admin/Components/Dropdowns/Common/PopulateDropdown";

export function BillingInfo() {
    const [fullName, setFullName] = useState<string>("");
    const [companyName, setCompanyName] = useState<string>("");
    const [city, setCity] = useState<string>("");
    const [address, setAddress] = useState<string>("");
    const [zipCode, setZipCode] = useState<string>("");
    const [showAddressTooltip, handleShowAddressTooltip] = useTooltip();
    const [showCountryTooltip, handleShowCountryTooltip] = useTooltip();
    const [showZipCodeTooltip, handleShowZipCodeTooltip] = useTooltip();
    const [showCityTooltip, handleShowCityTooltip] = useTooltip();
    const [showCompanyNameTooltip, handleShowCompanyNameTooltip] = useTooltip();
    const [showFullNameTooltip, handleShowFullNameTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ FullName?: string[]; CompanyName?: string[]; City?: string[]; Address?: string[]; ZipCode?: string[]; }>({});
    const [populatedData, setPopulatedData] = useState<Item[]>([]);
    const [selectedCountryId, setSelectedCountryId] = useState<number | undefined>(undefined);

    useEffect(() => {
        setFullName("");
        setCompanyName("");
        setCity("");
        setAddress("");
        setZipCode("");
        setValidationErrors({});
        const fetchData = async () => {
      try {
        const url = "https://localhost:7267/admin/filters/countries";
        const response = await axios.get<Item[]>(url);
        setPopulatedData(response.data);
      } catch (error) {
        console.error("Error fetching countries options:", error);
      }
    };
    fetchData();
    }, []);

    const handleFullNameChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setFullName(event.target.value);
    }

    const handleCompanyNameChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCompanyName(event.target.value);
    }

    const handleCityChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setCity(event.target.value);
    }

    const handleAddressChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setAddress(event.target.value);
    }

    const handleZipCodeChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setZipCode(event.target.value);
    }

    const handleSelectedCountryId = (id: number | undefined) => {
        setSelectedCountryId(id);
    }

   
        const handleOnSaveChanges = async () => {
            try{
                const url = "/api/billing-info";
                await axios.post(url, {
                    FullName: fullName,
                    CompanyName: companyName,
                    City: city,
                    Address: address,
                    ZipCode: zipCode
                });
                alert("Billing information saved successfully.");
            }catch(error: unknown) {
                if (axios.isAxiosError(error)) {
                    const response = error.response;
                    if (response && response.status === 400 && response.data) {
                        setValidationErrors({
                            FullName: response.data.errors?.FullName || [],
                            CompanyName: response.data.errors?.CompanyName || [],
                            City: response.data.errors?.City || [],
                            Address: response.data.errors?.Address || [],
                            ZipCode: response.data.errors?.ZipCode || []
                        });
                    } else {
                        alert("An unexpected error occurred while saving billing information.");
                    }
                } else {
                    console.error("Unexpected error:", error);
                }
            }
        }

    return (
    <div className="billing-info-container">
        <div className="biling-history-title">Billing Information</div>
        <div className="billing-info-form">
            <FormGroup 
                id={"full-name"} 
                label={"Full name"} 
                tooltipDescription={"Enter your full legal name as it appears on official documents."} 
                type={"text"} 
                value={fullName} 
                onChange={handleFullNameChange} 
                placeholder={"Enter full name"} 
                ariaDescribedBy={"full-name-help"} 
                onShowTooltip={handleShowFullNameTooltip} 
                showTooltip={showFullNameTooltip} 
                error={validationErrors.FullName || []} 
            />
            <FormGroup 
                id={"company-name"} 
                label={"Company name"} 
                tooltipDescription={"Enter the registered name of your company or organization."} 
                type={"text"} 
                value={companyName} 
                onChange={handleCompanyNameChange} 
                placeholder={"Enter company name"} 
                ariaDescribedBy={"company-name-help"} 
                onShowTooltip={handleShowCompanyNameTooltip} 
                showTooltip={showCompanyNameTooltip} 
                error={validationErrors.CompanyName || []} 
            />
            <SelectDropdown
                id="country"
                label="Country"
                options={populatedData}
                value={typeof selectedCountryId === "string" ? Number(selectedCountryId) || undefined : selectedCountryId}
                onChange={handleSelectedCountryId}
                getOptionLabel={(opt) => opt.name}
                getOptionValue={(opt) => typeof opt.id === "number" ? opt.id : (opt.id ? Number(opt.id) : undefined)}
                tooltipDescription={"Select your country to ensure accurate billing and tax information."}
                showTooltip={showCountryTooltip}
                ariaDescribedBy={"dropdown-help"}
                onShowTooltip={handleShowCountryTooltip}
            />
            <FormGroup 
                id={"city"} 
                label={"City"} 
                tooltipDescription={"Enter the city where your billing address is located."} 
                type={"text"} 
                value={city} 
                onChange={handleCityChange} 
                placeholder={"Enter city"} 
                ariaDescribedBy={"city-help"} 
                onShowTooltip={handleShowCityTooltip} 
                showTooltip={showCityTooltip} 
                error={validationErrors.City || []} 
            />
            <FormGroup 
                id={"address"} 
                label={"Address"} 
                tooltipDescription={"Provide the full street address for billing purposes."} 
                type={"text"} 
                value={address} 
                onChange={handleAddressChange} 
                placeholder={"Enter address"} 
                ariaDescribedBy={"address-help"} 
                onShowTooltip={handleShowAddressTooltip} 
                showTooltip={showAddressTooltip} 
                error={validationErrors.Address || []} 
            />
            <FormGroup 
                id={"zip-code"} 
                label={"Zip Code"} 
                tooltipDescription={"Enter the postal or zip code for your billing address."} 
                type={"text"} 
                value={zipCode} 
                onChange={handleZipCodeChange} 
                placeholder={"Enter Zip Code"} 
                ariaDescribedBy={"zip-code-help"} 
                onShowTooltip={handleShowZipCodeTooltip} 
                showTooltip={showZipCodeTooltip} 
                error={validationErrors.ZipCode || []} 
            />
            <ActionButton 
                text={"Save changes"} 
                onClick={handleOnSaveChanges} 
                className={"save-billing-info-button"} 
                ariaLabel={"Save billing info button"} 
            />
        </div>
    </div>
);

}