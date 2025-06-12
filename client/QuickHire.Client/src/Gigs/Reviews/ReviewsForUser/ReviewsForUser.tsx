import { useState } from "react";
import { NoPagesPagination } from "../../../Shared/PageItems/Pagination/NoPagesPagination/NoPagesPagination";
import { ReviewsForUserItem } from "./ReviewsForUserItem";
import "./ReviewsForUser.css";
export interface ReviewForUser{
  fullName: string;
  date: string;
  rating: number;
  profileImageUrl: string;
  comment: string;
}

interface ReviewsForUserProps {
  reviewsList: ReviewForUser[];
}
export function ReviewsForUser({ reviewsList }: ReviewsForUserProps) {
    const [currentTipIndex, setCurrentTipIndex] = useState(0);

    const handlePageChange = (page: number) => {
        setCurrentTipIndex(page);
    }

  return(
    <div className="reviews-for-user-wrapper d-flex flex-column">
        <div className="reviews-for-user-header">What people loved about this freelancer</div>
        <div className="no-pages-wrapper">        
            <NoPagesPagination totalPages={reviewsList.length} currentPage={currentTipIndex}  onPageChange={handlePageChange} />
        </div>
        <div className="reviews-for-user-list">
            {reviewsList.map((review, index) => (
                index === currentTipIndex && (
                    <ReviewsForUserItem key={index} review={review}/>
                )
            ))}
        </div>
        
    </div>  )
}