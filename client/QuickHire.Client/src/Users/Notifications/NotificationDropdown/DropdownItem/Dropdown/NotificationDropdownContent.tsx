import { NotificationItem } from '../../NotificationButtonDropdown';
import './NotificationDropdownContent.css';
import { NotificationDropdownContentItem } from '../NotificationDropdownContentItem';
interface NotificationDropdownContentProps {
    onHasNotifications: (has: boolean) => void;
    buyer: boolean;
    notifications: NotificationItem[];
    handleNotificationReadSuccessful: (id: number) => void;
}


export function NotificationDropdownContent({notifications, handleNotificationReadSuccessful}: NotificationDropdownContentProps) {
    
    return (
        <div className="notification-dropdown-content">
            {notifications.length > 0 && (
                notifications.map((notification) => (
                    <NotificationDropdownContentItem key={notification.id} id={notification.id}  title={notification.title}  message={notification.message} createdAt={notification.createdAt}   handleNotificationReadSuccessful={handleNotificationReadSuccessful}/>
                ))
            ) }
            
        </div>
    );
}