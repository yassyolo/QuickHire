import { NavLink } from 'react-router-dom';
import './SellerOrAdminNavbarItem.css';

interface SellerNavbarItemProps {
  to: string;
  label: string;
}

export function SellerOrAdminNavbarItem ({ to, label }: SellerNavbarItemProps) {
  return (
    <li className="nav-item">
      <NavLink to={to} className="nav-link"> {label} </NavLink>
    </li>
  );
};
