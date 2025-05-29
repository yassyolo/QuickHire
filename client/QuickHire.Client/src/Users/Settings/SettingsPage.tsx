import { useState } from "react";
import { SideNavigation } from "../../Shared/SideNavigation/SideNavigation";
import { AccountSettings } from "./AccountSettings";
import "./SettingsPage.css";
import { SecuritySettings } from "./SecuritySettings";
import { NotificationSettings } from "./NotifictionSettings";
import { SellerPage } from "../Seller/SellerPage";


export function SettingsPage() {
      const [view, setView] = useState<"account" | "security" | "notifications">("account");

    const handleOnSaveChanges = () => {
        setView("account");
    }
    
    return (
        <SellerPage>
        <div className="settings-page-container">
            <SideNavigation items={[{ label: "Account", onClick: () => setView("account") }, { label: "Security", onClick: () => setView("security") }, { label: "Notifications", onClick: () => setView("notifications") }]} />
                <div className="settings-page-items">
            {view === "account" && (
                <div className="account-view">
                    <AccountSettings  onSaveChanges={handleOnSaveChanges}/>
                </div>
            )}

            {view === "security" && (
                <div className="security-view">
                    <SecuritySettings onSaveChanges={handleOnSaveChanges}/>
               </div>
            )}

            {view === "notifications" && (
                <div className="notifications-view">
                    <NotificationSettings />
                </div>
            )}
            </div>

        </div>
        </SellerPage>
    );
}