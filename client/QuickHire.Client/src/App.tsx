import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css'
import { MainCategories } from './Admin/Pages/MainCategories'
import { SubCategories } from './Admin/Pages/SubCategories'
import { SubSubCategories } from './Admin/Pages/SubSubCategories'
import { SubCategoryDetails } from './Admin/Components/Details/SubCategoryDetails'
import { Gigs } from './Admin/Pages/Gigs'
import { GigDetailsPage } from './Admin/Components/Details/GigDetailsPage'
import { AdminNavbar } from './Shared/Navbar/Admin/AdminNavbar'
import { SubSubCategoryDetails } from './Admin/Components/Details/SubSubCategoryDetails'
import { Users } from './Admin/Pages/Users'
import { UserDetails } from './Admin/Components/Details/UserDetails'
import { MainCategoryDetails } from './Admin/Components/Details/MainCategoryDetails'
import { MainCategoryPage } from './Shared/Categories/MainCategories/MainCategoryPage'
import { AuthentionCardChild } from './Users/Authtentication/AuthenticationCardChild'
import { BillingAndPaymentsPage } from './Users/BillingAndPayments/BillingAndPaymentsPage'
import { SellerNavbar } from './Shared/Navbar/Seller/SellerNavbar'
import { SettingsPage } from './Users/Settings/SettingsPage'
import { BuyerNavbar } from './Shared/Navbar/Buyer/BuyerNavbar'
import { SearchProvider } from './Shared/Navbar/Buyer/SearchContext'
import { SellerDetails } from './Users/Seller/Pages/SellerDetails'

export function App() {
  return (
    <SearchProvider>
      <Router>
        <Routes>
          <Route path="/admin" element={<AdminNavbar />}>
            <Route path="users" element={<Users />} />
            <Route path="users/:id" element={<UserDetails />} />
            <Route path="main-categories" element={<MainCategories />} />
            <Route path="sub-categories" element={<SubCategories />} />
            <Route path="gigs" element={<Gigs />} />
            <Route path="gigs/:id" element={<GigDetailsPage />} />
            <Route path="sub-sub-categories" element={<SubSubCategories />} />
            <Route path="main-categories/:id" element={<MainCategoryDetails />} />
            <Route path="sub-categories/:id" element={<SubCategoryDetails />} />
            <Route path="sub-sub-categories/:id" element={<SubSubCategoryDetails />} />
          </Route>

          <Route path="/seller" element={<SellerNavbar />}>
            <Route path="billing-and-payment" element={<BillingAndPaymentsPage homeUrl={"seller"} />} />
            <Route path="profile" element={<SellerDetails />} />
          </Route>

          <Route path="/buyer" element={<BuyerNavbar />}>
            <Route path="settings" element={<SettingsPage />} />
            <Route path="billing-and-payment" element={<BillingAndPaymentsPage homeUrl={"buyer"} />} />
            <Route path="main-categories/:id" element={<MainCategoryPage />} />
          </Route>

          <Route path="/users/login" element={<AuthentionCardChild />} />
          <Route path="*" element={<div>Page Not Found</div>} />
        </Routes>
      </Router>
    </SearchProvider>
  );
}



