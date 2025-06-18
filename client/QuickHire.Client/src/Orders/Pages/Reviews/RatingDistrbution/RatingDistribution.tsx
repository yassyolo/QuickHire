import { useEffect, useState } from "react";
import "./RatingDistribution.css"
import { StarRatingShowcase } from "../Star/StarRatingShowcase";
import axios from "axios";
export interface Ratings{
    stars: number;
    count: number;
}

export interface RatingDistribution{
    ratings: Ratings[];
    total: number;
    average: number;
}

export interface RatingDistributionProps {
    gigId?: number
    userId?: number;
}

export function RatingDistribution({ gigId, userId }: RatingDistributionProps) {
    const [ratings, setRatings] = useState<RatingDistribution>()

     useEffect(() => {
     const fetchRatingDistribution = async () => {
        try {
            const response = await axios.get<RatingDistribution>("https://localhost:7267/orders/reviews/ratings-distribution", {
        params: {
          GigId: gigId,
          UserId: userId,
        },
      });
            setRatings(response.data);
        } catch (error) {
            console.error("Error fetching rating distribution:", error);
        }
    }

    fetchRatingDistribution();
  }, [gigId, userId]);
    return(
        <div aria-label="rating-distribution" className="rating-distribution">
            <div id={"rating-distribution"} className="rating-distribution-header">Reviews</div>
            <div className="rating-distribution-info">
                <div className="rating-distribution-total">{ratings?.total} reviews</div>
                <div className="rating-distribution-average"> <StarRatingShowcase rating={Math.round(ratings?.average ?? 0)}></StarRatingShowcase>{ratings?.average ?? 0}</div>
            </div>
            <div id={"rating-distribution-chart"} className="rating-distribution-chart">
                {[5,4,3,2,1].map((star) => {
const rating = ratings?.ratings?.find(x => x.stars === star);
                    const count = rating ? rating.count : 0;
                    const percentage = ratings ? (count / ratings.total) * 100 : 0;
                    return (
                        <div key={star} className="rating-distribution-bar">
                            {star === 1 ? (<div className="rating-distribution-bar-icon">{star} Star</div>) : (<div className="rating-distribution-bar-icon">{star} Stars</div>)}
                            <div className="rating-distribution-bar-fill-wrapper">
                              <div className="rating-distribution-bar-fill" style={{ width: `${percentage}%` }}></div>
                            </div>
                            <div className="rating-distribution-bar-count">({count})</div>
                        </div>
                    );
                })}
            </div>
        </div>
    )
}