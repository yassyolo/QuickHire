import { useEffect, useState } from "react";
import { SortBy } from "../../../Shared/Dropdowns/Common/Sort/SortBy";
import { ReviewRow } from "../ReviewsListItem/ReviewRow";
import axios from "axios";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
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

  const handleShowMore = () => 
    {setShowMore(!showMore);}

  const fetchReviews = async () => {
    try {
      const response = await axios.get<Review[]>("https://localhost:7267/orders/reviews", {
        params: {
          GigId: gigId,
          UserId: userId,
          SortBy: selectedName,
          ShowMore: showMore,
        },
      });
      setReviewsList(response.data);
    } catch (error) {
      console.error("Error fetching reviews:", error);
    }
  }

  useEffect(() => {
  if (selectedName === undefined) {
    setSelectedName("Most recent");
  }
}, []);

  useEffect(() => {
    if (selectedName === "Most recent" || selectedName === "Price" || selectedName === "Duration") {
      fetchReviews();
    }
  }, [selectedName]);
  
  useEffect(() => {
  fetchReviews();
}, [gigId, userId, selectedName, showMore]);


  
  
  const handleSelectedNameChange = (selectedName: string | undefined) => setSelectedName(selectedName);


  return (
    <div className="reviews-list">
      <div className="sort-by-list d-flex flex-column">
<SortBy setSelectedName={handleSelectedNameChange} type={"Reviews"} />
        {reviewsList.map((review, index) => (<ReviewRow key={index} review={review} />))}
      </div>
      <div className="show-more d-flex">
        {!showMore ? (
    <ActionButton
      text={"Show more reviews"}
      onClick={handleShowMore}
      className={"show-more-button"}
      ariaLabel={"Show More Button"}
    />
  ) : (
    <ActionButton
      text={"Show less reviews"}
      onClick={handleShowMore}
      className={"show-more-button"}
      ariaLabel={"Show Less Button"}
    />
  )} 
      </div>
   </div>
  );
}
