import { StarRatingShowcase } from '../../../Shared/Star/StarRatingShowcase';
import { Review } from '../ReviewsList/ReviewsList';
import './ReviewRow.css';

export interface ReviewRowProps {
    review: Review;
}

export function ReviewRow({review}: ReviewRowProps) {
    return(
        <div className="review-row" aria-label="review-row">
            <div className="reviewer-info">
                <img src={review.profileImageUrl} alt={"profile-image"} className="profile-image" />
                <div className="name-country">
                    <div className="d-flex flex-row">
                        <div className="reviewer-name">{review.fullName}</div>
                        {review.repeatBuyer == true && <span className="repeat-buyer"><i className="bi bi-repeat"></i>Repeat Buyer</span>}
                    </div>

                  <div className="country-name">{review.countryName}</div>
                </div>
            </div>
            <div className="rating-date">
               <StarRatingShowcase rating={review.rating} />            
               <div className="review-date">{review.date}</div>
            </div>

            <div className="review-comment">{review.comment}</div>
            <div className="review-details">
                <div className="review-details-info price">
                    <div className="review-details-label">${review.price}</div>
                    <div className="review-details-info">Price</div>
                </div>
                <div className="review-details-info duration">
                    <div className="review-details-label">{review.duration} days</div>
                    <div className="review-details-info">Duration</div>
                </div>
            </div>
        </div>
    );
}