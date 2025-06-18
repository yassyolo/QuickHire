import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import './App.css'
import { MainCategories } from './Admin/Pages/MainCategories/MainCategories'
import { SubCategories } from './Admin/Pages/SubCategories/SubCategories'
import { SubSubCategories } from './Admin/Pages/SubSubCategories/SubSubCategories'
import { SubCategoryDetails } from './Admin/Pages/Details/Categories/SubCategory/SubCategoryDetails'
import { Gigs } from './Admin/Pages/Gigs/Gigs'
import { AdminNavbar } from './Shared/Layout/Navbar/Admin/AdminNavbar'
import { SubSubCategoryDetails } from './Admin/Pages/Details/Categories/SubSubCategory/SubSubCategoryDetails'
import { Users } from './Admin/Pages/Users/Users'
import { MainCategoryDetails } from './Admin/Pages/Details/Categories/MainCategory/MainCategoryDetails'
import { MainCategoryPage } from './Users/Buyer/Pages/MainCategoryPage/MainCategoryPage'
import { AuthentionCardChild } from './Users/Authtentication/AuthenticationCard/Children/AuthenticationCardChild'
import { BillingAndPaymentsPage } from './Users/Buyer/Pages/BillingsAndPayments/BillingAndPaymentsPage'
import { SellerNavbar } from './Shared/Layout/Navbar/Seller/SellerNavbar'
import { SettingsPage } from './Users/Buyer/Pages/Settings/SettingsPage'
import { BuyerNavbar } from './Shared/Layout/Navbar/Buyer/BuyerNavbar'
import { SearchProvider } from './Shared/Layout/Navbar/Buyer/SearchContext'
import { BrowsingHistory } from './Users/Buyer/Pages/BuyerBrowsingHistory/BrowsingHistoryPage/BrowsingHistory'
import { FavouritesPage } from './Users/Buyer/Pages/FavouritesLists/FavouritesPage'
import { SellerDashboard } from './Users/Seller/Pages/SellerDashboard/SellerDashboard'
import { SellerAnalytics } from './Users/Seller/Pages/SellerAnalytics/SellerAnalytics'
import { SellerProfile } from './Users/Seller/Pages/SellerProfile/SellerProfile'
import { SellerEarnings } from './Users/Seller/Pages/SellerEarnings/SellerEarnings'
import { BuyerProfilePage } from './Users/Buyer/Pages/BuyerProfile/BuyerProfilePage'
import { FavouriteListPage } from './Users/Buyer/Pages/FavouriteItems/FavouriteListPage'
import { NewSellerForm } from './Users/Seller/Pages/NewSeller/Form/NewSellerForm'
import { GigDetailsForAdmin } from './Admin/Pages/Details/Gigs/GigDetailsPage'
import { GigDetailsPage } from './Gigs/Pages/GigDetails/GigDetails/GigDetailsPage'
import { SellerGigs } from './Users/Seller/Pages/SellerGigs/SellerGigs'
import { SellerOrders } from './Users/Seller/Pages/SellerOrders/SellerOrders'
import { SellerProjectBriefs } from './Users/Seller/Pages/SellerProjectBriefs/SellerProjectBriefs'
import { AuthProvider } from './AuthContext'
import { InboxPage } from './Users/Messaging/InboxPage/InboxPage'
import GoogleLoginRedirect from './Users/Authtentication/GoogleLoginRetirect'
import { MyProjectBriefs } from './Users/Buyer/Pages/BuyerProjectBriefs/MyProjectBriefs.tsx/MyProjectBriefs'
import { UserDetailsForAdmin } from './Admin/Pages/Details/Users/UserDetailsPage'
import { FrontPage } from './Users/Buyer/Pages/FirstPage/FrontPage'
import { NotAuthenticatedNavbar } from './Shared/Layout/Navbar/NotAuthenticated/NotAuthenticatedNavbar'
import { NotAuthenticatedPage } from './Users/NotAuthenticated/Pages/NotAuthenticatedPage'
import { SubSubCategoryPage } from './Users/Buyer/Pages/SubSubCategory/SubSubCategoryPage'
import { PostProjectBrief } from './Users/Buyer/Pages/BuyerProjectBriefs/PostProjectBrief/PostProjectBrief'
import { NewGigForm } from './Users/Seller/Pages/NewGig/Form/NewGigForm'
import { SubCategoryPage } from './Users/Buyer/Pages/SubCategoryPage/SubCategoryPage'
import { BuyerSearchResultsPage } from './Users/Buyer/Pages/SearchResults/BuyerSearchResults'
import { OrderDetails } from './Orders/Pages/OrderDetails/OrderDetails'
import { SellerDetailsPage } from './Users/Buyer/Pages/SellerDetailsPage/SellerDetailsPage'

