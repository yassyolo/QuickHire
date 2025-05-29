import { Link } from "react-router-dom";
import { ProfileDropdown } from "./ProfileDropdown";
import './SellerProfileDropdown.css';

export function AdminProfileDropdown() {
    return (
        <ProfileDropdown >
           <Link to="/logout" className="dropdown-item" aria-label="Logout Link">Logout</Link>
        </ProfileDropdown>
    );
}