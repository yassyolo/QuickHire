import { useEffect, useState } from "react";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
import { useTooltip } from "../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { Item } from "../../../../Shared/Dropdowns/Common/PopulateDropdown";
import { FormGroup } from "../../../../Shared/Forms/FormGroup/FormGroup";
import { SelectDropdown } from "../../../../Shared/Dropdowns/Select/SelectDropdown";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { BillingInfoDetails } from "../OrderForm/OrderForm";
import "./OrderBillingDetailsForm.css";



interface BillingInfoProps {
    billingInfo: BillingInfoDetails;
    onBillingInfoUpdate: (billingInfo: BillingInfoDetails) => void;
    onAddBillingInfo: (billingInfo: BillingInfoDetails) => void;
}

export function OrderBillingDetailsForm({ billingInfo, onBillingInfoUpdate, onAddBillingInfo}: BillingInfoProps) {
    const [fullName, setFullName] = useState<string>("");
    const [showBillingInfo, setShowBillingInfo] = useState<boolean>(!!billingInfo);
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
    const [showEditModal, setShowEditModal] = useState<boolean>(false);

    useEffect(() => {
        setShowBillingInfo(true);
         if (billingInfo) {
        setShowBillingInfo(true);
        setFullName(billingInfo.fullName);
        setCompanyName(billingInfo.companyName);
        setCity(billingInfo.city);
        setAddress(billingInfo.street);
        setZipCode(billingInfo.zipCode);
        setSelectedCountryId(billingInfo.countryId);
    } else {
        setShowBillingInfo(false); 
    }
    }
    , [billingInfo]);

    const handleEditModalVisibility = () => {
        setShowEditModal(!showEditModal);
        setShowBillingInfo(!showBillingInfo);
        if (billingInfo) {
            setFullName(billingInfo.fullName);
            setCompanyName(billingInfo.companyName);
            setCity(billingInfo.city);
            setAddress(billingInfo.street);
            setZipCode(billingInfo.zipCode);
            setSelectedCountryId(billingInfo.countryId);
        }
    }

    const fetchCountries = async () => {
        try {
            const url = "https://localhost:7267/admin/filters/countries";
            const response = await axios.get<Item[]>(url);
            setPopulatedData(response.data);
        }
        catch (error) {
            console.error("Error fetching countries options:", error);
        }
    }

    useEffect(() => {
        fetchCountries();
        console.log("b info", billingInfo);
    }, []);

    const handleFullNameChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setFullName(event.target.value);
    const handleCompanyNameChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setCompanyName(event.target.value);
    const handleCityChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setCity(event.target.value);
    const handleAddressChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) =>  setAddress(event.target.value);
    const handleZipCodeChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) =>  setZipCode(event.target.value);

    const handleSelectedCountryId = (id: number | undefined) => {
        setSelectedCountryId(id);
    }

   
    const handleOnSaveChanges = async () => {
            try{
                const url = "https://localhost:7267/users/billings-and-payments";
                const response = await axios.post(url, {
                    FullName: fullName,
                    CompanyName: companyName,
                    City: city,
                    Street: address,
                    ZipCode: zipCode,
                    CountryId: selectedCountryId
                });
                onAddBillingInfo(response.data);
                setValidationErrors({});
                setFullName("");
                setCompanyName("");
                setCity("");
                setAddress("");
                setZipCode("");
                setSelectedCountryId(undefined);
                setShowEditModal(false);               

                alert("Billing information saved successfully.");
            }catch(error: unknown) {
                if (isAxiosError(error)) {
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

    const handleOnEdit = async () => {
        try {
            const url = "https://localhost:7267/users/billings-and-payments";
            await axios.put(url, {
                Id: billingInfo.id,
                FullName: fullName,
                CompanyName: companyName,
                City: city,
                Address: address,
                ZipCode: zipCode,
                Street: address,
                CountryId: selectedCountryId
            });
            alert("Billing information updated successfully.");
            setShowEditModal(false);
            onBillingInfoUpdate({
                id: billingInfo.id,
                fullName,
                companyName,
                city,
                street: address,
                zipCode,
                country: populatedData.find(country => country.id === selectedCountryId)?.name || "",
                countryId: selectedCountryId || 0
            });
        }
        catch (error: unknown) {
            if (isAxiosError(error)) {
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
                    alert("An unexpected error occurred while updating billing information.");
                }
            } else {
                console.error("Unexpected error:", error);
            }
        }
    }

    return (
    <div className="billing-info-page d-flex flex-column">
        { billingInfo && showBillingInfo && <div className="billing-info-details-page" style={{justifyContent:'center', alignItems: 'center', display: 'flex', flexDirection: 'row', border: '1px solid #e0e0e0', padding: '20px', borderRadius: '8px'}}>
            <div className="billing-info-page d-flex flex-column" style={{width:'500px', gap: '20px'}}>
                <div className="billing-info-details-item-page d-flex flex-row">
                  <div className="billing-info-details-label-page" style={{fontWeight:'600', fontSize: '16px', marginRight: '5px'}}>Full name:</div>
                  <div className="billing-info-details-value-page">{billingInfo.fullName}</div>
                </div>
                <div className="billing-info-details-item-page d-flex flex-row">
                  <div className="billing-info-details-label-page" style={{fontWeight:'600', fontSize: '16px', marginRight: '5px'}}>Billing address:</div>
                  <div className="billing-info-details-value-page">{billingInfo.street}, {billingInfo.city}, {billingInfo.zipCode}, {billingInfo.country}</div>
                </div>
                <div className="billing-info-details-item-page d-flex flex-row">
                  <div className="billing-info-details-label-page" style={{fontWeight:'600', fontSize: '16px', marginRight: '5px'}}>Company name:</div>
                  <div className="billing-info-details-value-page">{billingInfo.companyName}</div>
                </div>
            </div>
            <ActionButton text={"Edit +"} onClick={handleEditModalVisibility} className={"edit-billing-info-button"} ariaLabel={"Edit Billing Info"}/>
            </div>}

            {!showEditModal && !showBillingInfo && 
            <div className="billing-info-form-page">
            <FormGroup id={"full-name"} label={"Full name"} tooltipDescription={"Enter your full legal name as it appears on official documents."} type={"text"} value={fullName} onChange={handleFullNameChange} placeholder={"Enter full name"} ariaDescribedBy={"full-name-help"} onShowTooltip={handleShowFullNameTooltip} showTooltip={showFullNameTooltip} error={validationErrors.FullName || []} />
            <FormGroup id={"company-name"} label={"Company name"} tooltipDescription={"Enter the registered name of your company or organization."} type={"text"} value={companyName} onChange={handleCompanyNameChange} placeholder={"Enter company name"} ariaDescribedBy={"company-name-help"} onShowTooltip={handleShowCompanyNameTooltip} showTooltip={showCompanyNameTooltip} error={validationErrors.CompanyName || []} />
            <div className="counties-address" style={{display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '20px'}}>
                <SelectDropdown id="country" label="Country" options={populatedData} value={typeof selectedCountryId === "string" ? Number(selectedCountryId) || undefined : selectedCountryId} onChange={handleSelectedCountryId} getOptionLabel={(opt) => opt.name} getOptionValue={(opt) => typeof opt.id === "number" ? opt.id : (opt.id ? Number(opt.id) : undefined)} tooltipDescription={"Select your country to ensure accurate billing and tax information."} showTooltip={showCountryTooltip} ariaDescribedBy={"dropdown-help"} onShowTooltip={handleShowCountryTooltip} />
            <FormGroup id={"city"} label={"City"} tooltipDescription={"Enter the city where your billing address is located."} type={"text"} value={city} onChange={handleCityChange} placeholder={"Enter city"} ariaDescribedBy={"city-help"} onShowTooltip={handleShowCityTooltip} showTooltip={showCityTooltip} error={validationErrors.City || []} />
            <FormGroup id={"address"} label={"Address"} tooltipDescription={"Provide the full street address for billing purposes."} type={"text"} value={address} onChange={handleAddressChange} placeholder={"Enter address"} ariaDescribedBy={"address-help"} onShowTooltip={handleShowAddressTooltip} showTooltip={showAddressTooltip} error={validationErrors.Address || []} />
            <FormGroup id={"zip-code"} label={"Zip Code"} tooltipDescription={"Enter the postal or zip code for your billing address."} type={"text"} value={zipCode} onChange={handleZipCodeChange} placeholder={"Enter Zip Code"} ariaDescribedBy={"zip-code-help"} onShowTooltip={handleShowZipCodeTooltip} showTooltip={showZipCodeTooltip} error={validationErrors.ZipCode || []} />
            </div>
            <ActionButton text={"Save changes"} onClick={handleOnSaveChanges} className={"save-billing-info-button"} ariaLabel={"Save billing info button"} />
        </div>}

            {showEditModal &&
            <div className="billing-info-form">
            <FormGroup id={"full-name"} label={"Full name"} tooltipDescription={"Enter your full legal name as it appears on official documents."} type={"text"} value={fullName} onChange={handleFullNameChange} placeholder={"Enter full name"} ariaDescribedBy={"full-name-help"} onShowTooltip={handleShowFullNameTooltip} showTooltip={showFullNameTooltip} error={validationErrors.FullName || []} />
            <FormGroup id={"company-name"} label={"Company name"} tooltipDescription={"Enter the registered name of your company or organization."} type={"text"} value={companyName} onChange={handleCompanyNameChange} placeholder={"Enter company name"} ariaDescribedBy={"company-name-help"} onShowTooltip={handleShowCompanyNameTooltip} showTooltip={showCompanyNameTooltip} error={validationErrors.CompanyName || []} />
            <SelectDropdown id="country" label="Country" options={populatedData} value={typeof selectedCountryId === "string" ? Number(selectedCountryId) || undefined : selectedCountryId} onChange={handleSelectedCountryId} getOptionLabel={(opt) => opt.name} getOptionValue={(opt) => typeof opt.id === "number" ? opt.id : (opt.id ? Number(opt.id) : undefined)} tooltipDescription={"Select your country to ensure accurate billing and tax information."} showTooltip={showCountryTooltip} ariaDescribedBy={"dropdown-help"} onShowTooltip={handleShowCountryTooltip} />
            <FormGroup id={"city"} label={"City"} tooltipDescription={"Enter the city where your billing address is located."} type={"text"} value={city} onChange={handleCityChange} placeholder={"Enter city"} ariaDescribedBy={"city-help"} onShowTooltip={handleShowCityTooltip} showTooltip={showCityTooltip} error={validationErrors.City || []} />
            <FormGroup id={"address"} label={"Address"} tooltipDescription={"Provide the full street address for billing purposes."} type={"text"} value={address} onChange={handleAddressChange} placeholder={"Enter address"} ariaDescribedBy={"address-help"} onShowTooltip={handleShowAddressTooltip} showTooltip={showAddressTooltip} error={validationErrors.Address || []} />
            <FormGroup id={"zip-code"} label={"Zip Code"} tooltipDescription={"Enter the postal or zip code for your billing address."} type={"text"} value={zipCode} onChange={handleZipCodeChange} placeholder={"Enter Zip Code"} ariaDescribedBy={"zip-code-help"} onShowTooltip={handleShowZipCodeTooltip} showTooltip={showZipCodeTooltip} error={validationErrors.ZipCode || []} />
            <ActionButton text={"Save changes"} onClick={handleOnEdit} className={"save-billing-info-button"} ariaLabel={"Save billing info button"} />
        </div> }
        
    </div>
);

}