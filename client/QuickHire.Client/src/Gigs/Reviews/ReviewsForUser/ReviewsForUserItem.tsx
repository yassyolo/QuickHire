import { StarRatingShowcase } from "../../../Shared/Star/StarRatingShowcase";
import { ReviewForUser } from "./ReviewsForUser";
import "./ReviewsForUserItem.css";

interface ReviewsForUserProps {
    review: ReviewForUser;}

export function ReviewsForUserItem({ review }: ReviewsForUserProps) {
    return (
        <div className="reviews-for-user-item d-flex flex-row">
            <img src={review.profileImageUrl} alt={`${review.fullName}'s profile`} className="profile-image" />
<div className="d-flex flex-column info-comment">
     <div className="d-flex flex-row riviews-for-user-header">
                    <div className="reviews-for-user-name">{review.fullName}</div>
                    <StarRatingShowcase rating={review.rating} />                             
                    <div className="reviews-for-user-date">{review.date}</div>
            </div>
            <p className="reviews-for-user-comment">{review.comment}</p>
</div>
           
        </div>
    );
}