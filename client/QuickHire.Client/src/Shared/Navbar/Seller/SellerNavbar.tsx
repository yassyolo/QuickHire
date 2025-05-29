import { Outlet } from 'react-router-dom';
import { SellerOrAdminNavbarItem } from '../Common/SellerOrAdminNavbarItem';
import { SellerOrAdminNavbar } from '../Common/SellerOrAdminNavbar';
import { IconButton } from '../../Buttons/IconButton';
import { NotificationButtonDropdown } from '../../Notifications/NotificationButtonDropdown';
import { ProfileIconDropdown } from '../../Profile/ProfileIconDropdown';
import { SellerProfileDropdown } from '../../Profile/SellerProfileDropdown';
import { Footer } from '../../Footer/Footer';
import './SellerNavbar.css';

export function SellerNavbar() {
  return (
    <div className="page-container">
        <SellerOrAdminNavbar  navLinks={<><SellerOrAdminNavbarItem to="/seller/dashboard" label="Dashboard" /><SellerOrAdminNavbarItem to="/admin/my-business" label="My Business" /><SellerOrAdminNavbarItem to="/seller/analytics" label="Analytics" /></>}
         navIcons={
            <>
                <NotificationButtonDropdown/>
                <IconButton buttonInfo="Messages" icon={<i className="fa-regular fa-envelope"></i>} onClick={() => {}} className={"inbox-button"} ariaLabel={"MessagesButton"}></IconButton>
                <ProfileIconDropdown><SellerProfileDropdown /></ProfileIconDropdown>
            </>}>      
          
        </SellerOrAdminNavbar>

        <main><Outlet /></main>
        <div className="seller-footer"><Footer/></div>
    </div>
  );
}
