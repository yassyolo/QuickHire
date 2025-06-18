import { useEffect, useState } from "react";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import "./ProjectBriefPreview.css";
import axios from "../../../../../axiosInstance";


 interface ProjectBriefPreview{
    projectBriefNumber: string;
    description: string;
    aboutBuyer: string;
    budget: number;
    deliveryTimeInDays: number;
    subSubCategoryName: string;
    buyerName: string;
    buyerProfilePictureUrl: string;
    createdAt: string;
    memberSince: string;
    status : string;
    location: string;
    languages: string[];
 }

 interface ProjectBriefPreviewProps {
    id: number;
    showBuyerInfo: boolean;
    onClose: () => void;
 }

 export function ProjectBriefPreview({ id, showBuyerInfo, onClose }: ProjectBriefPreviewProps) {
    const [projectBrief, setProjectBrief] = useState<ProjectBriefPreview | null>(null);

    useEffect(() => {
        const fetchProjectBrief = async () => {
            try {
                const url = `https://localhost:7267/project-brief/preview/${id}`;
                const response = await axios.get<ProjectBriefPreview>(url);
                if (response.status !== 200) {
                    throw new Error(`Failed to fetch project brief, status code: ${response.status}`);
                }
                const data = response.data;
                setProjectBrief(data);
            } catch (error) {
                console.error("Error fetching project brief:", error);
            }
        };

        fetchProjectBrief();
    }, [id]);

    if (!projectBrief) return <div>Loading...</div>;

    return (
        <div className="project-overlay">
            <div className="project-page d-flex flex-row">
               
         <div className="project-page-preview">
            <div className="project-number-status d-flex flex-row">
            <div className="project-number">#{projectBrief.projectBriefNumber} </div>
            <span className="project-status">{projectBrief.status}</span>
            </div>
            <div className="-d-flex flex-column project-brief-info">
                <div className="d-flex flex-row project-details">
                    <div className="d-flex flex-column project-details-item border">
                        <div className="project-details-item-label">Created at</div>
                        <div className="project-details-item-value">{projectBrief.createdAt}</div>
                    </div>
                    <div className="d-flex flex-column project-details-item border">
                        <div className="project-details-item-label">Delivery time (days)</div>
                        <div className="project-details-item-value">{projectBrief.deliveryTimeInDays}</div>
                    </div>
                    <div className="d-flex flex-column project-details-item border">
                        <div className="project-details-item-label">Budget($)</div>
                        <div className="project-details-item-value">{projectBrief.budget}</div>
                    </div>
                    <div className="d-flex flex-column project-details-item">
                        <div className="project-details-item-label">Industry</div>
                        <div className="project-details-item-value">{projectBrief.subSubCategoryName}</div>
                    </div>
                </div>
                <div className="buyer-info-description d-flex flex-row justify-content-between">
                    <div className="d-flex flex-column">
                        <div className="project-brief-info-item d-flex flex-column">
                            <div className="project-brief-info-item-label">About buyer</div>
                            <div className="project-brief-info-item-value">{projectBrief.aboutBuyer}</div>
                        </div>

                        <div className="project-brief-info-item d-flex flex-column">
                          <div className="project-brief-info-item-label">Description</div>
                         <div className="project-brief-info-item-value">{projectBrief.description}</div>
                        </div>
                    </div>
                {showBuyerInfo && (
                <div className="project-buyer-info d-flex flex-column">
                    <img className="buyer-profile-picture" src={projectBrief.buyerProfilePictureUrl} alt={`${projectBrief.buyerName}'s profile`} />
                    <div className="project-buyer-info-full-name">{projectBrief.buyerName}</div>
                    <div className="project-buyer-info-name">Member since: {new Date(projectBrief.memberSince).toLocaleDateString()}</div>
                    <div className="project-buyer-info-name">Location: {projectBrief.location}</div>
                    <div className="project-buyer-info-name">Languages: {projectBrief.languages.join(", ")}</div>
                </div>
            )}
                </div>
            </div>
            
           
            </div>
                        <IconButton icon={<i className="bi bi-x"></i>} onClick={onClose} className={"close-button"} ariaLabel={"Close project brief preview"} />

            </div>
                   </div>
        
    );
}