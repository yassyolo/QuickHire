import { useCallback, useEffect, useState } from "react";
import { SellerPage } from "../Common/SellerPage";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { DataTable } from "../../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { ProjectBriefActions } from "../../../../Shared/Tables/TableActions/ProjectBriefs/ProjectBriefActions";
import axios from "../../../../axiosInstance";

export interface ProjectBriefRow{
  id: number;
  buyerUsername: string;
  description: string;
  deliveryTimeInDays: string;
  budget: number;
}

const tableHeaders = {
  id: "ID",
    buyerUsername: "Buyer",
    description: "Description",
    deliveryTimeInDays: "Delivery Time (days)",
    budget: "Budget",
}


export function SellerProjectBriefs (){ 
    const [orders, setOrders] = useState<ProjectBriefRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
   
    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {    
                const url = `https://localhost:7267/seller/project-briefs/table`;
    
                const response = await axios.get<ProjectBriefRow[]>(url);
                if (response.status !== 200) {
                    throw new Error(`Failed to fetch gigs, status code: ${response.status}`);
                }
                const r = response.data.map((x: ProjectBriefRow) => ({
                    ...x,
                    deliveryTimeInDays: x.deliveryTimeInDays + " days",
                }));
                setOrders(r);
            } catch (error) {
                console.error("Error fetching orders:", error);
            } finally {
                setLoading(false);
            }
        }, []);

    useEffect(() => {
        fetchGigs();
    }
    , [ fetchGigs]);

    const onSendCustomOfferSuccess = useCallback(() => {
        fetchGigs();
    }, [fetchGigs]);

    return(
        <SellerPage>
          <div className="filter-table">
              <PageTitle title="Project briefs"    description="Browse project briefs that match your skills and interests. Reach out to potential buyers with custom offers tailored to their needs."

breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/seller/dashboard" }, { label: "Project briefs" }]}/>         
              {loading ? (<div className="loading">Loading...</div>
              ) : (
                <div style={{ marginTop: "30px" }}>
                <DataTable data={orders} columns={["id", "buyerUsername", "description", "deliveryTimeInDays", "budget"]} headers={tableHeaders} renderActions={(row: ProjectBriefRow) => (<ProjectBriefActions project={row} onSendCustomOfferSuccess={onSendCustomOfferSuccess} showNuyerInfo={true}  />)} />
              </div>
              )}
            </div>
        </SellerPage>
);

}