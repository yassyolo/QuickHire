import { Outlet, useNavigate } from 'react-router-dom';
import { IconButton } from '../../../Buttons/IconButton/IconButton';
import { NotificationButtonDropdown } from '../../../../Users/Notifications/NotificationDropdown/NotificationButtonDropdown';
import { ProfileIconDropdown } from '../Common/Profile/Common/ProfileDropdown/ProfileIconDropdown';
import { Footer } from '../../Footer/Footer';
import { BuyerProfileDropdown } from '../Common/Profile/Buyer/BuyerProfileDropdown';
import { MainCategoriesHeader } from '../../../../Users/Buyer/Common/MainCategoriesHeader/MainCategoriesHeader';
import './BuyerNavbar.css';
import { Logo } from '../../Logo/Logo';
import { SearchGig } from '../../../Forms/SearchInputs/SearchGigsByKeyword/SearchGig';
import { useSearch } from './SearchContext';

export function BuyerNavbar() {
    const { setKeyword } = useSearch();
    const navigate = useNavigate();
    const handleFavouritesIconClick = () => {
        navigate('/buyer/favourite-gigs');
    };

    const handleInboxIconClcik = () => {
        navigate('/buyer/inbox');
    };

  return (
    <div className="page-container">
        <nav className="top-seller-navbar">
            <div className="top-seller-navbar-left">
                <Logo />
                <SearchGig setKeyword={setKeyword}/>
            </div>

            <div className="top-seller-navbar-right">
                <button className="order-button" style={{padding: '10px 20px', border:'1px solid #ccc !important', color: '#ddd', backgroundColor: 'white !important', boxShadow: 'none !important', transform: 'none'}}  onClick={() => navigate('/buyer/orders')}>Orders</button>
                <IconButton buttonInfo="Favourites" icon={<i className="fa-regular fa-heart"></i>} onClick={handleFavouritesIconClick} className={"inbox-button"} ariaLabel={"FavoritesButton"}></IconButton>

                <NotificationButtonDropdown buyer={true}/>
                <IconButton buttonInfo="Messages" icon={<i className="fa-regular fa-envelope"></i>} onClick={handleInboxIconClcik} className={"inbox-button"} ariaLabel={"MessagesButton"}></IconButton>
                <ProfileIconDropdown> <BuyerProfileDropdown /></ProfileIconDropdown>
            </div>
        </nav>      
          <div className="bottom-seller-navbar">
            <MainCategoriesHeader />
            </div>

        <main className="buyer-main"> <Outlet /> </main>

<div className="buyer-footer">
            <Footer/>
</div>

    </div>
  );
}
