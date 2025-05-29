import { FinancialDocuments } from "../FinancialDocuments/FinancialDocuments";
import "./BillingHistory.css";

export function BillingHistory() {
    return (
        <div className="biling-history-container">
            <div className="biling-history-title">Billing History</div>   
            <FinancialDocuments/>      
        </div>
    );
}