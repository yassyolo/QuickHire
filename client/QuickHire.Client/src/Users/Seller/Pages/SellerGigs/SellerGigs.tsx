import { useCallback, useEffect, useState } from "react";
import { SellerPage } from "../Common/SellerPage";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { TitleFilterSelector } from "../../../../Shared/PageItems/TitleFilterSection/TitleFilterSection";
import { DataTable } from "../../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { SellerGigActions } from "../../../../Shared/Tables/TableActions/Seller/SellerGigActions";
import axios from "../../../../axiosInstance";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { useNavigate } from "react-router-dom";
import "./SellerGigs.css";
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
    const navigate = useNavigate();
    const onCreateGigButtonClick = () => {
        navigate("/seller/gigs/new");
    }
   
    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);

    useEffect(() => {
        setModerationStatusId(1);
    }, []);

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();              
                if (moderationStatusId) params.append("ModerationStatusId", moderationStatusId.toString());
    
                const url = `https://localhost:7267/seller/gigs/table?${params.toString()}`;
    
                const response = await axios.get<GigRow[]>(url);
                if (response.status !== 200) {
                    throw new Error(`Failed to fetch gigs, status code: ${response.status}`);
                }
                const r = response.data.map((gig) => ({
                    id: gig.id,
                    clicks: gig.clicks,
                    title: gig.title,
                    likes: gig.likes,
                    orders: gig.orders,
                    revenue: gig.revenue
                }));
    
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
                <div style={{marginTop: '10px'}}>
                                <div className="pag-title-button">

                    <TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/filters/moderation-status" /><div className="categories-list">
                        <div className="create-gig-button-wrapper">
                                    <ActionButton text={"CREATE A NEW GIG"} onClick={onCreateGigButtonClick} className="add-category-button" ariaLabel={"Create new gig button"} />
                </div>
                            </div>

<div style={{marginBottom: '20px', width: '100%'}}>                
    <DataTable data={gigs} columns={["id", "title", "clicks", "likes", "orders", "revenue"]} headers={tableHeaders} renderActions={(row: GigRow) => (<SellerGigActions gig={row} paused={moderationStatusId === 4} onActivateGigSuccess={handleOnActivateGigSuccess} onDeactivateGigSuccess={handleOnPauseGigSuccess} onDeleteSuccess={onDeleteSuccess} />)} />
</div>
              </div></div>
              )}
            </div>
        </SellerPage>
);

}