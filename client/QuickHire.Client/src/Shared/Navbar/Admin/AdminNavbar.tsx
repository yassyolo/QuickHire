import { Outlet } from 'react-router-dom';
import { SellerOrAdminNavbarItem } from '../Common/SellerOrAdminNavbarItem';
import { SellerOrAdminNavbar } from '../Common/SellerOrAdminNavbar';
import { Footer } from '../../Footer/Footer';
import { NotificationButtonDropdown } from '../../Notifications/NotificationButtonDropdown';
import { ProfileIconDropdown } from '../../Profile/ProfileIconDropdown';
import { AdminProfileDropdown } from '../../Profile/AdminProfileDropdown';
import './AdminNavbar.css';

export function AdminNavbar() {
  return (
    <div className="page-container">
        <SellerOrAdminNavbar 
        navLinks={<><SellerOrAdminNavbarItem to="/admin/users" label="Users" /><SellerOrAdminNavbarItem to="/admin/gigs" label="Gigs" /><SellerOrAdminNavbarItem to="/admin/main-categories" label="Main Categories" /><SellerOrAdminNavbarItem to="/admin/sub-categories" label="Sub Categories" /><SellerOrAdminNavbarItem to="/admin/sub-sub-categories" label="Sub Sub Categories" /></>}

         navIcons={<>
         <NotificationButtonDropdown/>

         <ProfileIconDropdown>
            <AdminProfileDropdown/>
          </ProfileIconDropdown>
          </>}>      
          
        </SellerOrAdminNavbar>
        <main> <Outlet /> </main>

        <Footer></Footer>
    </div>
  );
}
