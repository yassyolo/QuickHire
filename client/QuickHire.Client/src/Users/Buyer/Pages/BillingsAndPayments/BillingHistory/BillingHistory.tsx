import { FinancialDocuments } from "../FinancialDocuments/FinancialDocuments";
import "./BillingHistory.css";

interface BillingHistoryProps {
    buyer: boolean;
}

export function BillingHistory({ buyer }: BillingHistoryProps) {
    return (
        <div className="biling-history-container">
            <div className="biling-history-title">Billing History</div>   
            <FinancialDocuments buyer={buyer}/>      
        </div>
    );
}