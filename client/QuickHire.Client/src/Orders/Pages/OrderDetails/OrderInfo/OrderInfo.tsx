import { useNavigate } from "react-router-dom";
import { GigRequirement, GigRequirements } from "../GigRequirements/GigRequirements";

interface OrderInfoProps {
    buyerName: string;
    buyerProfilePictureUrl: string;
    memberSince: string;
    location: string;
    languages: string[];
    gigRequirements: GigRequirement[];
        gigId: number;
    gigTitle: string;
    gigImageUrl: string;
    orderNumber: string;
}

export function OrderInfo({ buyerName, buyerProfilePictureUrl, memberSince, location, languages, gigRequirements, gigId, gigImageUrl, gigTitle, orderNumber }: OrderInfoProps) {
    const navigate = useNavigate();
    const handleGigClick = () => {
      navigate(`/buyer/gigs/${gigId}`);

    };
    return(
        <div className="order-info d-flex flex-row">
            <div className="d-flex flex-column">
                <div className="order-number">Order Number: {orderNumber}</div>
                <div className="order-info-gig d-flex flex-row" onClick={handleGigClick}>
                    <img className="gig-image" src={gigImageUrl} alt={gigTitle} />
                    <div className="gig-details d-flex flex-column">
                        <div className="gig-title">{gigTitle}</div>
                    </div>
                </div>
                <GigRequirements requirements={gigRequirements ?? []}/>
            </div>
             <div className="project-buyer-info d-flex flex-column">
                    <img className="buyer-profile-picture" src={buyerProfilePictureUrl} alt={`${buyerName}'s profile`} />
                    <div className="project-buyer-info-full-name">{buyerName}</div>
                    <div className="project-buyer-info-name">Member since: {new Date(memberSince).toLocaleDateString()}</div>
                    <div className="project-buyer-info-name">Location: {location}</div>
                    <div className="project-buyer-info-name">Languages: {languages.join(", ")}</div>
            </div>
        </div>
    )
}