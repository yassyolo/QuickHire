import { useEffect, useState } from "react";
import { PageTitle } from "../../../../Admin/Pages/Common/PageTitle";
import { PageSelector } from "../../../BillingAndPayments/PageSelector";
import { FinancialDocuments } from "../../../FinancialDocuments/FinancialDocuments";
import { BillingHistory } from "../../../BillingAndPayments/BillingHistory";


export function EarningsPagePage() {
    const [showBillingHistory, setShowBillingHistory] = useState(false);
    const [showFinancialDocuments, setShowFinancialDocuments] = useState(false);
    const [breadcrumbs, setBreadcrumbs] = useState<{ label: React.ReactNode; to?: string }[]>([]);
    
    useEffect(() => {
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: "/admin" },
            { label: "Earnings" }
        ]);

        handleBillingInfoVisibility();
    }, []);

    const handleBillingInfoVisibility = () => {
        setShowFinancialDocuments(!showFinancialDocuments);
        setShowBillingHistory(false);
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: "/admin" },
            { label: "Earnings", to: "/seller/earnings" },
            { label: "Overview" }
        ]);
    }

    const handleFinancialDocumentsVisibility = () => {
        setShowBillingHistory(!showBillingHistory);
        setShowFinancialDocuments(false);
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: "/admin" },
            { label: "Earnings", to: "/seller/earnings" },
            { label: "Financial documents" }
        ]);
    }
    return (
        <div className="billing-and-payments-page">
            <PageTitle title="Earnings"    description="Track your earnings, review financial documents, and manage your payout information all in one place."breadcrumbs={breadcrumbs}/>
            <PageSelector data={[
                { name: "Overview", onClick: handleFinancialDocumentsVisibility, isActive: showBillingHistory },
                { name: "Financial documents", onClick: handleBillingInfoVisibility, isActive: showFinancialDocuments },
            ]} />

            {showBillingHistory && 
            <BillingHistory/>}

            {showFinancialDocuments &&
            <div className="financial-documents">
                <FinancialDocuments />
            </div>}
       
        </div>
    );
}