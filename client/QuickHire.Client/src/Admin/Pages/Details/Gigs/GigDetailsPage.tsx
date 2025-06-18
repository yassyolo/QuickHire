import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { Breadcrumb } from "../../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { SideNavigation } from "../../../../Shared/PageItems/SideNavigation/SideNavigation";
import { GigDetails } from "./GigInfo/GigInfo";
import { GigStatistics } from "./Statistics/GigStatistics";
import './GigDetailsPage.css';
import { ReviewsList } from "../../../../Orders/Pages/Reviews/ReviewsLIst/ReviewsList";
import { UserForGig } from "./UserForGig/UserForGig";
import { RatingDistribution } from "../../../../Orders/Pages/Reviews/RatingDistrbution/RatingDistribution";
import { GigModerationStatus } from "./Moderation/GigModeration";


export function GigDetailsForAdmin() { 
     const { id } = useParams<{ id: string }>();
        const gigId = id ? parseInt(id, 10) : NaN;
     const [showGigDetails, setShowGigDetails] = useState(false);
     const [showStatistics, setShowStatistics] = useState(false);
     const [showReviews, setShowReviews] = useState(false);
     const [showUser, setShowUser] = useState(false);
     const [showGigModeration, setShowGigModeration] = useState(false);
     const [view, setView] = useState<"details" | "statistics" | "reviews" | "user" | "moderation">("details");

     useEffect(() => {
        if (id) {
            handleGigDetailsVisibility();
            setView("details"); 
        }
    }, [id]);

        const handleGigModerationVisibility = () => {
            setView("moderation");
            setShowGigModeration(!showGigModeration);
            setShowGigDetails(false);
            setShowStatistics(false);
            setShowReviews(false);
            setShowUser(false);
        }

     const handleGigDetailsVisibility = () => {
        setView("details");
        setShowGigDetails(!showGigDetails);
        setShowStatistics(false);
        setShowReviews(false);
        setShowUser(false);
        setShowGigModeration(false);
     };

     const handleReviewsVisibility = () => {
        setView("reviews");
        setShowReviews(!showReviews);
        setShowGigDetails(false);
        setShowStatistics(false);
        setShowUser(false);
        setShowGigModeration(false);
    };

     const handleStatisticsVisibility = () => {
        setView("statistics");
        setShowStatistics(!showStatistics);
        setShowGigDetails(false);
        setShowReviews(false);
        setShowGigModeration(false);
        setShowUser(false);
    };

    const handleUserVisibility = () => { 
        setView("user");   
        setShowUser(!showUser);
        setShowGigDetails(false);
        setShowReviews(false);
        setShowGigModeration(false);
        setShowStatistics(false);
    };

    return(
            <div className="gig-details-page-admin d-flex flex-row">
            <div className="breadcrumb-side-nav">
                 <Breadcrumb items={[{ label: <i className="bi bi-house-door"></i> }, { label: "Gigs", to: "/admin/gigs" }]}/>
                <SideNavigation items={[
                    { label: "Details", onClick: handleGigDetailsVisibility, value: 'details' },
                    { label: "Reviews", onClick: handleReviewsVisibility, value: 'reviews' },
                    { label: "Seller", onClick: handleUserVisibility, value: 'user' },
                    { label: "Statistics", onClick: handleStatisticsVisibility, value: 'statistics' },
                    { label: "Moderation", onClick: handleGigModerationVisibility, value: 'moderation' }
                ]} active={view}></SideNavigation> 
            </div>
           
           <div className="gig-details-modals">
            {showGigDetails && (<GigDetails></GigDetails>)}  

            {showReviews && (              
                <div className="ratings-in-gig d-flex flex-row " >
                    <ReviewsList gigId={gigId} userId={undefined}></ReviewsList>
                    <div className="rating-distribution-gig"> <RatingDistribution/></div>
                </div>              
            )}  

            {showUser && (<UserForGig gigId={gigId}/>)}
            {showStatistics && (<GigStatistics id={gigId}></GigStatistics>)} 
            {showGigModeration && <GigModerationStatus gigId={gigId}/>}
            </div>   
        </div>
    );
}