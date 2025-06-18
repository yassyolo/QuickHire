import { useCallback, useEffect, useState } from "react";
import { UserRow } from "../../../Users/Users";
import { DataTable } from "../../../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { UserActions } from "../../../../../Shared/Tables/TableActions/Users/UserActions";
import { useNavigate } from "react-router-dom";
import axiosInstance from "../../../../../axiosInstance";

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
    
                const response = await axiosInstance.get<UserRow>(url);
                setUser(response.data);
                
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