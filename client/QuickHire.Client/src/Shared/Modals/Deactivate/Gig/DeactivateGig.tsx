
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";import { DeactivateModal } from "../Common/DeactivatePossibleModal/DeactivatePossibleModal";
import { useState } from "react";

interface DeactivateFavouriteListProps {
    id: number;
    deactivateGig: (id: number) => void;
    onClose: () => void;
}


export function DeactivateGig({ id, deactivateGig, onClose }: DeactivateFavouriteListProps) {
    const [reason, setReason] = useState<string>("");
    const [showReasonError, setShowReasonError] = useState<string[]>([]);
    const handleDeactivate = async () => {
    try {
        const url = `https://localhost:7267/admin/gigs/deactivate`;
        const params = new URLSearchParams();
        params.append("Id", id.toString());
        params.append("Reason", reason);
        
        await axios.post(url + "?" + params.toString());
        deactivateGig(id);
    }
    catch (error: unknown) {
        if (isAxiosError(error) && error.response && error.response.status === 400) {
            setShowReasonError(error.response.data.errors?.Reason || []);
        } else {
            console.error("Error deactivating gig:", error);
        }
    }
};



    return (
        <DeactivateModal id={id} onClose={onClose} onDeactivateSuccess={handleDeactivate} reason={reason} setReason={setReason} error={showReasonError}        />      
    );
}