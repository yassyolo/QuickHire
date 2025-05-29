import { useEffect, useState } from 'react';
import './NotificationDropdownContent.css';
import { NotificationDropdownContentItem } from './NotificationDropdownContentItem';
//import axios from 'axios';
interface NotificationDropdownContentProps {
    onNotificationCountChange: (count: number) => void;
    onHasNotifications: (has: boolean) => void;
}

export interface Notifications{
    id: number;
    title: string;
    message: string;
    createdAt: string;
}

export function NotificationDropdownContent({onNotificationCountChange, onHasNotifications}: NotificationDropdownContentProps) {
    const [notifications, setNotifications] = useState<Notifications[]>([]);

    const handleNotificationReadSuccessful = (id: number) => {
        setNotifications((prevNotifications) => 
            prevNotifications.filter(notification => notification.id !== id)
        );
    };

    /*const fetchNotifications = async () => {
        try {
            const response = await axios.get('/api/notifications');
            setNotifications(response.data);
            onNotificationCountChange(response.data.length);
            onHasNotifications(response.data.length > 0);
        } catch (error) {
            console.error("Error fetching notifications:", error);
        }   
    }*/

        const fetchNotifications = async () => {
    const fakeNotifications: Notifications[] = [
        {
            id: Date.now(),
            title: "New Message",
            message: "You received a message from Emily.",
            createdAt: new Date().toISOString(),
        },
        {
            id: Date.now() + 1,
            title: "Order Delivered",
            message: "Your order #1234 has been delivered.",
            createdAt: new Date().toISOString(),
        }
    ];
    setNotifications(fakeNotifications);
    onNotificationCountChange(fakeNotifications.length);
    onHasNotifications(true);
};


    useEffect(() => {
        const interval = setInterval(() => {
            fetchNotifications();
        }, 5000); 

        return () => clearInterval(interval); 
    }, []);

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