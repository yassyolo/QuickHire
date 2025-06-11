import { useEffect, useState } from "react";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { PageSelector } from "../../../../Shared/PageItems/PageSelector/PageSelector";
import { FinancialDocuments } from "../../../BillingAndPayments/BillingHistory/FinancialDocuments/FinancialDocuments";
import { SellerPage } from "../Common/SellerPage";
import { EarningsStatistics } from "../../../../Shared/Statistics/Users/EarningsStatistics";


export function SellerEarnings() {
    const [showEarningStatistics, setShowEarningStatistics] = useState(false);
    const [showFinancialDocuments, setShowFinancialDocuments] = useState(false);
    const [breadcrumbs, setBreadcrumbs] = useState<{ label: React.ReactNode; to?: string }[]>([]);
    
    useEffect(() => {
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: "/seller/dashboard" },
            { label: "Earnings" }
        ]);

        handleEarningStatisticsVisibility();
    }, []);

    const handleEarningStatisticsVisibility = () => {
        setShowEarningStatistics(!showEarningStatistics);
        setShowFinancialDocuments(false);
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: "/seller/dashboard" },
            { label: "Earnings", to: "/seller/earnings" },
            { label: "Overview" }
        ]);
    }  

    const handleFinancialDocumentsVisibility = () => {
        setShowFinancialDocuments(!showFinancialDocuments);
        setShowEarningStatistics(false);
        setBreadcrumbs([
            { label: <i className="bi bi-house-door"></i>, to: "/seller/dashboard" },
            { label: "Earnings", to: "/seller/earnings" },
            { label: "Financial documents" }
        ]);
    }

    return (
        <SellerPage>
            <PageTitle title="Earnings"    description="Track your earnings, review financial documents, and manage your payout information all in one place."breadcrumbs={breadcrumbs}/>
            <PageSelector data={[
                { name: "Overview", onClick: handleEarningStatisticsVisibility, isActive: showEarningStatistics },
                { name: "Financial documents", onClick: handleFinancialDocumentsVisibility, isActive: showFinancialDocuments },
            ]} />

            {showEarningStatistics && <EarningsStatistics/>}

            {showFinancialDocuments &&
            <div className="financial-documents">
                <FinancialDocuments buyer={false} />
            </div>}
       
        </SellerPage>
    );
}