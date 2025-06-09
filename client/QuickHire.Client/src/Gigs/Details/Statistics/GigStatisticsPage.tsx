import { GigStatistics } from "../../../Admin/Components/Details/Gig/GigStatistics";
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
                            <IconButton icon={<i className="bi bi-x"></i>} onClick={onGigPreviewClose} className={"Close gig preview"} ariaLabel={"Close gig preview"} />
            
            <GigStatistics id={id}></GigStatistics>  
            </div>
 </div>
    );
}