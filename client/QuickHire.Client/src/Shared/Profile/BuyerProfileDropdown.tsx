import { Link } from "react-router-dom";
import { ActionButton } from "../Buttons/ActionButton";
import { ProfileDropdown } from "./ProfileDropdown";
import './SellerProfileDropdown.css';

export function BuyerProfileDropdown() {
    return (
        <ProfileDropdown >
<ActionButton text={"Switch to Selling"} onClick={() => {}} className={"switch-to-button"} ariaLabel={"Switch to Buyer Button"}></ActionButton>
<div className="dropdown-divider"></div>
<Link to="/buyer/profile" className="dropdown-item" aria-label="Profile Link">Profile</Link>
<Link to="/buyer/browsing-history" className="dropdown-item" aria-label="Browsing history">Browsing history</Link>

<Link to="/buyer/project-briefs" className="dropdown-item" aria-label="Post a Project Brief Link">Post a project brief</Link>
<Link to="/buyer/project-briefs/me" className="dropdown-item" aria-label="My Project Briefs Link">Your briefs</Link>
<Link to="/seller/dashboard" className="dropdown-item" aria-label="My Seller Dashboard">Dasboard</Link>

<div className="dropdown-divider"></div>
<Link to="/buyer/settings" className="dropdown-item" aria-label="Settings Link">Settings</Link>
<Link to="/buyer/billing-and-payment" className="dropdown-item" aria-label="Billing and Payments">Billing and payments</Link>
<div className="dropdown-divider"></div>
<Link to="/logout" className="dropdown-item" aria-label="Logout Link">Logout</Link>
        </ProfileDropdown>
    );
}