import { useCallback, useEffect, useState } from "react";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { TitleFilterSelector } from "../../../../Shared/PageItems/TitleFilterSection/TitleFilterSection";
import { DataTable } from "../../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { OrderActions } from "../../../../Shared/Tables/TableActions/Orders/OrderActions";
import axios from "../../../../axiosInstance";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";


export interface OrderRow{
  id: number;
  gigTitle: string;
  dueOn: string;
  total: number;
    sellerUsername: string;
}

const tableHeaders = {
  id: "ID",
  sellerUsername: "Seller",
  gigTitle: "Service",
  dueOn: "Due On",
  total: "Total($)",
};


export function BuyerOrders (){ 
    const [moderationStatusId, setModerationStatusId] = useState<number>(1);
    const [orders, setOrders] = useState<OrderRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
   
    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();              
                if (moderationStatusId) params.append("OrderStatusId", moderationStatusId.toString());
                params.append("buyer", "true"); 

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
        setModerationStatusId(3); 
    }, []);
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
                <div style={{marginTop: '10px'}}><TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/admin/filters/order-status" /><div className="categories-list">
                <DataTable data={orders} columns={["id", "sellerUsername", "gigTitle", "dueOn", "total"]} headers={tableHeaders} renderActions={(row: OrderRow) => (<OrderActions order={row}  />)} />
              </div></div>
              )}
            </div>
        </SellerPage>
);

}