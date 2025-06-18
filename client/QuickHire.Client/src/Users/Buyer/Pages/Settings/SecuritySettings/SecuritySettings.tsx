import { useEffect, useState } from "react";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { PasswordCheckItem } from "../../../../Authtentication/AuthenticationCard/Common/PasswordCheck/PasswordCheckItem";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import './SecuritySettings.css';
import axios from "../../../../../axiosInstance";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { isAxiosError } from "axios";
import { useNavigate } from "react-router-dom";
interface SecuritySettingsProps {
    onSaveChanges: () => void;
}

export function SecuritySettings({onSaveChanges}: SecuritySettingsProps) {
    const [newPassword, setNewPassword] = useState<string>("");
    const [confirmPassword, setConfirmPassword] = useState<string>("");
    const [showPasswordTooltip, handleShowPasswordTooltip] = useTooltip();
    const [showConfirmPasswordTooltip, handleShowConfirmPasswordTooltip] = useTooltip();
    const navigate = useNavigate();

    const [validationErrors, setValidationErrors] = useState<{ NewPassword?: string[], ConfirmPassword?: string[]; }>({});

    useEffect(() => {
        setNewPassword("");
        setConfirmPassword("");
    }
    , []);

    const handleNewPasswordChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setNewPassword(event.target.value);
    }

    const handleConfirmPasswordChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setConfirmPassword(event.target.value);
    }

    const handleOnSaveChanges = () => {
        try{
            setValidationErrors({});
            const url = "https://localhost:7267/auth/change-password";
            axios.post(url, {
                newPassword,
                confirmPassword
            });
                        navigate("/buyer/profile");
            onSaveChanges();
        } catch (error: unknown) {
            if (isAxiosError(error)) {
                const response = error.response;
                if (response && response.status === 400 && response.data) {
                        setValidationErrors({
                            NewPassword: response.data.errors?.NewPassword || [],
                            ConfirmPassword: response.data.errors?.ConfirmPassword || [],
                        });
                    }  else {
                    console.error("An unexpected error occurred:", error);
                }
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
        
    }


    return(
        
        <div className="security-settings-container">
            <FormGroup showTooltip={showPasswordTooltip} onShowTooltip={handleShowPasswordTooltip} error={validationErrors.NewPassword || []} id="password" label="New password"   tooltipDescription="Use at least 8 characters with a mix of letters, numbers, and symbols to keep your account secure." type="password" value={newPassword} onChange={handleNewPasswordChange} ariaDescribedBy="password-help"/>
{newPassword &&
                                    <div className="password-check-items-list d-flex flex-column">
                                        <PasswordCheckItem isValid={newPassword.length >= 8} message="At least 8 characters" />                     
                                        <PasswordCheckItem isValid={/[A-Z]/.test(newPassword)} message="At least one uppercase letter" />
                                        <PasswordCheckItem isValid={/[a-z]/.test(newPassword)} message="At least one lowercase letter" />
                                        <PasswordCheckItem isValid={/\d/.test(newPassword)} message="At least one number" />
                                    </div>}
            <FormGroup showTooltip={showConfirmPasswordTooltip} onShowTooltip={handleShowConfirmPasswordTooltip}  error={validationErrors.ConfirmPassword || []} id="password" label="Confirm password"   tooltipDescription="Confirm your new password." type="password" value={confirmPassword} onChange={handleConfirmPasswordChange} ariaDescribedBy="password-help"/>
                <ActionButton  text={"Save changes"} onClick={handleOnSaveChanges}  className={"save-billing-info-button"} 
                    ariaLabel={"Save billing info button"} 
                />
        </div>
    )
}