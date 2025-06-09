import React from "react";
import "./MessageItem.css"; 
interface MessageProps {
    senderProfilePictureUrl: string;
    senderUsername: string;
    content: string;
    timestamp: string;
    children?: React.ReactNode;
}



export function MessageItem({ senderProfilePictureUrl, senderUsername, content, timestamp, children }: MessageProps) {
    return (
        <div className="message-item-inbox d-flex flex-row">
            <img className="message-item-sender-profile-image" src={senderProfilePictureUrl} alt={`${senderUsername}'s profile`} />
            <div className="message-item-content d-flex flex-column">
                <div className="d-flex flex-row username-date">
                    <div className="message-item-sender-username">{senderUsername}</div>
                    <span className="message-item-timestamp">{timestamp}</span>
                </div>
                <div className="message-text">{content}</div>
                {children && <div className="message-item-children">{children}</div>}
            </div>
        </div>
    );
}