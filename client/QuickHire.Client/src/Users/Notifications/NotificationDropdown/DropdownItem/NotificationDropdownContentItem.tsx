import axios from '../../../../axiosInstance';
import './NotificationDropdownContentItem.css';
interface NotificationDropdownContentItemProps {
    id: number;
    title: string;
    message: string;
    createdAt: string;
    handleNotificationReadSuccessful: (id: number) => void;
}
export function NotificationDropdownContentItem({ id, handleNotificationReadSuccessful, title, message,
    createdAt
}: NotificationDropdownContentItemProps) {

    const handleNotificationRead = async () => {
        try {
            const url = `https://localhost:7267/notifications/${id}`;
            await axios.put(url);
            handleNotificationReadSuccessful(id);
        } catch (error) {
            console.error("Error marking notification as read:", error);
        }
    };
    return (
        <div className="notification-dropdown-content-item" key={id}>
            <div className="notification-icon">
                <i className="bi bi-bell-fill"></i>
            </div>
            <div className="d-flex flex-column">
              <div className="notification-title">{title}</div>
               <div className="notification-message">{message}</div>
               <span className="notification-timestamp">{createdAt}</span>
            </div>
            <div className="mark-as-read" onClick={handleNotificationRead}>
              <i className="bi bi-check2-all"></i>
            </div>

        </div>
    );
}