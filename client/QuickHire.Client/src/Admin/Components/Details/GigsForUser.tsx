import { useCallback, useEffect, useState } from "react";


import { GigRow, PaginatedResult } from "../../Pages/Gigs";
import { DataTable } from "../Tables/Common/AdminDataTable";
import { Pagination } from "../../../Shared/Pagination/Pagination";

const tableHeaders = {
  id: "ID",
  createdOn: "Created On",
  service: "Service",
  orders: "Orders",
  revenue: "Revenue",
  clicks: "Clicks",
  avgReview: "Avg Review",
};

export function GigsForUser (){
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
                params.append("UserId", true.toString());
    
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
            } 
        }, [currentPage]);

    useEffect(() => {
        fetchGigs();
    }
    , [currentPage, fetchGigs]);

    return(
        <>
            <div className="categories-list">
              <DataTable data={gigs} columns={["id", "createdOn", "service", "orders", "revenue", "clicks", "avgReview"]} headers={tableHeaders}/>
            </div>
            <div className="pagination-container">
               <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div>
        </>
);

}