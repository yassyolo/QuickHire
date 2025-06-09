import { useCallback, useEffect, useState } from "react";
import { UserRow } from "../../../Pages/Users";
import { DataTable } from "../../Tables/Common/AdminDataTable";
import { UserActions } from "../../Tables/TableActions/UserActions";
import { useNavigate } from "react-router-dom";

const tableHeaders = {
  id: "ID",
  joined: "Joined",
  username: "Username",
  roles: "Roles",
    country: "Country",
  status: "Status",
    actions: "Actions",
};

interface UserForGigProps {
    gigId: number;
}

export function UserForGig ({gigId}: UserForGigProps) {
    const [user, setUser] = useState<UserRow>();
    const navigate = useNavigate();

    const handleOnDeactivateSuccess = (id: string) => {
        console.log(`User with ID ${id} has been deactivated successfully.`);
        navigate("/admin/users");
    };

    const fetchUser = useCallback(async () => {
            try {
                const params = new URLSearchParams();
                params.append("Id", gigId.toString());               
                const url = `https://localhost:7267/admin/gigs/seller?${params.toString()}`;
    
                const response = await fetch(url, {
                    method: 'GET',
                    headers: {
                        'Accept': '*/*',
                    },
                });
                if (!response.ok) {
                    throw new Error("Failed to fetch users");
                }
                    const result: UserRow = await response.json();
                setUser(result);
            } catch (error) {
                console.error("Error fetching user:", error);
            } 
        }, []);

    useEffect(() => {
        fetchUser();
    }
    , [fetchUser]);

    return(
            <div className="user-for-gig">
                    <DataTable data={user ? [user] : []} columns={["id", "joined", "username", "roles", "status"]} headers={tableHeaders} renderActions={(row: UserRow) => (<UserActions user={row} onDeactivateSuccess={handleOnDeactivateSuccess} />)} />           
            </div>
);

}