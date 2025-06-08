import { useCallback, useEffect, useState } from "react";
import { SellerPage } from "../Common/SellerPage";
import { PageTitle } from "../../../../Admin/Pages/Common/PageTitle";
import { DataTable } from "../../../../Admin/Components/Tables/Common/AdminDataTable";
import { ProjectBriefActions } from "../../../../Admin/Components/Tables/TableActions/ProjectBriefActions";

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
    
                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Accept': '*/*',
                    },
                });
                if (!response.ok) {
                    throw new Error("Failed to fetch orders");
                }
                const r = await response.json() as ProjectBriefRow[];
    
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
                <>
                <DataTable data={orders} columns={["id", "buyerUsername", "description", "deliveryTimeInDays", "budget"]} headers={tableHeaders} renderActions={(row: ProjectBriefRow) => (<ProjectBriefActions project={row} onSendCustomOfferSuccess={onSendCustomOfferSuccess} showNuyerInfo={true}  />)} />
              </>
              )}
            </div>
        </SellerPage>
);

}