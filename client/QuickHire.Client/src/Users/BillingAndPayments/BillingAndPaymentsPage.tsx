import { useEffect, useState } from "react";
import { PageTitle } from "../../Admin/Pages/Common/PageTitle";
import { PageSelector } from "./PageSelector";
import { BillingHistory } from "./BillingHistory/BillingHistory";
import { SellerPage } from "../Seller/Pages/Common/SellerPage";
import { BillingInfo } from "./BillingInfo/BillingInfo";

interface BillingAndPaymentsPageProps {
    homeUrl: string;
    buyer: boolean;
}

export function BillingAndPaymentsPage({ homeUrl, buyer }: BillingAndPaymentsPageProps) {
    const [showBillingHistory, setShowBillingHistory] = useState(false);
    const [showBillingInfo, setShowBillingInfo] = useState(false);
    const [breadcrumbs, setBreadcrumbs] = useState<{ label: React.ReactNode; to?: string }[]>([]);
    
    useEffect(() => {
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: `/${homeUrl}` },
            { label: "Billing and Payments" }
        ]);

        handleBillingHistoryVisibility();
    }, []);

    const handleBillingInfoVisibility = () => {
        setShowBillingInfo(!showBillingInfo);
        setShowBillingHistory(false);
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: `/${homeUrl}` },
            { label: "Billing and Payments", to: `/${homeUrl}/billing-and-payment` },
            { label: "Billing Info" }
        ]);
    }

    const handleBillingHistoryVisibility = () => {
        setShowBillingHistory(!showBillingHistory);
        setShowBillingInfo(false);
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: `/${homeUrl}` },
            { label: "Billing and Payments", to: `/${homeUrl}/billing-and-payment` },
            { label: "Billing History" }
        ]);
    }
    return (
        <SellerPage>
            <PageTitle title="Billing and Payments"   description="View your billing history, manage payment methods, and keep your account information up to date."  breadcrumbs={breadcrumbs}/>
            <PageSelector data={[
                { name: "Billing history", onClick: handleBillingHistoryVisibility, isActive: showBillingHistory },
                { name: "Billing info", onClick: handleBillingInfoVisibility, isActive: showBillingInfo },
            ]} />

            {showBillingHistory && <BillingHistory buyer={buyer}/>}
            {showBillingInfo && <BillingInfo></BillingInfo>}      
        </SellerPage>
    );
}