import { useEffect, useState } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import { Breadcrumb } from "../../../Shared/Breadcrumb/Breadcrumb";
import { SideNavigation } from "../../../Shared/SideNavigation/SideNavigation";
import { GigDetails } from "./GigDetails";
import './GigDetailsPage.css';
import { AdminPage } from "../../Pages/Common/AdminPage";
import { MixedStatistics } from "../Charts/Common/MixedLineChart";
import { GigsForUser } from "./GigsForUser";

/*export interface GigDetailsProps {
    
}*/


export function UserDetails() { 
     const { id } = useParams<{ id: string }>();
     const [showUserDetails, setShowUserDetails] = useState(false);
     const [showStatistics, setShowStatistics] = useState(false);
     const [showGigs, setShowGigs] = useState(false);

     useEffect(() => {
        if (id) {
            handleUserDetailsVisibility();
        }
    }, [id]);

     const handleUserDetailsVisibility = () => {
        setShowUserDetails(!showUserDetails);
        setShowStatistics(false);
        setShowGigs(false);
     };

     const handleStatisticsVisibility = () => {
        setShowStatistics(!showStatistics);
        setShowUserDetails(false);
        setShowGigs(false);
    };

    const handleGigsVisibility = () => {    
        setShowGigs(!showGigs);
        setShowUserDetails(false);
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
                 <Breadcrumb items={[{ label: <i className="bi bi-house-door"></i> }, { label: "Users", to: "/admin/users" }]}/>
                 <SideNavigation items={[{ label: "Details", onClick: handleUserDetailsVisibility },{ label: "Gigs", onClick: handleGigsVisibility }, { label: "Statistics", onClick: handleStatisticsVisibility },]}></SideNavigation> 
            </div>
           
           <div className="gig-details-modals">
            {showUserDetails && (<GigDetails id={1}></GigDetails>)}  
            {showGigs && (<GigsForUser/>)}
            {showStatistics && (<MixedStatistics></MixedStatistics>)} 
            </div>    
        </div>
        </AdminPage>
    );
}