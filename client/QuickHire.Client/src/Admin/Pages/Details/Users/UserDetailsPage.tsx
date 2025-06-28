import { useEffect, useState } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Breadcrumb } from "../../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { SideNavigation } from "../../../../Shared/PageItems/SideNavigation/SideNavigation";
import { GigsForUser } from "../../../Pages/Details/Users/GigsForUser/GigsForUser";
import { UserInfo } from "./UserInfo/UserInfo";
import { SellerPage } from "../../../../Users/Seller/Pages/Common/SellerPage";
import { UserModerationStatus } from "../../../Pages/Details/Users/Moderation/UserModerationStatus";

export function UserDetailsForAdmin() { 
     const { id } = useParams<{ id: string }>();
     const [showUserDetails, setShowUserDetails] = useState(false);
     const [showGigs, setShowGigs] = useState(false);
     const [showModeration, setShowModeration] = useState(false);
     const [view, setView] = useState<"details" | "gigs" | "statistics" | "moderation">("details");


    

    const handleModerationVisibility = () => {
        setView("moderation");
        setShowModeration(!showModeration);
        setShowUserDetails(false);
        setShowGigs(false);
    }

     const handleUserDetailsVisibility = () => {
        setView("details");
        setShowUserDetails(!showUserDetails);
        setShowGigs(false);
        setShowModeration(false);
     };

    const handleGigsVisibility = () => { 
        setView("gigs");   
        setShowGigs(!showGigs);
        setShowUserDetails(false);
        setShowModeration(false);
    };


         useEffect(() => {
             setView("details");
            handleUserDetailsVisibility();
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
                 <SideNavigation items={[{
                        label: "Details", onClick: handleUserDetailsVisibility,
                        value: "details"
                    }, {
                        label: "Gigs", onClick: handleGigsVisibility,
                        value: "gigs"
                    }, {
                        label: "Moderation", onClick: handleModerationVisibility,
                        value: "moderation"
                    }]} active={view}></SideNavigation> 
            </div>
           
            {showUserDetails && <UserInfo/>}  
            {showGigs && (<GigsForUser userId={id ?? ""}/>)}
            {showModeration && <UserModerationStatus userId={id ?? ""} />}
        </div>
        </SellerPage>
    );
}