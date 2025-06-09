import { useState } from "react";
import { NotificationDropdownContent } from "../DropdownItem/NotificationDropdownContent";
import './NotificationDropdown.css';
import { NotificationItem } from "../NotificationButtonDropdown";

export interface NotificationDropdownProps {
    onHasNotifications: (has: boolean) => void;
    buyer: boolean;
    notifications: NotificationItem[]; 
    onNotificationReadSuccessful: (id: number) => void;
}

export function NotificationDropdown({ onHasNotifications, buyer, notifications, onNotificationReadSuccessful }: NotificationDropdownProps) {
    const [notificationsCount, setNotificationsCount] = useState(0);

    const handleNotificationsCountChange = (count: number) => setNotificationsCount(count);
    return (
        <div className="notification-dropdown-container">
            <div className="notification-dropdown-top">
                <div className="notifications-top-title"><i className="bi bi-bell" style={{marginRight:"5px", fontWeight: 600}}></i>Notifications  ({notificationsCount})</div>
            </div>
            <div className="notification-dropdown-content">
                <NotificationDropdownContent notifications={notifications} buyer={buyer} onNotificationCountChange={handleNotificationsCountChange} onHasNotifications={onHasNotifications} handleNotificationReadSuccessful={onNotificationReadSuccessful}/>
            </div>
            <div className="notification-dropdown-bottom">
            </div>
        </div>
    );
}