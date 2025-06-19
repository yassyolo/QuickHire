import { DeliveryPayload } from "../../../../../Orders/Pages/OrderDetails/OrderChat/OrderChat";
import { MessageItem } from "../Common/MessageItem";

interface CustomOfferProps {
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
    content: string;
    payload: DeliveryPayload;
}

export function DeliveryMessage({ senderProfilePictureUrl, senderUsername, timestamp, payload, content }: CustomOfferProps) {
    return (
        <MessageItem senderProfilePictureUrl={senderProfilePictureUrl} senderUsername={senderUsername} content={content} timestamp={timestamp}>
            <div className="revision-message d-flex flex-column">
                <div className="decription-title">Description</div>
                <div className="revision-description">{payload.description}</div>
                <div className="decription-title">Attahcments</div>
                <div className="revision-attachments d-flex flex-row" style={{ overflowX: 'auto', gap: '20px' }}>
                        <img className="attachment-image" src={payload.attachment}  />
                </div>
            </div>
          


        </MessageItem>
    );
}