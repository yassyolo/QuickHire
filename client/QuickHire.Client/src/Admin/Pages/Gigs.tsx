import { useCallback, useEffect, useState } from "react";
import {Pagination} from "../../Shared/Pagination/Pagination/Pagination";
import { DataTable } from "../Components/Tables/Common/AdminDataTable";
import { GigsFilter } from "../Components/Filters/PageFilters/GigsFilter";
import { GigActions } from "../Components/Tables/TableActions/GigActions";
import { AdminPage } from "./Common/AdminPage";
import { PageTitle } from "./Common/PageTitle";
import { TitleFilterSelector } from "./Common/TitleFilterSection";

export interface GigRow{
  id: number;
  createdOn: string;
  service: string;
  orders: number;
  revenue: number;
  clicks: number;
  avgReview: number;
}

const tableHeaders = {
  id: "ID",
  createdOn: "Created On",
  service: "Service",
  orders: "Orders",
  revenue: "Revenue",
  clicks: "Clicks",
  avgReview: "Avg Review",
};

export interface PaginatedResult{
    data: GigRow[];
    totalPages: number;
}

export function Gigs (){
    const [id, setId] = useState<number | undefined>(undefined);
    const [keyword, setKeyword] = useState<string>('');
    const [subCategoryId, setSubCategoryId] = useState<number>(0);
    const [subSubCategoryId, setSubSubCategoryId] = useState<number>(0);
    const [priceRangeId, setPriceRangeId] = useState<number>(0);
    const [moderationStatusId, setModerationStatusId] = useState<number>(0);
    const [gigs, setGigs] = useState<GigRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const itemsPerPage = 2;
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);
    
    const handlePageChange = (page: number) => setCurrentPage(page);

    useEffect(() => {
        setCurrentPage(1);
    }
    , [id, keyword, subCategoryId, subSubCategoryId, priceRangeId, moderationStatusId]);

    const handleSubCategoryIdSelect = (id: number) => setSubCategoryId(id);
    const handleSubSubCategoryIdSelect = (id: number) => setSubSubCategoryId(id);
    const handleSelectedPriceRangeId = (id: number) => setPriceRangeId(id);
    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();
                if (id !== undefined) params.append("id", id.toString());
                if (keyword) params.append("keyword", keyword);
                params.append("CurrentPage", currentPage.toString());
                params.append("ItemsPerPage", itemsPerPage.toString());
                if (subCategoryId) params.append("SubCategoryId", subCategoryId.toString());
                if (subSubCategoryId) params.append("SubSubCategoryId", subSubCategoryId.toString());
                if (priceRangeId) params.append("PriceRangeId", priceRangeId.toString());
                if (moderationStatusId) params.append("ModerationStatusId", moderationStatusId.toString());
    
                const url = `https://localhost:7267/admin/gigs?${params.toString()}`;
    
                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Accept': '*/*',
                    },
                });
                if (!response.ok) {
                    throw new Error("Failed to fetch gigs");
                }
    
                const result: PaginatedResult = await response.json();
                setGigs(result.data);
                setTotalPages(result.totalPages);
            } catch (error) {
                console.error("Error fetching gigs:", error);
            } finally {
                setLoading(false);
            }
        }, [id, keyword, currentPage, subCategoryId, subSubCategoryId, priceRangeId, moderationStatusId]);

    useEffect(() => {
        fetchGigs();
    }
    , [id, keyword, currentPage, subCategoryId, subSubCategoryId, priceRangeId, moderationStatusId, fetchGigs]);

    const handleOnDeactivateSuccess = () => {
        setKeyword('');
        setId(undefined);
        setCurrentPage(1);
        setSubCategoryId(0);
        setSubSubCategoryId(0);
        setPriceRangeId(0);
        setModerationStatusId(0);
        fetchGigs();
    }

    return(
        <AdminPage>
          <div className="filter-table">
              <PageTitle title="Gigs" description="View, filter, and manage all gigs on the platform. Approve, edit, or remove listings, monitor performance metrics, and ensure quality standards are met."breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/admin" }, { label: "Gigs" }]}/>         
              <div className="d-flex flex-column">
                 <GigsFilter setId={setId} setKeyword={setKeyword} setSelectedSubCategoryId={handleSubCategoryIdSelect} setSelectedSubSubCategoryId={handleSubSubCategoryIdSelect} setSelectedPriceRangeId={handleSelectedPriceRangeId} selectedSubCategoryId={subCategoryId} selectedSubSubCategoryId={subSubCategoryId} selectedPriceRangeId={priceRangeId} />
              </div>
              {loading ? (<div className="loading">Loading...</div>
              ) : (
                <><TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/admin/filters/moderation-status" /><div className="categories-list">
                <DataTable data={gigs} columns={["id", "createdOn", "service", "orders", "revenue", "clicks", "avgReview"]} headers={tableHeaders} renderActions={(row: GigRow) => (<GigActions gig={row} onDeactivateSuccess={handleOnDeactivateSuccess} />)} />
              </div></>
              )}
            </div>
            <div className="pagination-container">
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div>
        </AdminPage>
);

}