export function App() {
  return (
          <Router>
    <SearchProvider>

    <AuthProvider>
        <Routes>
          <Route path="/admin" element={<AdminNavbar />}>
            <Route path="users" element={<Users />} />
            <Route path="users/:id" element={<UserDetailsForAdmin />} />
            <Route path="main-categories" element={<MainCategories />} />
            <Route path="sub-categories" element={<SubCategories />} />
            <Route path="gigs" element={<Gigs />} />
            <Route path="gigs/:id" element={<GigDetailsForAdmin />} />
            <Route path="sub-sub-categories" element={<SubSubCategories />} />
            <Route path="main-categories/:id" element={<MainCategoryDetails />} />
            <Route path="sub-categories/:id" element={<SubCategoryDetails />} />
            <Route path="sub-sub-categories/:id" element={<SubSubCategoryDetails />} />
          </Route>

          <Route path="/seller" element={<SellerNavbar />}>
            <Route path="billing-and-payment" element={<BillingAndPaymentsPage homeUrl={"seller"} buyer={false} />} />
            <Route path="profile" element={<SellerProfile />} />
            <Route path="dashboard" element={<SellerDashboard />} />
            <Route path="analytics" element={<SellerAnalytics />} />
            <Route path="earnings" element={<SellerEarnings />} />
            <Route path="new" element={<NewSellerForm/>} />
            <Route path="gigs" element={<SellerGigs />} />
            <Route path="orders" element={<SellerOrders />} />
            <Route path="project-briefs" element={<SellerProjectBriefs/>} />
            <Route path="inbox" element={<InboxPage />} />
            <Route path="gigs/new" element={<NewGigForm />} />
            <Route path="orders/:id" element={<OrderDetails />} />
          </Route>

          <Route path="/buyer" element={<BuyerNavbar />}>
            <Route path="settings" element={<SettingsPage homeUrl={'buyer'} />} />
            <Route path="billing-and-payment" element={<BillingAndPaymentsPage homeUrl={"buyer"} buyer={true} />} />
            <Route path="main-categories/:id" element={<MainCategoryPage />} />
            <Route path="browsing-history" element={<BrowsingHistory />} />
            <Route path="favourite-gigs" element={<FavouritesPage />} />
            <Route path="favourite-gigs/:id" element={<FavouriteListPage />} />
            <Route path="seller/:id" element={<SellerDetailsPage />} />
            <Route path="gigs/:id" element={<GigDetailsPage />} />
            <Route path="project-briefs" element={<PostProjectBrief/>} />
            <Route path="my-project-briefs" element={<MyProjectBriefs />} />
            <Route path="profile" element={<BuyerProfilePage />} />
            <Route path="inbox" element={<InboxPage />} />
            <Route path="" element={<FrontPage/>} />
            <Route path="sub-sub-categories/:id" element={<SubSubCategoryPage />} />
            <Route path="sub-categories/:id" element={<SubCategoryPage />} />
            <Route path="search" element={<BuyerSearchResultsPage />} />
            <Route path="orders/:id" element={<OrderDetails />} />

          </Route>

          <Route path="*" element={<div>Page Not Found</div>} />
          <Route path="/login" element={<AuthentionCardChild />} />
          <Route path="/google-redirect" element={<GoogleLoginRedirect />} />

          <Route path="/" element={<NotAuthenticatedNavbar />}>
  <Route index element={<NotAuthenticatedPage />} />  
  <Route path="users" element={<NotAuthenticatedPage />} />
</Route>

        </Routes>

            </AuthProvider>

            </SearchProvider>

      </Router>
  );
}



