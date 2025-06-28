import { useEffect, useState } from "react";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { SelectDropdown } from "../../../../../Shared/Dropdowns/Select/SelectDropdown";
import axios from "../../../../../axiosInstance";
import { Item } from "../../../../../Shared/Dropdowns/Common/PopulateDropdown";
import './AccountSettings.css';
import { isAxiosError } from "axios";
import { useNavigate } from "react-router-dom";
interface AccountSettingsProps {
    onSaveChanges: () => void;
}

export function AccountSettings({onSaveChanges}: AccountSettingsProps) {
    const [fullName, setFullName] = useState<string>("");
    const navigate = useNavigate();

    const [username, setUsername] = useState<string>("");
    const [city, setCity] = useState<string>("");
    const [address, setAddress] = useState<string>("");
    const [zipCode, setZipCode] = useState<string>("");
    const [showUsernameTooltip, handleShowUsernameTooltip] = useTooltip();
    const [showCountryTooltip, handleShowCountryTooltip] = useTooltip();
    const [showAddressTooltip, handleShowAddressTooltip] = useTooltip();
    const [showZipCodeTooltip, handleShowZipCodeTooltip] = useTooltip();
    const [showCityTooltip, handleShowCityTooltip] = useTooltip();
    const [showFullNameTooltip, handleShowFullNameTooltip] = useTooltip();
    const [email, setEmail] = useState<string>("");
    const [showEmailTooltip, handleShowEmailTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ FullName?: string[]; Username?: string[]; City?:  string[]; Address?: string[]; ZipCode?: string[]; Email?: string[]; }>({});
    const [populatedData, setPopulatedData] = useState<Item[]>([]);
        const [selectedCountryId, setSelectedCountryId] = useState<number | undefined>(undefined);

    useEffect(() => {
        setFullName("");
        setCity("");
        setAddress("");
        setZipCode("");
        setUsername("");    
        setValidationErrors({});
        setEmail("");
        setSelectedCountryId(undefined);
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

    const handleEmailChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setEmail(event.target.value);
    }

    const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setUsername(event.target.value);
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
        const data = {
            FullName: fullName,
            Username: username,
            City: city,
            Street: address,
            ZipCode: zipCode,
            Email: email,
            CountryId: selectedCountryId

        };

        try {
            const url = "https://localhost:7267/buyers/profile/edit";
            await axios.put(url, data);
            navigate("/buyer/profile");

            onSaveChanges();
        } catch (error) {
            if (isAxiosError(error)) {
                setValidationErrors(error.response?.data || {});
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    }

        return (
        <div className="account-settings-container">
            <div className="account-settings-form">
                <FormGroup 
                    id={"full-name"} 
                    label={"Full name"} 
                    tooltipDescription={"Enter your full legal name as it appears on official documents."} type={"text"} 
                    value={fullName}  onChange={handleFullNameChange}  ariaDescribedBy={"full-name-help"} 
                    onShowTooltip={handleShowFullNameTooltip} 
                    showTooltip={showFullNameTooltip} 
                    error={validationErrors.FullName || []} 
                />

                <FormGroup 
                    id={"email"} 
                    label={"Email"} tooltipDescription={"Enter your personal email."} 
                    type={"text"} 
                    value={email} onChange={handleEmailChange} ariaDescribedBy={"email-help"} 
                    onShowTooltip={handleShowEmailTooltip} 
                    showTooltip={showEmailTooltip} 
                    error={validationErrors.Email || []} 
                />
                 <FormGroup id={"username"} label={"Username"} tooltipDescription={"Write a short, catchy description that grabs attention."} type={"text"} value={username} onChange={handleUsernameChange} ariaDescribedBy={"username-help"} onShowTooltip={handleShowUsernameTooltip} showTooltip={showUsernameTooltip} error={validationErrors.Username || []} />

                <div className="address-forms">
                    <SelectDropdown
                    id="country"
                    label="Country"
                    options={populatedData} value={typeof selectedCountryId === "string" ? Number(selectedCountryId) || undefined : selectedCountryId}
                    onChange={handleSelectedCountryId} getOptionLabel={(opt) => opt.name}
                    getOptionValue={(opt) => typeof opt.id === "number" ? opt.id : (opt.id ? Number(opt.id) : undefined)}
                    tooltipDescription={"Select your country to ensure accurate billing and tax information."} showTooltip={showCountryTooltip}
                    ariaDescribedBy={"dropdown-help"}
                    onShowTooltip={handleShowCountryTooltip}
                />
                <FormGroup 
                    id={"city"} 
                    label={"City"} 
                    tooltipDescription={"Enter the city where your billing address is located."}  type={"text"} 
                    value={city} 
                    onChange={handleCityChange} ariaDescribedBy={"city-help"}  onShowTooltip={handleShowCityTooltip} 
                    showTooltip={showCityTooltip} 
                    error={validationErrors.City || []} 
                />
                <FormGroup 
                    id={"address"} 
                    label={"Address"} 
                    tooltipDescription={"Provide the full street address for billing purposes."} 
                    type={"text"}  value={address}  onChange={handleAddressChange} 
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
                    value={zipCode} onChange={handleZipCodeChange} 
                    ariaDescribedBy={"zip-code-help"} 
                    onShowTooltip={handleShowZipCodeTooltip} 
                    showTooltip={showZipCodeTooltip} 
                    error={validationErrors.ZipCode || []} 
                />
                </div>
                <ActionButton 
                    text={"Save changes"} 
                    onClick={handleOnSaveChanges}  className={"save-billing-info-button"} 
                    ariaLabel={"Save billing info button"} 
                />
            </div>
        </div>
    );
}