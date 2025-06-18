import { MessageItem } from "../Common/MessageItem";
import { useAuth } from "../../../../../AuthContext";
import { useState } from "react";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";

interface CustomOfferProps {
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
    content: string;
    payload: RevisionPayload;
}

export interface RevisionPayload {
    attachments: string[];
    description: string;
    revisionNumber: number;
    acceptUntil: string;
    revisionId: number;
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
                    {payload.attachments.map((attachment, index) => (
                        <img key={index} className="attachment-image" src={attachment} alt={`Attachment ${index + 1}`} />
                    ))}
                </div>
               
<div className="has-until-container">
    Buyer has until <span className="has-until-date">{payload.acceptUntil}</span> to accept this revision.
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