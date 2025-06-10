import { MessageItem } from "../Common/MessageItem";
import "./FileMessage.css";

interface FileMessageProps {
    fileUrl: string;
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
    content: string;
}
export function FileMessage({ fileUrl, senderProfilePictureUrl, senderUsername, timestamp, content }: FileMessageProps) {
    return (
        <MessageItem senderProfilePictureUrl={senderProfilePictureUrl} senderUsername={senderUsername} content={content} timestamp={timestamp}>
      <div className="file-message">
    <img className="file-icon" src={fileUrl} alt="File Icon" /></div>
        </MessageItem>
    )
}