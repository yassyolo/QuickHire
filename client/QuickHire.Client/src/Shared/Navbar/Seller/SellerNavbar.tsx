import { Outlet, useNavigate } from 'react-router-dom';
import { SellerOrAdminNavbarItem } from '../Common/SellerOrAdminNavbarItem';
import { SellerOrAdminNavbar } from '../Common/SellerOrAdminNavbar';
import { IconButton } from '../../Buttons/IconButton/IconButton';
import { NotificationButtonDropdown } from '../../../Users/Notifications/NotificationDropdown/NotificationButtonDropdown';
import { ProfileIconDropdown } from '../../Profile/ProfileIconDropdown';
import { SellerProfileDropdown } from '../../Profile/SellerProfileDropdown';
import { Footer } from '../../Footer/Footer';
import './SellerNavbar.css';

export function SellerNavbar() {
  const navigate = useNavigate();
  const handleShowinbox = () => {
    navigate('/seller/inbox');
  };
  return (
    <div className="page-container">
        <SellerOrAdminNavbar  navLinks={<><SellerOrAdminNavbarItem to="/seller/dashboard" label="Dashboard" /><SellerOrAdminNavbarItem hasDropdown={true} label="My Business" /><SellerOrAdminNavbarItem to="/seller/analytics" label="Analytics" /></>}
         navIcons={
            <>
                <NotificationButtonDropdown buyer={false}/>
                <IconButton buttonInfo="Messages" icon={<i className="fa-regular fa-envelope"></i>} onClick={handleShowinbox} className={"inbox-button"} ariaLabel={"MessagesButton"}></IconButton>
                <ProfileIconDropdown><SellerProfileDropdown /></ProfileIconDropdown>
            </>}>      
          
        </SellerOrAdminNavbar>

        <main className="seller-main"><Outlet /></main>
        <div className="seller-footer"><Footer/></div>
    </div>
  );
}
