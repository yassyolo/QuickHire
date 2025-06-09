import { useState } from "react";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { NotificationSettingsItem } from "./NotificationSettingsItem";
import axios from "axios";
import './NotificationSettings.css';

export function NotificationSettings(){
    const [settings, setSettings] = useState<string[]>([]);
    const notificationSettings = [
        { label: "Inbox Messages" },
        { label: "Order Messages" },
        { label: "Order Updates" },
        { label: "Rating Reminders" },
        { label: "uyer Briefs" }
    ];

    const handleSettingChange = (label: string, checked: boolean) => {
        setSettings(prevSettings => {
            if (checked) {
                return [...prevSettings, label];
            } else {
                return prevSettings.filter(setting => setting !== label);
            }
        });
    }

    const handleOnSaveChanges = () => {
        axios.post("/api/notification-settings", { settings })
            .then(response => {
                console.log("Settings saved successfully:", response.data);
            })
            .catch(error => {
                console.error("Error saving settings:", error);
            });
    }

    return(
        <div className="notification-settings-container">
            <div className="info-description d-flex flex-column">
                <div className="notification-settings-info">Notifications</div>
                <div className="notification-settings-description">For important updates regarding your Fiverr activity, certain notifications cannot be disabled.</div>
            </div>
            <div className="notification-items">
                {notificationSettings.map((setting, index) => (
                    <NotificationSettingsItem key={index} label={setting.label} onChange={handleSettingChange}/>
                ))}
            </div>
            <ActionButton  text={"Save changes"} 
                                onClick={handleOnSaveChanges} 
                                className={"save-billing-info-button"} 
                                ariaLabel={"Save billing info button"} 
                            />

        </div>
    )
}