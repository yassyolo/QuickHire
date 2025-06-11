
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";import { DeactivateModal } from "./Common/DeactivatePossibleModal/DeactivatePossibleModal";
import { useState } from "react";

interface DeactivateFavouriteListProps {
    id: string;
    deactivateUser: (id: string) => void;
    onClose: () => void;
}


export function DeactivateUser({ id, deactivateUser, onClose }: DeactivateFavouriteListProps) {
    const [reason, setReason] = useState<string>("");
    const [showReasonError, setShowReasonError] = useState<string[]>([]);
    const handleDeactivate = async () => {
        try {

            const url = `https://localhost:7267/admin/users/deactivate`;
             await axios.post(url, { id, reason});
            deactivateUser(id);
        }
        catch (error : unknown) {
            if (isAxiosError(error) && error.response && error.response.status === 400) {
                setShowReasonError(error.response.data.errors?.Reason || []);
            } else {
                console.error("Error deactivating user:", error);
            }


    }
}

    return (
        <DeactivateModal id={id} onClose={onClose} onDeactivateSuccess={handleDeactivate} reason={reason} setReason={setReason} error={showReasonError}        />      
    );
}