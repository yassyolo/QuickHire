import { Link, useNavigate } from "react-router-dom";
import { ActionButton } from "../../../../../Buttons/ActionButton/ActionButton";
import { ProfileDropdown } from "../Common/ProfileDropdown/ProfileDropdown";
import './SellerProfileDropdown.css';
import { useAuth } from "../../../../../../AuthContext";  

export function SellerProfileDropdown() {
  const { switchMode, logout } = useAuth();
  const navigate = useNavigate();

  const handleSwitchToBuying = () => {
    switchMode("buyer");
    navigate("/buyer"); 
  };

  const handleLogoutClick = async () => {
    await logout();
  };
  return (
    <ProfileDropdown>
      <ActionButton 
        text={"Switch to Buying"} 
        onClick={handleSwitchToBuying} 
        className={"switch-to-button"} 
        ariaLabel={"Switch to Buyer Button"} 
      />
      <div className="dropdown-divider"></div>
      <Link to="/seller/profile" className="dropdown-item" aria-label="Profile Link">Profile</Link>
      <Link to="/buyer/settings" className="dropdown-item" aria-label="Settings Link">Settings</Link>
      <Link to="/seller/billing-and-payment" className="dropdown-item" aria-label="Billing and Payments">Billing and payments</Link>
      <div className="dropdown-divider"></div>
<div 
        className="dropdown-item logout-button" 
        aria-label="Logout Button" 
        onClick={handleLogoutClick} 
        style={{background:"none", border:"none", padding:0, margin:0, cursor:"pointer", width:"100%", textAlign:"left"}}
      >
        Logout
      </div>    </ProfileDropdown>
  );
}
