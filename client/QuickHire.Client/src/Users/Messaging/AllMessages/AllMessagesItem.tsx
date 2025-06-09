import { AllMessagesItem } from "../InboxPage";
import "./AllMessagesItemComponent.css";
import axiosInstance from "../../../axiosInstance";

interface AllMessagesItemProps {
    message: AllMessagesItem;
}

export function AllMessagesItemComponent({ message }: AllMessagesItemProps) {

    const handleToggleConversationLike = async () => {
        try {
            const response = await axiosInstance.post(`/api/messages/${message.id}/toggle-like`);
            if (response.status === 200) {
                console.log("Message like toggled successfully");
            }
        } catch (error) {
            console.error("Error toggling message like:", error);
        }
    };

    return (
        <div className="inbox-messages-item d-flex flex-row">
            <div className="message-item">
                <img className="sender-profile-image" src={message.senderProfilePictureUrl} alt={`${message.senderUsername}'s profile`} />
                <div className="message-content d-flex flex-column">
                    <div className="username-date d-flex flex-row justify-content-between">
                        <div className="sender-username">{message.senderUsername}</div>
                    <div className="date-star d-flex flex-row">
                    <span className="message-timestamp">{message.timestamp}</span>
                    <i className={`bi ${message.isStarred ? "bi-star-fill" : "bi-star"}`} onClick={handleToggleConversationLike}></i>
                </div>
                    </div>
                    
                    <div className={`message-text ${message.isRead ? "read" : "unread"}`}>{message.content}</div>
                </div>
                
            </div>
        </div>
    );
}