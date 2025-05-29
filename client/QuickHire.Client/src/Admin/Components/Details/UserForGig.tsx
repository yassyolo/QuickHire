import { useCallback, useEffect, useState } from "react";
import { PaginatedResult, UserRow } from "../../Pages/Users";
import { DataTable } from "../Tables/Common/AdminDataTable";

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

interface UserForGigProps {
    userId: string;
}

export function UserForGig ({userId}: UserForGigProps) {
    const [user, setUser] = useState<UserRow[]>([]);
    const currentPage = 1;
    const itemsPerPage = 1;

    const fetchUser = useCallback(async () => {
            try {
                const params = new URLSearchParams();
                params.append("UserId", userId);               
                params.append("CurrentPage", currentPage.toString());
                params.append("ItemsPerPage", itemsPerPage.toString());
    
                const url = `https://localhost:7267/admin/users?${params.toString()}`;
    
                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Accept': '*/*',
                    },
                });
                if (!response.ok) {
                    throw new Error("Failed to fetch users");
                }
    
                const result: PaginatedResult = await response.json();
                setUser(result.data);
            } catch (error) {
                console.error("Error fetching user:", error);
            } 
        }, [currentPage]);

    useEffect(() => {
        fetchUser();
    }
    , [ currentPage, fetchUser]);

    return(
            <div className="user-for-gig">
                <DataTable data={user} columns={["id", "joined", "username", "role", "status", "revenue"]} headers={tableHeaders} />            
            </div>
);

}