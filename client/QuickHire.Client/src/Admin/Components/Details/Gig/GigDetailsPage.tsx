import { useEffect, useState } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Breadcrumb } from "../../../../Shared/Breadcrumb/Breadcrumb";
import { SideNavigation } from "../../../../Shared/SideNavigation/SideNavigation";
import { GigDetails } from "./GigInfo";
import { GigStatistics } from "./GigStatistics";
import './GigDetailsPage.css';
import { ReviewsList } from "../../../../Gigs/Reviews/ReviewsList/ReviewsList";
import { AdminPage } from "../../../Pages/Common/AdminPage";
import { UserForGig } from "./UserForGig";
import { RatingDistribution } from "../../../../Gigs/Reviews/RatingDistrbution/RatingDistribution";
import { GigModerationStatus } from "./GigModeration";

/*export interface GigDetailsProps {
    
}*/


export function GigDetailsForAdmin() { 
     const { id } = useParams<{ id: string }>();
        const gigId = id ? parseInt(id, 10) : NaN;
     const [showGigDetails, setShowGigDetails] = useState(false);
     const [showStatistics, setShowStatistics] = useState(false);
     const [showReviews, setShowReviews] = useState(false);
     const [showUser, setShowUser] = useState(false);
     const [showGigModeration, setShowGigModeration] = useState(false);

     useEffect(() => {
        if (id) {
            handleGigDetailsVisibility();
        }
    }, [id]);

        const handleGigModerationVisibility = () => {
            setShowGigModeration(!showGigModeration);
            setShowGigDetails(false);
            setShowStatistics(false);
            setShowReviews(false);
            setShowUser(false);
        }

     const handleGigDetailsVisibility = () => {
        setShowGigDetails(!showGigDetails);
        setShowStatistics(false);
        setShowReviews(false);
        setShowUser(false);
        setShowGigModeration(false);
     };

     const handleReviewsVisibility = () => {
        setShowReviews(!showReviews);
        setShowGigDetails(false);
        setShowStatistics(false);
        setShowUser(false);
        setShowGigModeration(false);
    };

     const handleStatisticsVisibility = () => {
        setShowStatistics(!showStatistics);
        setShowGigDetails(false);
        setShowReviews(false);
        setShowGigModeration(false);
        setShowUser(false);
    };

    const handleUserVisibility = () => {    
        setShowUser(!showUser);
        setShowGigDetails(false);
        setShowReviews(false);
        setShowGigModeration(false);
        setShowStatistics(false);
    };


         useEffect(() => {
            const fetchDetails = async () => {
                try {
                    const params = new URLSearchParams();
                    const parsedId = id ? parseInt(id, 10) : NaN;
                    if (!isNaN(parsedId)) params.append("id", parsedId.toString());
    
                    const url = `https://localhost:7267/admin/gigs/${parsedId}`;
    
                    await axios.get(url, {
                        headers: {
                            'Accept': '*/*',
                        },
                    });
                    
                } catch (error) {
                    console.error("Error fetching main category details:", error);
                } 
            };
    
            fetchDetails();
    
        }, [id]);

    return(
        <AdminPage>
            <div className="gig-details-page">
            <div className="breadcrumb-side-nav">
                 <Breadcrumb items={[{ label: <i className="bi bi-house-door"></i> }, { label: "Gigs", to: "/admin/gigs" }]}/>
                 <SideNavigation items={[{ label: "Details", onClick: handleGigDetailsVisibility }, { label: "Reviews", onClick: handleReviewsVisibility }, { label: "Seller", onClick: handleUserVisibility }, { label: "Statistics", onClick: handleStatisticsVisibility }, { label: "Moderation", onClick: handleGigModerationVisibility }]}></SideNavigation> 
            </div>
           
           <div className="gig-details-modals">
            {showGigDetails && (<GigDetails id={gigId}></GigDetails>)}  

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
        </AdminPage>
    );
}