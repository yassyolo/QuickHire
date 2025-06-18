import { useEffect, useState } from "react";
import axios from "../../axiosInstance";
import { IconButton } from "../../Shared/Buttons/IconButton/IconButton";
import "./CustomOfferPreview.css";
interface CustomOfferPreview {
    customOfferNumber: string;
    description: string;
    price: string;
    revisions: string;
    deliveryTimeInDays: string;
    offerIncludes: string[];
    createOn: string;
    status: string;
    sellerName: string;
    sellerProfilePictureUrl: string;
    memberSince: string;
    location: string;
    languages: string[];

}

interface CustomOfferPreviewProps {
    onClose: () => void;
    id: number;
    showSellerInfo: boolean;
}

export function CustomOfferPreview({ onClose, id, showSellerInfo }: CustomOfferPreviewProps) {
    const [customOffer, setCustomOffer] = useState<CustomOfferPreview | null>(null);
    const fetchCustomOffer = async () => {
        try {
            const response = await axios.get<CustomOfferPreview>(`https://localhost:7267/buyer/custom-offers/${id}`);
            setCustomOffer(response.data);
        } catch (error) {
            console.error("Error fetching custom offer:", error);
        }
    }

    useEffect(() => {
        fetchCustomOffer();
    }, [id]);

    return(<div className="offer-overlay">
              <div className="offer-page d-flex flex-row justify-content-between">
                <div className="offer-preview d-flex flex-column">
                                <div className="project-number-status d-flex flex-row">
            <div className="project-number">#{customOffer?.customOfferNumber} </div>
            <span className="project-status">{customOffer?.status}</span>
            </div>
            <div className="-d-flex flex-column project-brief-info">
                <div className="d-flex flex-row project-details">
                    <div className="d-flex flex-column project-details-item border">
                        <div className="project-details-item-label">Created at</div>
                        <div className="project-details-item-value">{customOffer?.createOn}</div>
                    </div>
                    <div className="d-flex flex-column project-details-item border">
                        <div className="project-details-item-label">Delivery time</div>
                        <div className="project-details-item-value">{customOffer?.deliveryTimeInDays}</div>
                    </div>
                    <div className="d-flex flex-column project-details-item border">
                        <div className="project-details-item-label">Total</div>
                        <div className="project-details-item-value">{customOffer?.price}</div>
                    </div>
                    <div className="d-flex flex-column project-details-item">
                        <div className="project-details-item-label">Revisions</div>
                        <div className="project-details-item-value">{customOffer?.revisions}</div>
                    </div>
                </div>
                <div className="buyer-info-description d-flex flex-row justify-content-between">
                    <div className="d-flex flex-column">
                        <div className="project-brief-info-item d-flex flex-column">
                          <div className="project-brief-info-item-label">Description</div>
                         <div className="project-brief-info-item-value">{customOffer?.description}</div>
                        </div>

                        <div className="project-brief-info-item d-flex flex-column">
                            <div className="project-brief-info-item-label">Offer includes</div>
                            <ul className="project-brief-info-item-value includes-list">
                                {customOffer?.offerIncludes.map((item, index) => (
                                    <li key={index}><i className="bi bi-check-all" style={{color: '#54FF73'}}></i>{item}</li>
                                ))}
                            </ul>
                        </div>
                    </div>
                    {showSellerInfo &&
                <div className="project-buyer-info d-flex flex-column">
                    <img className="buyer-profile-picture" src={customOffer?.sellerProfilePictureUrl}  />
                    <div className="project-buyer-info-full-name">{customOffer?.sellerName}</div>
                    <div className="project-buyer-info-name">Member since: {customOffer?.memberSince}</div>
                    <div className="project-buyer-info-name">Location: {customOffer?.location}</div>
                    <div className="project-buyer-info-name">Languages: {customOffer?.languages.join(", ")}</div>
                </div>}

                </div>
                
                                          
              </div>  
            </div>
                            <IconButton icon={<i className="bi bi-x"></i>} onClick={onClose} className={"close-button"} ariaLabel={"Close customOffer preview"} />

          </div>
        </div>
    );
}
