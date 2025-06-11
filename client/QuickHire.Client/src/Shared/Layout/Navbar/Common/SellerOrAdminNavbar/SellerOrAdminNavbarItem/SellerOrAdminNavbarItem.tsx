import { NavLink } from 'react-router-dom';
import './SellerOrAdminNavbarItem.css';
import { useState } from 'react';

interface SellerNavbarItemProps {
  to?: string;
  label: string;
  hasDropdown?: boolean;
}


export function SellerOrAdminNavbarItem ({ to, label, hasDropdown}: SellerNavbarItemProps) {
  const [dropdownVisible, setDropdownVisible] = useState(false);
  const handleDropdownVisibility = () => {
    setDropdownVisible(!dropdownVisible);
  };
  return (
    <li className="nav-item">
      {!hasDropdown  && to ? ( <NavLink to={to} className="nav-link"> {label} </NavLink>) : (
        <NavLink to={to ?? '#'} className="nav-link dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false" onClick={handleDropdownVisibility}>
          {label}
        </NavLink>
      )}
      {hasDropdown && dropdownVisible && <ul className="seller-dropdown-menu">
        <NavLink to="/seller/orders" className="seller-dropdown-item">Orders</NavLink>
        <div className="dropdown-divider"></div>
        <NavLink to="/seller/gigs" className="seller-dropdown-item">Gigs</NavLink>
        <NavLink to="/seller/project-briefs" className="seller-dropdown-item">Projects</NavLink>
        <div className="dropdown-divider"></div>
        <NavLink to="/seller/profile" className="seller-dropdown-item">Profile</NavLink>
        <NavLink to="/seller/earnings" className="seller-dropdown-item">Earnings</NavLink>

        </ul>}
     

    </li>
  );
};
