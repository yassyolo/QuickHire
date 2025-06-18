import { useEffect, useState } from "react";
import { SideNavigation } from "../../../../Shared/PageItems/SideNavigation/SideNavigation";
import { AccountSettings } from "./AccountSettings/AccountSettings";
import "./SettingsPage.css";
import { SecuritySettings } from "./SecuritySettings/SecuritySettings";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import { Breadcrumb } from "../../../../Shared/PageItems/Breadcrumb/Breadcrumb";

interface SettingsPageProps {
    homeUrl: string;
}

export function SettingsPage({ homeUrl }: SettingsPageProps) {
      const [view, setView] = useState<"account" | "security">("account");
      const [breadcrumbs, setBreadcrumbs] = useState<{ label: React.ReactNode; to?: string }[]>([]);

    const handleOnSaveChanges = () => {
        setView("account");
    }

    useEffect(() => {

        if (view === "account") {
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/${homeUrl}/dashboard` },
                { label: "Settings", to: `/${homeUrl}/settings` },
                { label: "Account" }
            ]);
        }

        if (view === "security") {
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/${homeUrl}/dashboard` },
                { label: "Settings", to: `/${homeUrl}/settings` },
                { label: "Security" }
            ]);
        }

    }, [view, homeUrl]);
    
    return (
        <SellerPage>
            <Breadcrumb items={breadcrumbs}></Breadcrumb>
            
            
        <div className="settings-page-container">
            <SideNavigation items={[{ label: "Account", onClick: () => setView("account"), value: "account" }, { label: "Security", onClick: () => setView("security"), value: "security" }]} active={view}/>
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
            </div>

        </div>
        </SellerPage>
    );
}