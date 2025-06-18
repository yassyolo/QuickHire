import {  useCallback, useEffect, useState } from "react";
import {Pagination} from "../../../Shared/PageItems/Pagination/Pagination/Pagination";
import { DataTable } from "../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { PageTitle } from "../../../Shared/PageItems/PageTitle/PageTitle";
import { UsersFilter } from "../PageFilters/UsersFilter";
import { TitleFilterSelector } from "../../../Shared/PageItems/TitleFilterSection/TitleFilterSection";
import { UserActions } from "../../../Shared/Tables/TableActions/Users/UserActions";
import axios from "../../../axiosInstance";
export interface UserRow{
  id: string;
  joined: string;
  username: string;
  roles: string;
  country: number;
  status: string;
}

const tableHeaders = {
  id: "ID",
  joined: "Joined",
  username: "Username",
  roles: "Role(s)",
    country: "Country",
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
    const [selectedRoleId, setSelectedRoleId] = useState<string | undefined>(undefined);
    const [moderationStatusId, setModerationStatusId] = useState<number>(0);
    const [gigs, setGigs] = useState<UserRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const itemsPerPage = 10;
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);
    
    const handlePageChange = (page: number) => setCurrentPage(page);

    useEffect(() => {
        setModerationStatusId(1);
        setCurrentPage(1);
    }
    , [id, keyword, moderationStatusId, selectedRoleId]);

    const handleSelectedModerationStatusId = (id: number) => setModerationStatusId(id);
    const handleSelectedRoleId = (id: string) => setSelectedRoleId(id);

    const hanldeDeactivateSuccess = () => {
        setKeyword('');
        setId(undefined);
        setCurrentPage(1);
        fetchGigs();
    }

    const fetchGigs = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();
                if (id !== undefined) params.append("id", id.toString());
                if (keyword) params.append("keyword", keyword);
                params.append("CurrentPage", currentPage.toString());
                params.append("ItemsPerPage", itemsPerPage.toString());
                if (moderationStatusId) params.append("ModerationStatusId", moderationStatusId.toString());
                if(selectedRoleId) params.append("RoleId", selectedRoleId.toString());
    
                const url = `https://localhost:7267/admin/users?${params.toString()}`;
    
                const result = await axios.get<PaginatedResult>(url);
                
                setGigs(result.data.data);
                setTotalPages(result.data.totalPages);
            } catch (error) {
                console.error("Error fetching users:", error);
            } finally {
                setLoading(false);
            }
        }, [id, keyword, currentPage, selectedRoleId, moderationStatusId]);

    useEffect(() => {
        fetchGigs();
    }
    , [id, keyword, currentPage, moderationStatusId, selectedRoleId, fetchGigs]);

    return(
          <><div className="filter-table">
            <PageTitle title="Users" description="View, filter, manage and monitor user activity to maintain platform integrity." breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/admin" }, { label: "Users" }]} />
            <div className="d-flex flex-column">
                <UsersFilter setId={setId} setKeyword={setKeyword} setSelectedRoleId={handleSelectedRoleId} selectedRoleId={selectedRoleId}></UsersFilter>
            </div>
            {loading ? (<div className="loading">Loading...</div>
            ) : (
                <>
                    <TitleFilterSelector selectedId={moderationStatusId} setSelectedId={handleSelectedModerationStatusId} endpoint="https://localhost:7267/filters/moderation-status" />
                    <DataTable data={gigs} columns={["id", "joined", "username", "roles", "status"]} headers={tableHeaders} renderActions={(row: UserRow) => (<UserActions user={row} onDeactivateSuccess={hanldeDeactivateSuccess} />)} /></>
            )}
        </div><div className="pagination-container">
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div></>
);

}