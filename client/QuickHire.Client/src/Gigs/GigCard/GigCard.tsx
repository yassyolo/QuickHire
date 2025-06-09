import './GigCard.css';
import { GigCardSeller } from "./GigCardSeller/GigCardSeller";
import { GigCardRating } from "./GigCardRating/GigCardRating";
import { ImageCarrousel } from "../../Shared/ImageCarrousel/ImageCarrousel";
import { FavouriteButtonDropdown } from '../../Users/Buyer/Favourites/FavouriteButtonDropdown';

 interface GigCardProps {
     gig: Gig;
     showSeller: boolean;
        setLiked: (liked: boolean, id: number) => void;
 }

 export interface Gig {
     id: number;
     title: string;
     fromPrice: number;
     imageUrls: string[];
     sellerName: string;
     sellerId: number;
     sellerProfileImageUrl: string;
     topRatedSeller: boolean;
     reviewsCount: number;
     averageRating: number;
     liked: boolean;
 }

 export function GigCard({ gig, showSeller , setLiked}: GigCardProps) {
    return(
        <div className="gig-card">
            <div className="gig-card-image-button-wrapper">
                <ImageCarrousel images={gig.imageUrls}/>
                <div className="like-button-wrapper">
                     <FavouriteButtonDropdown liked={gig.liked} gigId={gig.id} setLiked={setLiked} />             
                </div>
            </div>
            {showSeller && <GigCardSeller sellerName={gig.sellerName} sellerId={gig.sellerId} sellerProfileImageUrl={gig.sellerProfileImageUrl} topRatedSeller={gig.topRatedSeller}/>}
            <div className="gig-card-title">{gig.title}</div>

            <GigCardRating reviewsCount={gig.reviewsCount} averageRating={gig.averageRating}/>
             <span className="gig-card-price-amount">From ${gig.fromPrice}</span>
        </div>
    )
 }