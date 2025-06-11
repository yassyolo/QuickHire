import { useCallback, useEffect, useState } from "react";
import { SellerPage } from "../Common/SellerPage";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { TitleFilterSelector } from "../../../../Shared/PageItems/TitleFilterSection/TitleFilterSection";
import { DataTable } from "../../../../Admin/Components/Tables/Common/AdminDataTable";
import { OrderActions } from "../../../../Admin/Components/Tables/TableActions/OrderActions";
import axios from "../../../../axiosInstance";


export interface OrderRow{
  id: number;
  buyerUsername: string;
  gigTitle: string;
  dueOn: string;
  total: number;
  status: string;
}

const tableHeaders = {
  id: "ID",
  buyerUsername: "Buyer",
  gigTitle: "Service",
  dueOn: "Due On",
  total: "Total",
  status: "Status",
};


export function SellerOrders (){ 
    const [moderationStatusId, setModerationStatusId] = useState<number>(1);
    const [orders, setOrders] = useState<OrderRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
   
    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();              
                if (moderationStatusId) params.append("OrderStatusId", moderationStatusId.toString());

                const response = await axios.get<OrderRow[]>(`https://localhost:7267/seller/orders/table`, {
                    params: params
                });
                setOrders(response.data);
    
                        } catch (error) {
                console.error("Error fetching orders:", error);
            } finally {
                setLoading(false);
            }
        }, [moderationStatusId]);

    useEffect(() => {
        fetchGigs();
    }
    , [ moderationStatusId, fetchGigs]);

    return(
        <SellerPage>
          <div className="filter-table">
              <PageTitle title="Orders"   description="Track and manage all your gig orders — view order status, delivery timelines, and communicate with buyers in one place."
breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/seller/dashboard" }, { label: "Orders" }]}/>         
              {loading ? (<div className="loading">Loading...</div>
              ) : (
                <><TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/admin/filters/order-status" /><div className="categories-list">
                <DataTable data={orders} columns={["id", "buyerUsername", "gigTitle", "dueOn", "total", "status"]} headers={tableHeaders} renderActions={(row: OrderRow) => (<OrderActions order={row}  />)} />
              </div></>
              )}
            </div>
        </SellerPage>
);

}