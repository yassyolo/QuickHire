import { useCallback, useEffect, useState } from "react";
import { SellerPage } from "../Common/SellerPage";
import { PageTitle } from "../../../../Admin/Pages/Common/PageTitle";
import { TitleFilterSelector } from "../../../../Admin/Pages/Common/TitleFilterSection";
import { DataTable } from "../../../../Admin/Components/Tables/Common/AdminDataTable";
import { SellerGigActions } from "../../../../Admin/Components/Tables/TableActions/SellerGigActions";

export interface GigRow{
  id: number;
  clicks: number;
  title: string;
  likes: number;
  orders: number;
  revenue: number;
}
const tableHeaders = {
  id: "ID",
    clicks: "Clicks",
    title: "Title",
    likes: "Likes",
    orders: "Orders",
    revenue: "Revenue",
};

export function SellerGigs (){ 
    const [moderationStatusId, setModerationStatusId] = useState<number>(0);
    const [gigs, setGigs] = useState<GigRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
   
    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();              
                if (moderationStatusId) params.append("ModerationStatusId", moderationStatusId.toString());
    
                const url = `https://localhost:7267/seller/gigs/table?${params.toString()}`;
    
                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Accept': '*/*',
                    },
                });
                if (!response.ok) {
                    throw new Error("Failed to fetch gigs");
                }
                const r = await response.json() as GigRow[];
    
                setGigs(r);
            } catch (error) {
                console.error("Error fetching gigs:", error);
            } finally {
                setLoading(false);
            }
        }, [moderationStatusId]);

    useEffect(() => {
        fetchGigs();
    }
    , [ moderationStatusId, fetchGigs]);

    const handleOnActivateGigSuccess  = (id: number) => {
        setGigs((prevGigs) => prevGigs.map((gig) => gig.id === id ? { ...gig, moderationStatusId: 1 } : gig));
        setModerationStatusId(1); 
    }

    const handleOnPauseGigSuccess = (id: number) => {
        setGigs((prevGigs) => prevGigs.map((gig) => gig.id === id ? { ...gig, moderationStatusId: 3 } : gig));
        setModerationStatusId(4);
    }

    const onDeleteSuccess = (id: number) => {
        setGigs((prevGigs) => prevGigs.filter((gig) => gig.id !== id));
    };

    return(
        <SellerPage>
          <div className="filter-table">
              <PageTitle title="Gigs" description="Manage your gigs — create, edit, or remove listings and track their success."breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/seller/dashboard" }, { label: "Gigs" }]}/>         
              {loading ? (<div className="loading">Loading...</div>
              ) : (
                <><TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/admin/filters/moderation-status" /><div className="categories-list">
                <DataTable data={gigs} columns={["id", "title", "clicks", "likes", "orders", "revenue"]} headers={tableHeaders} renderActions={(row: GigRow) => (<SellerGigActions gig={row} paused={moderationStatusId === 4} onActivateGigSuccess={handleOnActivateGigSuccess} onDeactivateGigSuccess={handleOnPauseGigSuccess} onDeleteSuccess={onDeleteSuccess} />)} />
              </div></>
              )}
            </div>
        </SellerPage>
);

}