import { ReactNode } from 'react';
import './SellerOrAdminNavbar.css';
import { Logo } from '../../Logo/Logo';

interface NavbarProps {
  navLinks: ReactNode;
  navIcons: ReactNode;
}

export function SellerOrAdminNavbar({ navLinks, navIcons }: NavbarProps) {
  return (
    <nav className={`seller-navbar`}>
      <div className="navbar-left">
        <div className="logo">
            <Logo/>
        </div>
        <ul className="nav">{navLinks}</ul>
      </div>
      <div className="navbar-right"> {navIcons} </div>
    </nav>
  );
}
