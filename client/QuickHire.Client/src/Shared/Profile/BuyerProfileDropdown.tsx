import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { ActionButton } from "../Buttons/ActionButton/ActionButton";
import { ProfileDropdown } from "./ProfileDropdown";
import './SellerProfileDropdown.css';
import { useAuth } from "../../AuthContext";  

export function BuyerProfileDropdown() {
  const { switchMode, logout } = useAuth();  // <-- get logout
  const navigate = useNavigate();

  const handleSwitchToSelling = () => {
    switchMode("seller");
    navigate("/seller/dashboard");
  };

  const handleLogoutClick = async () => {
    await logout();
  };

  return (
    <ProfileDropdown>
      <ActionButton 
        text={"Switch to Selling"} 
        onClick={handleSwitchToSelling} 
        className={"switch-to-button"} 
        ariaLabel={"Switch to Seller Button"} 
      />
      <div className="dropdown-divider"></div>
      <Link to="/buyer/profile" className="dropdown-item" aria-label="Profile Link">Profile</Link>
      <Link to="/buyer/browsing-history" className="dropdown-item" aria-label="Browsing history">Browsing history</Link>
      <Link to="/buyer/project-briefs" className="dropdown-item" aria-label="Post a Project Brief Link">Post a project brief</Link>
      <Link to="/buyer/project-briefs/me" className="dropdown-item" aria-label="My Project Briefs Link">Your briefs</Link>
      <Link to="/seller/dashboard" className="dropdown-item" aria-label="My Seller Dashboard">Dashboard</Link>
      <div className="dropdown-divider"></div>
      <Link to="/buyer/settings" className="dropdown-item" aria-label="Settings Link">Settings</Link>
      <Link to="/buyer/billing-and-payment" className="dropdown-item" aria-label="Billing and Payments">Billing and payments</Link>
      <div className="dropdown-divider"></div>
      <div 
        className="dropdown-item logout-button" 
        aria-label="Logout Button" 
        onClick={handleLogoutClick} 
        style={{background:"none", border:"none", padding:0, margin:0, cursor:"pointer", width:"100%", textAlign:"left"}}
      >
        Logout
      </div>
    </ProfileDropdown>
  );
}
