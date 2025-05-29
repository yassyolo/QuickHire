import { useState } from "react";
import { IconButton } from "../Buttons/IconButton";
import './NotificationButtonDropdown.css';
import { NotificationDropdown } from "./NotificationDropdown";

export function NotificationButtonDropdown() {
    const [showNotificationsDropdown, setShowNotificationsDropdown] = useState(false);
    const [hasNotifications, setHasNotifications] = useState(true); 

    const handleShowNotificationsDropdown = () => setShowNotificationsDropdown(!showNotificationsDropdown);

    return (
        <div className="notification-button-dropdown">
            <div className="notification-icon-wrapper">
                <IconButton 
                    buttonInfo="Notifications"  icon={<i className="fa-regular fa-bell"></i>} 
                    onClick={handleShowNotificationsDropdown} 
                    className="notifications-icon"  ariaLabel="Notifications" 
                />
                {hasNotifications && <span className="notification-dot" />}
            </div>

            {showNotificationsDropdown && (
                <div className="notification-dropdown">
                    <NotificationDropdown onHasNotifications={setHasNotifications}/>
                </div>
            )}
        </div>
    );
}
