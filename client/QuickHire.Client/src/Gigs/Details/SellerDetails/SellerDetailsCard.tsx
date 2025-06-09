
import { ActionButton } from '../../../Shared/Buttons/ActionButton/ActionButton';
import { GigCardRating } from '../../GigCard/GigCardRating/GigCardRating';
import './SellerDetailsCard.css';
import { useNavigate } from 'react-router-dom';
interface SellerDetailsCardProps {
    profileImageUrl: string;
    fullName: string;
    description: string;
    rating: number;
    totoalReviews: number;
    location: string;
    topRated: boolean;
    memberSince: string;
    lastDelivery: string;
    languages: string[];
    industry: string;
}

export function SellerDetailsCard({
    profileImageUrl,
    fullName,
    description,
    rating,
    totoalReviews,
    location,
    topRated,
    memberSince,
    lastDelivery,
    languages,
    industry
}: SellerDetailsCardProps) {

    const navigate = useNavigate();
    const handleContactMe = () => {
        navigate(`/messages/new?recipient=${fullName}`);
    };
    return (
        <div className="seller-details-card d-flex flex-column">
            <div className="seller-details-card-header">Get to know {fullName}</div>
            <div className="d-flex flex-row seller-details-card-top">
                <img src={profileImageUrl} alt={`${fullName}'s profile`} className="seller-details-profile-image" />
                <div className="d-flex flex-column seller-details-info-details">
                    <div className="d-flex flex-row">
                        <div className="seller-details-name">{fullName}</div>
                        {topRated && <span className="top-rated-badge">Top Rated</span>}
                    </div>
                    <div className="seller-details-industry">{industry}</div>
                    <GigCardRating reviewsCount={totoalReviews} averageRating={rating} />           
                </div>
            <ActionButton text={"Contact me"} onClick={handleContactMe} className={'contact-me-button'} ariaLabel={'Contact Seller Button'}/>
            </div>
            <div className="seller-details-card-content">
                <div className="seller-details-card-content-top">
                    <div className="seller-details-card-content-item d-flex flex-column">
                        <div className="seller-details-card-content-item-header">From</div>
                        <div className="seller-details-card-content-item-value">{location}</div>
                    </div>
                    <div className="seller-details-card-content-item d-flex flex-column">
                        <div className="seller-details-card-content-item-header">Member since</div>
                        <div className="seller-details-card-content-item-value">{memberSince}</div>
                    </div>
                     <div className="seller-details-card-content-item d-flex flex-column">
                        <div className="seller-details-card-content-item-header">Last delivery</div>
                        <div className="seller-details-card-content-item-value">{lastDelivery}</div>
                    </div>
                     <div className="seller-details-card-content-item d-flex flex-column">
                        <div className="seller-details-card-content-item-header">Languages</div>
                        <div className="seller-details-card-content-item-value">{languages.join(", ")}</div>
                    </div>
                </div>
            </div>
            <div className="seller-description">{description}</div>
                   
        </div>
    );
}