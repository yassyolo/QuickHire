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
                <div className="revision-attachments d-flex flex-row" style={{
    overflowX: 'auto',
    gap: '20px',
    width: '80%',
    margin: '0 auto'  
  }}>
                        <img className="attachment-image" src={payload.attachment}     style={{ objectFit: 'cover', width: '400px', height: '250px', borderRadius: '10px' }}
  />
                </div>
            </div>
          


        </MessageItem>
    );
}