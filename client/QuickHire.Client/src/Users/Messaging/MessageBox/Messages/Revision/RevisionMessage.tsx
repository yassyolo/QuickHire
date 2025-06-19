import { MessageItem } from "../Common/MessageItem";
import { useAuth } from "../../../../../AuthContext";
import { useState } from "react";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import "./RevisionMessage.css";
import { RevisionPayload } from "../../../../../Orders/Pages/OrderDetails/OrderChat/OrderChat";

interface CustomOfferProps {
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
    content: string;
    payload: RevisionPayload;
}

export function RevisionMessage({ senderProfilePictureUrl, senderUsername, timestamp, payload, content }: CustomOfferProps) {
    const { user } = useAuth();
    const [showAcceptRevisionModal, setAcceptRevisionModal] = useState(false);
    const handleShowRevisionModal = () => {
        setAcceptRevisionModal(!showAcceptRevisionModal);
    }

    return (
        <MessageItem senderProfilePictureUrl={senderProfilePictureUrl} senderUsername={senderUsername} content={content} timestamp={timestamp}>
            <div className="revision-message d-flex flex-column">
                <div className="revision-message-header">Revision {payload.revisionNumber}</div>
                <div className="decription-title">Description</div>
                <div className="revision-description">{payload.description}</div>
                <div className="decription-title">Attahcments</div>
                <div className="revision-attachments d-flex flex-row" style={{ overflowX: 'auto', gap: '20px' }}>
                    <img className="attachment-image" src={payload.attachment}  />
                </div>
            

            </div>
          {user?.mode === "buyer" && (
            <>
            
                <ActionButton className="withdraw-offer-button" onClick={handleShowRevisionModal} text={"Accept"} ariaLabel={"Accept revision offer"}></ActionButton>
                            {showAcceptRevisionModal && <div></div>}

            </>
          ) }


        </MessageItem>
    );
}