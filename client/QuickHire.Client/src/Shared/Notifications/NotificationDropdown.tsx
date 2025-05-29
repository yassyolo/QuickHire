import { useState } from "react";
import { NotificationDropdownContent } from "./NotificationDropdownContent";
import './NotificationDropdown.css';

export interface NotificationDropdownProps {
    onHasNotifications: (has: boolean) => void;
}

export function NotificationDropdown({ onHasNotifications }: NotificationDropdownProps) {
    const [notificationsCount, setNotificationsCount] = useState(0);

    const handleNotificationsCountChange = (count: number) => setNotificationsCount(count);
    return (
        <div className="notification-dropdown-container">
            <div className="notification-dropdown-top">
                <div className="notifications-top-title"><i className="bi bi-bell" style={{marginRight:"5px", fontWeight: 600}}></i>Notifications  ({notificationsCount})</div>
            </div>
            <div className="notification-dropdown-content">
                <NotificationDropdownContent onNotificationCountChange={handleNotificationsCountChange} onHasNotifications={onHasNotifications}/>
            </div>
            <div className="notification-dropdown-bottom">
                <i className="bi bi-gear-fill" onClick={() => {}}></i>
            </div>
        </div>
    );
}