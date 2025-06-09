import { useEffect, useState } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Breadcrumb } from "../../../../Shared/Breadcrumb/Breadcrumb";
import { SideNavigation } from "../../../../Shared/SideNavigation/SideNavigation";
import { GigsForUser } from "./GigsForUser";
import { UserStatisticsForAdmin } from "./UserStatisticsForAdmin";
import { UserInfo } from "./UserInfo";
import { SellerPage } from "../../../../Users/Seller/Pages/Common/SellerPage";
import { UserModerationStatus } from "./UserModerationStatus";

export function UserDetailsForAdmin() { 
     const { id } = useParams<{ id: string }>();
     const [showUserDetails, setShowUserDetails] = useState(false);
     const [showStatistics, setShowStatistics] = useState(false);
     const [showGigs, setShowGigs] = useState(false);
     const [showModeration, setShowModeration] = useState(false);


        

     useEffect(() => {
        if (id) {
            handleUserDetailsVisibility();
        }
    }, [id]);

    const handleModerationVisibility = () => {
        setShowModeration(!showModeration);
        setShowUserDetails(false);
        setShowStatistics(false);
        setShowGigs(false);
    }

     const handleUserDetailsVisibility = () => {
        setShowUserDetails(!showUserDetails);
        setShowStatistics(false);
        setShowGigs(false);
        setShowModeration(false);
     };

     const handleStatisticsVisibility = () => {
        setShowStatistics(!showStatistics);
        setShowUserDetails(false);
        setShowModeration(false);
        setShowGigs(false);
    };

    const handleGigsVisibility = () => {    
        setShowGigs(!showGigs);
        setShowUserDetails(false);
        setShowStatistics(false);
        setShowModeration(false);
    };


         useEffect(() => {
            const fetchDetails = async () => {
                try {
                    const params = new URLSearchParams();
                    if (id) {
                        params.append("id", id);
                    }
    
                    const url = `https://localhost:7267/admin/user/${params.toString()}`;
    
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
        <SellerPage>
            <div className="user-details-for-admin d-flex flex-row" >
            <div className="breadcrumb-side-nav">
                 <Breadcrumb items={[{ label: <i className="bi bi-house-door"></i> }, { label: "Users", to: "/admin/users" }]}/>
                 <SideNavigation items={[{ label: "Details", onClick: handleUserDetailsVisibility },{ label: "Gigs", onClick: handleGigsVisibility }, 
                    { label: "Statistics", onClick: handleStatisticsVisibility }, { label: "Moderation", onClick: handleModerationVisibility }]}></SideNavigation> 
            </div>
           
            {showUserDetails && <UserInfo/>}  
            {showGigs && (<GigsForUser/>)}
            {showStatistics && <UserStatisticsForAdmin/> }
            {showModeration && <UserModerationStatus userId={id ?? ""} />}
        </div>
        </SellerPage>
    );
}