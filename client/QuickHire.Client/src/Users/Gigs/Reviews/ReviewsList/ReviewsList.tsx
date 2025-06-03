import { useEffect, useState } from "react";
import { SortBy } from "../../../../Admin/Components/Dropdowns/Common/Sort/SortBy";
import { ReviewRow } from "./ReviewsListItem/ReviewRow";
import axios from "axios";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton";
import "./ReviewsList.css";

export interface Review {
  fullName: string;
  date: string;
  rating: number;
  profileImageUrl: string;
  comment: string;
  duration: string;
  price: string;
  countryName: string;
  repeatBuyer: boolean;
}

export interface ReviewsListProps {
  gigId?: number;
  userId?: number;
}

export function ReviewsList({ gigId, userId }: ReviewsListProps) {
  const [reviewsList, setReviewsList] = useState<Review[]>([]);
  const [selectedName, setSelectedName] = useState<string | undefined>(undefined);
  const [showMore, setShowMore] = useState<boolean>(false);

  const handleShowMore = () => setShowMore(!showMore);
  
  useEffect(() => {
  if (!gigId || !userId) return;

  const fetchReviews = async () => {
  if (!gigId || !userId) return;
  try {
    const params = new URLSearchParams();
    params.append('GigId', gigId.toString());
    params.append('UserId', userId.toString());
    if (selectedName) {
      params.append('SortBy', selectedName);
    }
    params.append('ShowMore', showMore.toString());
            const url = `https://localhost:7267/orders/reviews?${params.toString()}`;

    const response = await axios.get<Review[]>(url, { params });
    setReviewsList(response.data);
  } catch (error) {
    console.error("Error fetching reviews:", error);
  }
};
  fetchReviews();
}, [gigId, userId, selectedName, showMore]);


  
  
  const handleSelectedNameChange = (selectedName: string | undefined) => setSelectedName(selectedName);


  return (
    <div className="reviews-list">
      <div className="sort-by-list d-flex flex-column">
        <SortBy setSelectedName={handleSelectedNameChange} type={"Reviews"} />
        {reviewsList.map((review, index) => (<ReviewRow key={index} review={review} />))}
      </div>
      <ActionButton text={"Show more reviews"} onClick={handleShowMore} className={"show-more-button"} ariaLabel={"Show More Button"}></ActionButton>
    </div>
  );
}
