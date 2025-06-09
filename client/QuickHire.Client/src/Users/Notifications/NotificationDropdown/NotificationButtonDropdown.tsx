import { useState, useEffect } from "react";
import axios from "axios";
import { IconButton } from "../../../Shared/Buttons/IconButton/IconButton";
import './NotificationButtonDropdown.css';
import { NotificationDropdown } from "./Dropdown/NotificationDropdown";

export interface NotificationItem {
    id: number;
    title: string;
    message: string;
    createdAt: string;
}

interface NotificationButtonDropdownProps {
    buyer: boolean;
}

export function NotificationButtonDropdown({ buyer }: NotificationButtonDropdownProps) {
    const [showNotificationsDropdown, setShowNotificationsDropdown] = useState(false);
    const [notifications, setNotifications] = useState<NotificationItem[]>([]);
    const [hasNotifications, setHasNotifications] = useState(false);

    const fetchNotifications = async () => {
        try {
            const buyerParam = buyer ? "true" : "false";
            const url = "https://localhost:7267/notifications";
            const response = await axios.get<NotificationItem[]>(`${url}?buyer=${buyerParam}`);
            setNotifications(response.data);
            setHasNotifications(response.data.length > 0);
        } catch (error) {
            console.error("Error fetching notifications:", error);
        }
    };

    useEffect(() => {
        fetchNotifications();
        const interval = setInterval(() => {
            fetchNotifications();
        }, 20000);

        return () => clearInterval(interval);

    }, [buyer]);

    const handleShowNotificationsDropdown = () => setShowNotificationsDropdown(prev => !prev);

    const handleNotificationReadSuccessful = (id: number) => {
        setNotifications(prev => prev.filter(n => n.id !== id));
        setHasNotifications(prev => prev && notifications.length - 1 > 0);
    };

    return (
        <div className="notification-button-dropdown">
            <div className="notification-icon-wrapper">
                <IconButton
                    buttonInfo="Notifications"
                    icon={<i className="fa-regular fa-bell"></i>}
                    onClick={handleShowNotificationsDropdown}
                    className="notifications-icon"
                    ariaLabel="Notifications"
                />
                {hasNotifications && <span className="notification-dot" />}
            </div>

            {showNotificationsDropdown && (
                <div className="notification-dropdown">
                    <NotificationDropdown 
                        notifications={notifications}
                        onNotificationReadSuccessful={handleNotificationReadSuccessful}
                        buyer={buyer}
                        onHasNotifications={setHasNotifications}
                    />
                </div>
            )}
        </div>
    );
}
