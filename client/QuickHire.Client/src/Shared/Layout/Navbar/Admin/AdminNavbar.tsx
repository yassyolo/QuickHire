import { Outlet } from 'react-router-dom';
import { SellerOrAdminNavbarItem } from '../Common/SellerOrAdminNavbar/SellerOrAdminNavbarItem/SellerOrAdminNavbarItem';
import { SellerOrAdminNavbar } from '../Common/SellerOrAdminNavbar/SellerOrAdminNavbar';
import { ProfileIconDropdown } from '../Common/Profile/Common/ProfileDropdown/ProfileIconDropdown';
import { AdminProfileDropdown } from '../Common/Profile/Admin/AdminProfileDropdown';
import './AdminNavbar.css';

export function AdminNavbar() {
  return (
    <div className="page-container">
        <SellerOrAdminNavbar 
        navLinks={<><SellerOrAdminNavbarItem to="/admin/users" label="Users" /><SellerOrAdminNavbarItem to="/admin/gigs" label="Gigs" /><SellerOrAdminNavbarItem to="/admin/main-categories" label="Main Categories" /><SellerOrAdminNavbarItem to="/admin/sub-categories" label="Sub Categories" /><SellerOrAdminNavbarItem to="/admin/sub-sub-categories" label="Sub Sub Categories" /></>}

         navIcons={<> 
         <ProfileIconDropdown> <AdminProfileDropdown/>  </ProfileIconDropdown></>}>      
          
        </SellerOrAdminNavbar>
        <main className="admin-main"> <Outlet /> </main>
        <div className="buyer-footer"> <footer><div className="footer-bottom">
                <p>© 2025 QuickHire. All rights reserved.</p>
            </div></footer></div>
    </div>
  );
}
