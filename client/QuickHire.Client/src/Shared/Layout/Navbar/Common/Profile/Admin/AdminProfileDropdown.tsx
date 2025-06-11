import { ProfileDropdown } from "../Common/ProfileDropdown/ProfileDropdown";
import '../Seller/SellerProfileDropdown.css';
import { useAuth } from "../../../../../../AuthContext";

export function AdminProfileDropdown() {
              const { logout } = useAuth();
const handleLogoutClick = async () => {
    await logout();
}
    return (        
        <ProfileDropdown >
<div 
        className="dropdown-item logout-button" 
        aria-label="Logout Button" 
        onClick={handleLogoutClick} 
        style={{background:"none", border:"none", padding:0, margin:0, cursor:"pointer", width:"100%", textAlign:"left"}}
      >
        Logout
      </div>        </ProfileDropdown>
    );
}