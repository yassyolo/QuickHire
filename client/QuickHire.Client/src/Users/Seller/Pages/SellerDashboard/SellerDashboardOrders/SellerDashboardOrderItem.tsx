import { useNavigate } from "react-router-dom";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { SellerDashboardOrder } from "../SellerDashboard";
import "./SellerDashboardOrderItem.css";

interface SellerDashboardOrderItemProps {
    item: SellerDashboardOrder;
}

export function SellerDashboardOrderItem({ item }: SellerDashboardOrderItemProps) {
    const navigate = useNavigate();

    const handleOrderNavigate = () => {
        navigate(`/seller/orders/${item.id}`);
    };
    return (
        <div className="seller-dashboard-order-item d-flex flex-row">
            <div className="seller-dashboard-order-image-wrapper">
                <img src={item.imageUrl} alt={item.title} className="seller-dashboard-order-image" />
            </div>
            <div className="seller-dashboard-order-title">{item.title}</div>

            <div className="seller-dashboard-order-details d-flex flex-row">
                <div className="seller-dashboard-order-details-item d-flex flex-column">
                    <div className="seller-dashboard-order-details-label">Due in:</div>
                    <div className="seller-dashboard-order-details-item-value">{item.dueIn}</div>
                </div>
                <div className="seller-dashboard-order-details-item d-flex flex-column">
                    <div className="seller-dashboard-order-details-label">Price:</div>
                    <div className="seller-dashboard-order-details-item-value">${item.price}</div>
                </div>
                <div className="seller-dashboard-order-details-item d-flex flex-column">
                    <div className="seller-dashboard-order-details-label">Status</div>
                    <div className="seller-dashboard-order-details-item-value">{item.status}</div>
                </div>
            </div>

            <ActionButton text={"View"} onClick={handleOrderNavigate} className={"view-order-details-row-button"} ariaLabel={"View-order-details"}></ActionButton>
        </div>
    );
}