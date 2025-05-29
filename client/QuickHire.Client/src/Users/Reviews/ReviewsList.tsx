import { useEffect, useState } from "react";
import { SortBy } from "../../Admin/Components/Dropdowns/Common/SortBy";
import { ReviewRow } from "./ReviewRow";
import axios from "axios";
import { ActionButton } from "../../Shared/Buttons/ActionButton";
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
  
  const fetchReviews = async () => {
  const params = new URLSearchParams(window.location.search);

  if (gigId !== undefined) params.set("GigId", gigId.toString());
  if (userId !== undefined) params.set("UserId", userId.toString());
  if (selectedName !== undefined) params.set("SortBy", selectedName);
  await axios.get(`https://localhost:7267/gigs/statistics/reviews-statistics/${params.toString()}`, {
    headers: {
      "Accept": "*/*",
    },
  });
};

useEffect(() => {
  fetchReviews();
}, [gigId, userId, selectedName, showMore]);
  useEffect(() => {
    
      const mockData: Review[] = [
      {
        fullName: "John Doe",
        date: "2025-05-10",
        rating: 5,
        profileImageUrl: "https://i.pravatar.cc/50?img=1",
        comment: "Excellent service, highly recommended!",
        duration: "3",
        price: "150",
        countryName: "USA",
        repeatBuyer: true
      },
      {
        fullName: "Jane Smith",
        date: "2025-05-08",
        rating: 4,
        profileImageUrl: "https://i.pravatar.cc/50?img=2",
        comment: "Great experience, would work with again.",
        duration: "5",
        price: "200",
        countryName: "UK",
        repeatBuyer: true
      },
      {
        fullName: "Carlos Mendoza",
        date: "2025-05-05",
        rating: 3,
        profileImageUrl: "https://i.pravatar.cc/50?img=3",
        comment: "It was okay, some delays but delivered.",
        duration: "7",
        price: "100",
        countryName: "Mexico",
        repeatBuyer: true
      }
    ]

    setReviewsList(mockData);
        
  }, []);

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
