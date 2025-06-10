import { useEffect, useState } from "react";

import { DataTable } from "../../Tables/Common/AdminDataTable";
import { IconButton } from "../../../../Shared/Buttons/IconButton/IconButton";
import { DeactivateUser } from "../../Modals/Deactivate/DeactivateUser";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import "./UserModerationStatus.css";

interface ModerationStatusRow{
    id: number;
    createdOn: string;
    reason: string;
    createdBy: string;
}

const tableHeaders = {
  id: "ID",
    createdOn: "Created On",
    reason: "Reason",
    createdBy: "Created By",
};

interface ModerationStatus{
    moderationStatus: ModerationStatusRow[];
    status: string;
}

interface UserModerationStatusProps {
    userId: string;
}

export function UserModerationStatus ({ userId }: UserModerationStatusProps) {
    const [moderationStatusDetails, setModerationStatusDetails] = useState<ModerationStatus | null>(null);
    const [showDeactivateModal, setShowDeactivateModal] = useState<boolean>(false);
    const navigate = useNavigate();

    const hanldeDeactivateSuccess = () => navigate("/admin/users");

    const handleDeactivateDeactivateModalVisibility = () => {
        setShowDeactivateModal(!showDeactivateModal);
    }

    useEffect(() => {
        const fetchModerationStatus = async () => {
            try {
                const url = `https://localhost:7267/admin/report`;
                const response = await axios.get<ModerationStatus>(url, {
                    params: { userId }}
                );
                if (response.status === 200) {
                    setModerationStatusDetails(response.data);
                } else {
                    console.error("Failed to fetch moderation status:", response.statusText);
                }
            } catch (error) {
                console.error("Error fetching moderation status:", error);
            }
        };

        fetchModerationStatus();
    }, [userId]);

    return(
        <>
            <div className="user-moderation d-flex flex-column" >
                <div className="d-flex flex-row user-moderation-top">
                    <div className="user-moderation-item">
                    <div className="user-moderation-item-label">Status</div>
                    <div className="user-moderation-item-value">{moderationStatusDetails ? moderationStatusDetails.status : "Loading..."}</div>
                </div>
                <div className="user-moderation-item">
                    <div className="user-moderation-item-label">Actions</div>
                    <div className="user-moderation-item-value"> <IconButton icon={<i className="bi bi-x" style={{fontSize: "20px", color: "red"}}></i>} onClick={handleDeactivateDeactivateModalVisibility} className="faq-delete-button" ariaLabel="Deactivate Category Button" /> </div>
                </div>
                </div>
                
                {moderationStatusDetails && moderationStatusDetails.moderationStatus.length > 0 && 
                <><div className="user-moderation-table-header">Reports</div><DataTable data={moderationStatusDetails.moderationStatus} columns={["id", "createdOn", "reason", "createdBy"]} headers={tableHeaders} /></>
                }

                {showDeactivateModal && <DeactivateUser id={userId} deactivateUser={hanldeDeactivateSuccess} onClose={handleDeactivateDeactivateModalVisibility}/>}
            </div>

        </>
);

}