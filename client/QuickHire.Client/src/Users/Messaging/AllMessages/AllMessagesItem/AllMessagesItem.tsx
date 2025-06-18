import { AllMessagesItem } from "../../InboxPage/InboxPage";
import "./AllMessagesItemComponent.css";
import axios from "../../../../axiosInstance";

interface AllMessagesItemProps {
  message: AllMessagesItem;
  setSelectedMessageId: (id: number | null) => void;
  onStarSuccess?: (messageId: number) => void;
  onMessageClick: (messageId: number) => void;
}

export function AllMessagesItemComponent({ message, setSelectedMessageId, onMessageClick, onStarSuccess }: AllMessagesItemProps) {
  const handleToggleConversationLike = async () => {
  try {
    const url = `https://localhost:7267/messages/star?messageId=${message.id}`;
    const response = await axios.post(url); // no body

    if (response.status === 200) {
        if (onStarSuccess) {
          onStarSuccess(message.id); 
        }
    }
  } catch (error) {
    console.error("Error toggling conversation like:", error);
  }
};


    const handleMessageClick = () => {
        setSelectedMessageId(message.id);
        onMessageClick(message.id);
    };

  return (
    <div
      className="inbox-messages-item d-flex flex-row"
      onClick={handleMessageClick}
      style={{ cursor: "pointer" }}
    >
      <div className="message-item">
        <img
          className="sender-profile-image"
          src={message.senderProfilePictureUrl}
          alt={`${message.senderUsername}'s profile`}
        />
        <div className="message-content d-flex flex-column">
          <div className="username-date d-flex flex-row justify-content-between">
            <div className="sender-username">{message.senderUsername}</div>
            <div className="date-star d-flex flex-row">
              <span className="message-timestamp">{message.timestamp}</span>
              <i
                className={`bi ${message.isStarred ? "bi-star-fill" : "bi-star"}`}
                onClick={(e) => {
                  e.stopPropagation(); 
                  handleToggleConversationLike();
                }}
                style={{ cursor: "pointer", marginLeft: "8px" }}
              ></i>
            </div>
          </div>

          <div className={`message-text ${message.isRead ? "read" : "unread"}`}>{message.content}</div>
        </div>
      </div>
    </div>
  );
}
