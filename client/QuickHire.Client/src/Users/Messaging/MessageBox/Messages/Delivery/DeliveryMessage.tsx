import { MessageItem } from "../Common/MessageItem";

interface CustomOfferProps {
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
    content: string;
    payload: DeliveryPayload;
}

export interface DeliveryPayload {
    attachments: string[];
    description: string;
}
export function DeliveryMessage({ senderProfilePictureUrl, senderUsername, timestamp, payload, content }: CustomOfferProps) {
    return (
        <MessageItem senderProfilePictureUrl={senderProfilePictureUrl} senderUsername={senderUsername} content={content} timestamp={timestamp}>
            <div className="revision-message d-flex flex-column">
                <div className="decription-title">Description</div>
                <div className="revision-description">{payload.description}</div>
                <div className="decription-title">Attahcments</div>
                <div className="revision-attachments d-flex flex-row" style={{ overflowX: 'auto', gap: '20px' }}>
                    {payload.attachments.map((attachment, index) => (
                        <img key={index} className="attachment-image" src={attachment} alt={`Attachment ${index + 1}`} />
                    ))}
                </div>
            </div>
          


        </MessageItem>
    );
}