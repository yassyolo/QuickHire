import { GigStatistics } from "../../../Admin/Components/Details/Gig/Statistics/GigStatistics";
import { IconButton } from "../../../Shared/Buttons/IconButton/IconButton";
import "./GigStatisticsPage.css";

interface GigStatisticsProps {
    id: number;
    onGigPreviewClose: () => void;
}

export function GigStatisticsPage({id, onGigPreviewClose}: GigStatisticsProps) {
    return (
        <div className="gig-statistics-page-overlay">
            <div className="gig-statistics-page">            
            <GigStatistics id={id}></GigStatistics>  
                                        <IconButton icon={<i className="bi bi-x"></i>} onClick={onGigPreviewClose} className={"close-button"} ariaLabel={"Close gig preview"} />
            </div>
 </div>
    );
}