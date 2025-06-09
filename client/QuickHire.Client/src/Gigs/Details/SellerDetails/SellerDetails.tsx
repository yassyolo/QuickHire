import { useEffect, useState } from "react";
import { Portfolio } from "../../../Users/Seller/Pages/SellerProfile/SellerProfile";
import { ReviewForUser, ReviewsForUser } from "../../Reviews/ReviewsForUser/ReviewsForUser";
import axios from "axios";
import { SellerDetailsCard } from "./SellerDetailsCard";

interface SellerDetails{
    id: string;
    fullName: string;
    profileImageUrl: string;
    description: string;
    rating: number;  
        totoalReviews: number;
    location: string;
    reviews: ReviewForUser[];
    topRated: boolean;
    memberSince: string;
    lastDelivery: string;
    languages: string[];
    portfilios: Portfolio[];
    industry: string;
}

interface SellerDetailsProps {
    gigId: number;
}
export function GigSellerDetails({gigId}: SellerDetailsProps) {
    const [sellerDetails, setSellerDetails] = useState<SellerDetails | null>(null);

    useEffect(() => {
        const fetchSellerDetails = async () => {
            try {
                const response = await axios.get<SellerDetails>(`https://localhost:7267/gigs/seller-details/${gigId}`);
                setSellerDetails(response.data);
            } catch (error) {
                console.error("Error fetching seller details:", error);
            }
        };
        fetchSellerDetails();
    }, [gigId]);

    return (
        <div className="seller-details d-flex flex-column">
            <ReviewsForUser reviewsList={sellerDetails?.reviews ?? []}   /> 
            <SellerDetailsCard
                profileImageUrl={sellerDetails?.profileImageUrl ?? ""}
                fullName={sellerDetails?.fullName ?? ""}
                description={sellerDetails?.description ?? ""}
                rating={sellerDetails?.rating ?? 0}
                totoalReviews={sellerDetails?.totoalReviews ?? 0}
                location={sellerDetails?.location ?? ""}
                topRated={sellerDetails?.topRated ?? false}
                memberSince={sellerDetails?.memberSince ?? ""}
                lastDelivery={sellerDetails?.lastDelivery ?? ""}
                languages={sellerDetails?.languages ?? []}
                industry={sellerDetails?.industry ?? ""}
            />
            </div>
    );
}