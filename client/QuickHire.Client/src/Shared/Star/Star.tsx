import "./Star.css"; 
interface StarRatingProps {
  rating: number;
  onRatingChange: (rating: number) => void;
}

export const StarRating: React.FC<StarRatingProps> = ({ rating, onRatingChange }) => {
    const handleStarClick = (star: number) => () => onRatingChange(star);

  return (
    <div className="star-rating">
      {[1, 2, 3, 4, 5].map((star) => (
        <span key={star} onClick={handleStarClick(star)}> {star <= rating ? <i className="bi bi-star-fill"></i> : <i className="bi bi-star"></i>}</span>
      ))}
    </div>
  );
};
