import { useEffect, useState } from "react";
import { SideNavigation } from "../../../Shared/PageItems/SideNavigation/SideNavigation";
import { SellerPage } from "../../../Users/Seller/Pages/Common/SellerPage";
import { GigRequirement } from "./GigRequirements/GigRequirements";
import axios from "../../../axiosInstance";
import { useParams } from "react-router-dom";
import { OrderProgressTracker, OrderStatusStep } from "./OrderStatus/OrderProgressTracker";
import { BillingHistoryRowModel } from "../../../Users/Buyer/Pages/BillingsAndPayments/FinancialDocuments/FinancialDocuments";
import { useAuth } from "../../../AuthContext";
import { DataTable } from "../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { OrderRevision, Revision } from "./Revisions/OrderRevision";
import { PaymentPlan } from "../../../Gigs/Pages/GigPreview/GigPreview";
import { OrderInfo } from "./OrderInfo/OrderInfo";
import { OrderChat } from "./OrderChat/OrderChat";

interface OrderDetails{
    gigRequirements : GigRequirement[];
    steps: OrderStatusStep[];
    currentStatus: string;   
    gigId: number;
    gigTitle: string;
    gigImageUrl: string;
    orderNumber: string;
    revision: Revision[];
    plan: PaymentPlan;
    conversationId: number;
}

const tableHeaders = {
    id: "ID",
    date: "Date",
    documentNumber: "Invoice Number",
    service: "Service",
    orderNumber: "Order",
    total: "Total",
    pdfLink: "Document Preview"
};

export  function OrderDetails() {
    const {user} = useAuth();
    const [order, setOrder] = useState<OrderDetails | null>(null);
    const {id} = useParams<{ id: string }>();
    const orderId = parseInt(id || "0", 10);
    const [invoices, setInvoices] = useState<BillingHistoryRowModel[]>([]);
   

    const fetchInvoices = async () => {
        try {
            const url = new URL(`https://localhost:7267/users/billings-and-payments/financial-documents`);
            const params = new URLSearchParams();
                params.append("keyword", "");
                params.append("fromDate", "");
                params.append("toDate", "");
                params.append("orderId", orderId.toString());
            params.append("buyer", user?.mode === "buyer" ? "true" : "false");
            const response = await axios.get<BillingHistoryRowModel[]>(url.toString(), { params });
            setInvoices(response.data);
            console.log("Invoices fetched:", response.data);
        } catch (error) {
            console.error("Error fetching billing history:", error);
        }
    };

    useEffect(() => {
        fetchInvoices();
    }, []);


    const fetchOrderDetails = async () => {
        try {
            const response = await axios.get<OrderDetails>(`https://localhost:7267/orders/${orderId}`);
            setOrder(response.data);
        } catch (error) {
            console.error("Error fetching order details:", error);
        }
    }

    useEffect(() => {
        fetchOrderDetails();
    }, [orderId]);

    const [view, setView] = useState< "details" | "revisions" | "delivery" | "documents" | "review" | "chat">("details");

    const handleChatVisibility = () => {
        setView("chat");
    };

    const handleDetailsVisibility = () => {
        setView("details");
    };
    
    const handleRevisionsVisibility = () => {
        setView("revisions");
    };
    const handleDeliveryVisibility = () => {
        setView("delivery");
    };
    const handleDocumentsVisibility = () => {
        setView("documents");
    };
    const handleReviewVisibility = () => {
        setView("review");
    };
    return (
        <SellerPage>
            <div className="d-flex flex-row justify-content-between">
             <SideNavigation items={[
                                { label: "Details", onClick: handleDetailsVisibility, value: 'details' },
                                { label: "Revisions", onClick: handleRevisionsVisibility, value: 'revisions' },
                                { label: "Delivery", onClick: handleDeliveryVisibility, value: 'delivery' },
                                { label: "Documents", onClick: handleDocumentsVisibility, value: 'documents' },
                                { label: "Review", onClick: handleReviewVisibility, value: 'review' },
                                { label: "Chat", onClick: handleChatVisibility, value: 'chat' }
                            ]} active={view}></SideNavigation>

{view ==="documents" && <div className="invoice-for-order-container" style={{width: '950px'}}><DataTable data={invoices} columns={[ "id", "date", "documentNumber", "service", "orderNumber", "total", "pdfLink"]} headers={tableHeaders} /></div> 
}
{view === "revisions" && order?.revision && order.revision.length > 0 && (
    <div className="revisions-container">
        {order.revision.map((revision, index) => (
            <OrderRevision key={index} revision={revision} />
        ))}
    </div>
)}
{view === "details" && order && <OrderInfo gigRequirements={order.gigRequirements} gigId={order.gigId} gigTitle={order.gigTitle} gigImageUrl={order.gigImageUrl} orderNumber={order.orderNumber} paymentPlan={order.plan}/>}
{view === "chat" && order?.conversationId !== undefined && <OrderChat id={order.conversationId} />}
<OrderProgressTracker steps={order?.steps ?? []} />
        </div>
        </SellerPage>
    );
}