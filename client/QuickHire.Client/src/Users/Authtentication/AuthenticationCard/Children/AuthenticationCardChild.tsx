import { useState } from "react";
import { AuthenticationCard } from "../AuthenticationCard";
import { FormGroup } from "../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../Shared/Forms/Common/Tooltips/Tooltip";
import axios from "axios";
import './AuthenticationCardChild.css';
import { IconActionButton } from "../../../../Shared/Buttons/IconActionButton/IconActionButton";
import { IconButton } from "../../../../Shared/Buttons/IconButton/IconButton";
import { PasswordCheckItem } from "../Common/PasswordCheck/PasswordCheckItem";
import { useAuth } from "../../../../AuthContext";
import { useNavigate, useSearchParams } from "react-router-dom";

export function AuthentionCardChild() {
    const [showSignInModal, setShowSignInModal] = useState(false);
    const [showRegisterModal, setShowRegisterModal] = useState(false);
    const [showRegisterOrSignInModal, setShowRegisterOrSignInModal] = useState(true);
    const [showEmailTooltip, handleEmailTooltip] = useTooltip();
    const [showThanksForRegisteringModal, handleThanksForRegisteringModal] = useState(false);
    const [showPasswordTooltip, handlePasswordTooltip] = useTooltip();
    const navigate = useNavigate();
    const [password, setPassword] = useState("");
    const [username, setUsername] = useState("");
    const [validationErrors, setValidationErrors] = useState<{ Email?: string[]; Password?: string[] }>({});
    const [email, setEmail] = useState("");
    const [searchParams] = useSearchParams();
    const redirectTo = searchParams.get("redirectTo");

    const { login } = useAuth();

    const handleEmailChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setEmail(e.target.value);
    };

    const handlePasswordChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setPassword(e.target.value);
    };

    const handleRegisterModalVisibility = () => {
        setShowRegisterModal(!showRegisterModal);
        setShowRegisterOrSignInModal(false);
    };

    const handleSignInModalVisibility = () => {
        setShowSignInModal(!showSignInModal);
        setShowRegisterOrSignInModal(false);
    };

    const handleBackToRegisterOrSignInModal = () => {
        setShowRegisterModal(false);
        setShowSignInModal(false);
        setShowRegisterOrSignInModal(true);
    };



    const handleRegistration = async () => {
        setValidationErrors({});
        try {
            const url = "https://localhost:7267/auth/register";
            const response = await axios.post(
                url,
                {
                    email,
                    password,
                },
                {
                    headers: {
                        Accept: "*/*",
                    },
                }
            );

            if (response.status === 200) {
                setShowRegisterModal(false);
                setEmail("");
                setPassword("");
                setUsername(response.data.username || "");
                handleThanksForRegisteringModal(true);
            }
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                setValidationErrors({
                    Email: error.response.data.errors?.Email || [],
                    Password: error.response.data.errors?.Password || [],
                });
            } else {
                console.error("Error during registration:", error);
            }
        }
    };

   const handleLogin = async () => {
  setValidationErrors({});
  try {
    await login(email, password); 
    setShowSignInModal(false);
    setPassword("");
    setShowRegisterOrSignInModal(false);
    console.log("email", email);
    if (email.includes("admin")){
        navigate("/admin/main-categories");
        return;
    }
        setUsername("");
        setEmail("");

    if (redirectTo) {
      navigate(redirectTo);
    } else {    
        navigate("/buyer");
    }
  } catch (error: unknown) {
    if (axios.isAxiosError(error) && error.response?.status === 400) {
      setValidationErrors({
        Email: error.response.data.errors?.Email || [],
        Password: error.response.data.errors?.Password || [],
      });
    } else {
      console.error("Error during login:", error);
    }
  }
};


    const handleContinueButtonDisability =
        !email || !password || Object.keys(validationErrors).length > 0;

    return (
        <AuthenticationCard>
            {showRegisterOrSignInModal && (
                <div className="create-an-account">
                    <div className="card-title-subtitle d-flex flex-column">
                        <div className="card-title">Create a new account</div>
                        <div className="card-subtitle">
                            Already have an account?{" "}
                            <span className="sign-in-button" style={{cursor: 'pointer'}} onClick={handleSignInModalVisibility}>
                                Sign in
                            </span>
                        </div>
                    </div>
                    <div className="card-button-choices d-flex flex-column">
                        <IconActionButton
                            icon={<i className="bi bi-envelope"></i>}
                            text="Continue with email"
                            onClick={handleRegisterModalVisibility}
                            ariaLabel="Continue with Email"
                        />
                    </div>
                </div>
            )}

            {showRegisterModal && (
                <div className="register-modal-show">
                    <div className="button-title-register d-flex flex-column">
                        <div className="d-flex flex-row">
                            <IconButton
                                icon={<i className="bi bi-arrow-left-short"></i>}
                                onClick={handleBackToRegisterOrSignInModal}
                                className={"back-to-register"}
                                ariaLabel={"Back to Sign in or register"}
                            />
                            <span className="back-span">Back</span>
                        </div>

                        <div className="card-title">Continue with your email</div>
                    </div>

                    <FormGroup
                        error={validationErrors.Email || []}
                        id="email"
                        label="Email"
                        tooltipDescription="Enter a valid email address that you have access to. It will be used for login and verification."
                        type="text"
                        value={email}
                        onChange={handleEmailChange}
                        placeholder="Enter your email"
                        ariaDescribedBy="email-help"
                        onShowTooltip={handleEmailTooltip}
                        showTooltip={showEmailTooltip}
                    />
                    <FormGroup
                        error={validationErrors.Password || []}
                        id="password"
                        label="Password"
                        tooltipDescription="Use at least 8 characters with a mix of letters, numbers, and symbols to keep your account secure."
                        type="password"
                        value={password}
                        onChange={handlePasswordChange}
                        showTooltip={showPasswordTooltip}
                        onShowTooltip={handlePasswordTooltip}
                        placeholder="Enter Password"
                        ariaDescribedBy="password-help"
                    />
                    <div className="password-check-items-list d-flex flex-column">
                        <PasswordCheckItem
                            isValid={password.length >= 8}
                            message="At least 8 characters"
                        />
                        <PasswordCheckItem
                            isValid={/[A-Z]/.test(password)}
                            message="At least one uppercase letter"
                        />
                        <PasswordCheckItem
                            isValid={/[a-z]/.test(password)}
                            message="At least one lowercase letter"
                        />
                        <PasswordCheckItem
                            isValid={/\d/.test(password)}
                            message="At least one number"
                        />
                    </div>
                    <button
                        className="continue-button-email"
                        onClick={handleRegistration}
                        disabled={handleContinueButtonDisability}
                    >
                        Continue
                    </button>
                </div>
            )}

            {showThanksForRegisteringModal && (
                <div className="thanks-for-registering-modal">
                    <h2 className="card-title">Welcome, {username}!</h2>
                    <p className="card-subtitle">
                        Thank you for signing up. Please check your email to verify your account and
                        get started!
                    </p>
                </div>
            )}

            {showSignInModal && (
                <div className="register-modal-show">
                    <div className="button-title-register d-flex flex-column">
                        <div className="d-flex flex-row">
                            <IconButton
                                icon={<i className="bi bi-arrow-left-short"></i>}
                                onClick={handleBackToRegisterOrSignInModal}
                                className={"back-to-register"}
                                ariaLabel={"Back to Sign in or register"}
                            />
                            <span className="back-span">Back</span>
                        </div>
                        <div className="card-title">Use your email or username</div>

<FormGroup
  error={validationErrors.Email || []}
  id="email"
  label="Email or Username"
  tooltipDescription="Enter the email address or username associated with your account to log in."
  type="text"
  value={email}
  onChange={handleEmailChange}
  placeholder="Email or username"
  ariaDescribedBy="email-help"
  onShowTooltip={handleEmailTooltip}
  showTooltip={showEmailTooltip}
/>

<FormGroup
  error={validationErrors.Password || []}
  id="password"
  label="Password"
  tooltipDescription="Enter your account password. Passwords are case-sensitive."
  type="password"
  value={password}
  onChange={handlePasswordChange}
  onShowTooltip={handlePasswordTooltip}
    showTooltip={showPasswordTooltip}
  placeholder="Password"
  ariaDescribedBy="password-help"
/>
                        <button
                            className="continue-button-email"
                            onClick={handleLogin}
                            disabled={handleContinueButtonDisability}
                        >
                            Continue
                        </button>
                    </div>
                </div>
            )}
        </AuthenticationCard>
    );
}


