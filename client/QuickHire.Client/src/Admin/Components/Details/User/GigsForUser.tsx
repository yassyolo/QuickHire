import { useCallback, useEffect, useState } from "react";


import { GigRow, PaginatedResult } from "../../../Pages/Gigs";
import { DataTable } from "../../Tables/Common/AdminDataTable";
import { Pagination } from "../../../../Shared/Pagination/Pagination/Pagination";
import { GigActions } from "../../Tables/TableActions/GigActions";

const tableHeaders = {
  id: "ID",
  createdOn: "Created On",
  service: "Service",
  orders: "Orders",
  revenue: "Revenue",
  clicks: "Clicks",
  avgReview: "Avg Review",
};
interface GigsForUserProps {
  userId: string;
}
export function GigsForUser ({ userId }: GigsForUserProps) {
    const [gigs, setGigs] = useState<GigRow[]>([]);
    const itemsPerPage = 2;
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);
    
    const handlePageChange = (page: number) => setCurrentPage(page);

    const fetchGigs = useCallback(async () => {
            try {
                const params = new URLSearchParams();               
                params.append("CurrentPage", currentPage.toString());
                params.append("ItemsPerPage", itemsPerPage.toString());
                if (userId) {
                    params.append("UserId", userId);
                }
    
                const url = `https://localhost:7267/admin/users/gigs?${params.toString()}`;
    
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
            } 
        }, [currentPage]);

    useEffect(() => {
        fetchGigs();
    }
    , [currentPage, fetchGigs]);

    const handleDeactivateSuccess = () => {
        setCurrentPage(1);
        setGigs((prev) => prev.filter(gig => gig.id !== gig.id));
    }

    return(
        <>
            <div className="categories-list d-flex flex-column" style={{gap: "10px"}}>
                                <DataTable data={gigs} columns={["id", "createdOn", "service", "orders", "revenue", "clicks", "avgReview"]} headers={tableHeaders} renderActions={(row: GigRow) => (<GigActions gig={row} onDeactivateSuccess={handleDeactivateSuccess} />)} />
            <div className="pagination-container">
               <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div>
                                        </div>

        </>
);

}