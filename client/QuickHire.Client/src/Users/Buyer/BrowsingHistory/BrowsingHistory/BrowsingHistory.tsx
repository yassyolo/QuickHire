import { useEffect, useState } from "react";
import axios from "axios";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { GigCard } from "../../../../Gigs/GigCard/GigCard";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import './BrowsingHistory.css';
import { NoContentItem } from "../../../../Shared/NoContent/NoContentItem";

 export interface Gig {
     id: number;
     title: string;
     fromPrice: number;
     imageUrls: string[];
     sellerName: string;
     sellerId: number;
     sellerProfileImageUrl: string;
     topRatedSeller: boolean;
     reviewsCount: number;
     averageRating: number;
     liked: boolean;
 }

export function BrowsingHistory() {
    const [gigHistory, setGigHistory] = useState<Gig[] | null>(null);

   const fetchGigHistory = async () => {
        try {
            const response = await axios.get<Gig[]>('https://localhost:7267/buyers/browsing-history');
            setGigHistory(response.data);
        }
        catch (error) {
            console.error("Error fetching gig history:", error);
        }
    }

    useEffect(() => {
        fetchGigHistory();
    }
    , []);

     const handleClearBrowsingHistory = async () => {
        try {
            await axios.delete(`https://localhost:7267/buyers/browsing-history`);
            setGigHistory([]);
        } catch (error) {
            console.error("Error clearing browsing history:", error);
        }
    };

    const onSetLiked = (liked: boolean, gigId: number) => {
        setGigHistory((prev) =>
            (prev ?? []).map((gig) =>
                gig.id === gigId ? { ...gig, liked: liked } : gig
            )
        );
    }

    return (
        <SellerPage>
            <div className="browsing-history-container">
                <div className="page-title-button d-flex flex-row">
  <PageTitle
    title="Browsing history"
    description="Review and revisit the gigs you've recently viewed to find the right service faster."
    breadcrumbs={[
      { label: <i className="bi bi-house-door"></i>, to: "/buyer" },
      { label: "Browsing history" },
    ]}
  />
  <button className="browsing-history-button" onClick={handleClearBrowsingHistory}>Clear All</button>
</div>
 {gigHistory && gigHistory.length > 0 ? (
        <div className="browsing-history-items">
             {gigHistory.map((gig) => (  
                <GigCard key={gig.id} gig={gig} showSeller={true} setLiked={onSetLiked} />
             ))}
        </div>
 ) : (
    <div className="no-history">
        <NoContentItem title={"No browsing history"} description={"Start exploring servises now"}></NoContentItem>
    </div>
 )}
            
        </div>
        </SellerPage>
    );
}