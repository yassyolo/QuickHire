import { NotificationDropdownContent } from "../DropdownItem/Dropdown/NotificationDropdownContent";
import './NotificationDropdown.css';
import { NotificationItem } from "../NotificationButtonDropdown";

export interface NotificationDropdownProps {
    onHasNotifications: (has: boolean) => void;
    buyer: boolean;
    notifications: NotificationItem[]; 
    onNotificationReadSuccessful: (id: number) => void;
}

export function NotificationDropdown({ onHasNotifications, buyer, notifications, onNotificationReadSuccessful }: NotificationDropdownProps) {
    return (
        <div className="notification-dropdown-container">
            <div className="notification-dropdown-top">
                <div className="notifications-top-title"><i className="bi bi-bell" style={{marginRight:"5px", fontWeight: 600}}></i>Notifications  ({notifications.length})</div>
            </div>
            <div className="notification-dropdown-content">
                <NotificationDropdownContent notifications={notifications} buyer={buyer} onHasNotifications={onHasNotifications} handleNotificationReadSuccessful={onNotificationReadSuccessful}/>
            </div>
            <div className="notification-dropdown-bottom">
            </div>
        </div>
    );
}