import './GigCardRating.css';
interface GigCardRatingProps {
    reviewsCount: number;
     averageRating: number;
}
export function GigCardRating({ reviewsCount, averageRating }: GigCardRatingProps) {
    return(
        <div className="gig-card-rating d-flex flex-row">
            <div className="average-rating">
                <i className="fa-solid fa-star"></i>
                {averageRating.toFixed(1)}
            </div>
            <div className="reviews-count">({reviewsCount})</div>
        </div>
    )
}