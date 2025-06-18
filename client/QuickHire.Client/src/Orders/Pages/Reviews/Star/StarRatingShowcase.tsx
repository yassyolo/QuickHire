import './StarRatingShowcase.css';
export interface StarRatingShowcaseProps {
    rating: number;
}

export function StarRatingShowcase({rating} : StarRatingShowcaseProps) {
    return (
        <div className="star-rating-showcase">
            {[1, 2, 3, 4, 5].map((star) => (
             <span key={star}> {star <= rating ? <i className="bi bi-star-fill"></i> : <i className="bi bi-star"></i>}</span>
            ))}
        </div>
    );
}