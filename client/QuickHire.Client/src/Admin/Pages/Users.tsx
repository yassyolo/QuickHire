import { useCallback, useEffect, useState } from "react";
import {Pagination} from "../../Shared/Pagination/Pagination";
import { DataTable } from "../Components/Tables/Common/AdminDataTable";
import { AdminPage } from "./Common/AdminPage";
import { PageTitle } from "./Common/PageTitle";
import { UsersFilter } from "../Components/Filters/PageFilters/UsersFilter";
import { UserActions } from "../Components/Tables/UserActions";
import { TitleFilterSelector } from "./Common/TitleFilterSection";

export interface UserRow{
  id: string;
  joined: string;
  username: string;
  role: number;
  country: number;
  revenue: number;
  status: string;
}

const tableHeaders = {
  id: "ID",
  joined: "Joined",
  username: "Username",
  role: "Role",
    country: "Country",
    revenue: "Revenue",
  status: "Status",
    actions: "Actions",
};

export interface PaginatedResult{
    data: UserRow[];
    totalPages: number;
}

export function Users (){
    const [id, setId] = useState<number | undefined>(undefined);
    const [keyword, setKeyword] = useState<string | undefined>(undefined);
    const [selectedCountryId, setSelectedCountryId] = useState<number>(0);
    const [selectedRoleId, setSelectedRoleId] = useState<number>(0);
    const [moderationStatusId, setModerationStatusId] = useState<number>(0);
    const [gigs, setGigs] = useState<UserRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const itemsPerPage = 2;
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);
    
    const handlePageChange = (page: number) => setCurrentPage(page);

    useEffect(() => {
        setCurrentPage(1);
    }
    , [id, keyword, moderationStatusId]);

    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);
    const handleSelectedRoleId = (id: number) => setSelectedRoleId(id);
    const handleSelectedCountryId = (id: number) => setSelectedCountryId(id);

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();
                if (id !== undefined) params.append("id", id.toString());
                if (keyword) params.append("keyword", keyword);
                params.append("CurrentPage", currentPage.toString());
                params.append("ItemsPerPage", itemsPerPage.toString());
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
        }, [id, keyword, currentPage, moderationStatusId]);

    useEffect(() => {
        fetchGigs();
    }
    , [id, keyword, currentPage, moderationStatusId, fetchGigs]);

    return(
        <AdminPage>
          <div className="filter-table">
              <PageTitle title="Users"   description="View, filter, manage and monitor user activity to maintain platform integrity."breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/admin" }, { label: "Users" }]}/>         
              <div className="d-flex flex-column">
                <UsersFilter setId={setId} setKeyword={setKeyword} setSelectedRoleId={handleSelectedRoleId} setSelectedCountryId={handleSelectedCountryId} selectedRoleId={selectedRoleId} selectedCountryId={selectedCountryId}></UsersFilter>
              </div>
              {loading ? (<div className="loading">Loading...</div>
              ) : (
                <div className="categories-list">
                    <TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/admin/filters/moderation-status"/>
                    <DataTable data={gigs} columns={["id", "joined", "username", "role", "status", "revenue"]} headers={tableHeaders} renderActions= {(row: UserRow) => (<UserActions user={row} />)} />            
                </div>
              )}
            </div>
            <div className="pagination-container">
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div>
        </AdminPage>
);

}