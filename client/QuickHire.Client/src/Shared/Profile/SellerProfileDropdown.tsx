import { Link } from "react-router-dom";
import { ActionButton } from "../Buttons/ActionButton";
import { ProfileDropdown } from "./ProfileDropdown";
import './SellerProfileDropdown.css';

export function SellerProfileDropdown() {
    return (
    <ProfileDropdown >
        <ActionButton text={"Switch to Buying"} onClick={() => {}} className={"switch-to-button"} ariaLabel={"Switch to Buyer Button"}></ActionButton>
        <div className="dropdown-divider"></div>
        <Link to="/seller/profile" className="dropdown-item" aria-label="Profile Link">Profile</Link>
        <Link to="/buyer/settings" className="dropdown-item" aria-label="Settings Link">Settings</Link>
        <Link to="/seller/billing-and-payment" className="dropdown-item" aria-label="Billing and Payments">Billing and payments</Link>
        <div className="dropdown-divider"></div>
        <Link to="/logout" className="dropdown-item" aria-label="Logout Link">Logout</Link>
    </ProfileDropdown>
    );
}