import { useEffect, useState } from "react";
import "./SellerDashboardOrders.css";
import axios from "../../../../../axiosInstance";
import { SellerDashboardOrder } from "../SellerDashboard";
import { SellerDashboardOrderItem } from "./SellerDashboardOrderItem";
import { SortBy } from "../../../../../Shared/Dropdowns/Common/Sort/SortBy";

export function SellerDashboardOrders() {
  const [orders, setOrders] = useState<SellerDashboardOrder[]>([]);
  const [selectedName, setSelectedName] = useState<string | undefined>("Active"); 
  const [orderType, setOrderType] = useState<string>("Active");

  useEffect(() => {
    if (selectedName === "Active" || selectedName === "Completed") {
      const isActive = selectedName === "Active";
      setOrderType(selectedName);
      fetchOrders(isActive);
    }
  }, [selectedName]);

  const handleSelectedNameChange = (name: string | undefined) => {
    setSelectedName(name);
  };

  const fetchOrders = async (isActive: boolean) => {
    try {
      const response = await axios.get<SellerDashboardOrder[]>(
        "https://localhost:7267/seller/dashboard/orders",
        {
          params: {
            active: isActive,
          },
        }
      );
      setOrders(response.data);
    } catch (error) {
      console.error("Error fetching orders:", error);
    }
  };

  return (
    <div className="seller-dashboard-orders-container d-flex flex-column">
      <div className="seller-dashboard-orders-header d-flex flex-row justify-content-between">
        <div className="seller-dashboard-orders-header-title">
          {orderType} Orders ({orders.length})
        </div>
        <SortBy setSelectedName={handleSelectedNameChange} type={"Orders"} />
      </div>
      <div className="seller-dashboard-orders-list">
        {orders.map((order, index) => (
          <div
            key={order.id}
            className={index !== orders.length - 1 ? "list-item-order" : ""}
          >
            <SellerDashboardOrderItem item={order} />
          </div>
        ))}
      </div>
    </div>
  );
}
