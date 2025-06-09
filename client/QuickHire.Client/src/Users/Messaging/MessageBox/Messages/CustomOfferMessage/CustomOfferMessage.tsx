import { Link } from "react-router-dom";
import { MessageItem } from "../Common/MessageItem";
import "./CustomOfferMessage.css";

interface CustomOfferProps {
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
    content: string;
    payload: CustomOfferPayload;
}

export interface CustomOfferPayload{
    gigTitle: string;
    gigId: number;
    offerAmount: string;
    includes: string[];
    offerId: number;
    senderUsername: string;
}
export function CustomOfferMessage({ senderProfilePictureUrl, senderUsername, timestamp, payload, content }: CustomOfferProps) {
    return (
        <MessageItem senderProfilePictureUrl={senderProfilePictureUrl} senderUsername={senderUsername} content={content} timestamp={timestamp}>
            <div className="custom-offer-message d-flex flex-column">
                <div className="custom-offer-message-top d-flex flex-row justify-content-between">
                    <div className="custom-offer-message-gig-title">{payload.gigTitle}</div>
                    <div className="custom-offer-message-gig-price">{payload.offerAmount}</div>
                </div>
                <div className="view-gig-container">View gig <Link className="gig-link" to={`/buyer/gig/${payload.gigId}`}>here</Link></div>
                <div className="offer-includes-container">
                    <span className="offer-includes-label">Offer includes</span>
                    <div className="offer-includes-list d-flex flex-row">
                        {payload.includes.map((item, index) => (
                            <div key={index} className="offer-include-item"><i className="bi bi-check-all"></i>{item}</div>
                        ))}
                    </div>
                </div>
                <div className="offer-actions d-flex flex-row">
                    <Link to={`/buyer/gig/${payload.gigId}/offer/${payload.offerId}`} className="btn btn-primary">View Offer</Link>
                    <button className="btn btn-secondary">View Offer</button>
                    <button className="btn btn-danger">Withdraw Offer</button>
                </div>
            </div>
        </MessageItem>
    );
}