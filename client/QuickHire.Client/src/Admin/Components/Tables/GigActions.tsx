import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { DeactivateModal } from "../Modals/Deactivate/Common/DeactivatePossibleModal";
import axios from "axios";
import { CategoriesActionButton } from "./CategoriesActionButton";

export interface GigActionsProps {
    gig: { id: number };
}

export function GigActions ({gig}: GigActionsProps) {
    const [showDeactivateModal, setShowDeactivateModal] = useState(false);
    const [reason, setReason] = useState("");
    const [error, setShowReasonError] = useState<string[]>([]);
    const {id} = gig;
    const navigate = useNavigate();

    const handleDeactivateModalVisibility = () => setShowDeactivateModal(!showDeactivateModal);
    const handleSeeButtonClick = () => navigate(`/admin/gigs/${id}`);

    useEffect(() => {
        if (!showDeactivateModal) {
            setReason("");
            setShowReasonError([]);          
        }
    }, [showDeactivateModal]);

    const onDeactivateSuccess = () => {
        try{
            const url = `https://localhost:7267/admin/gigs`;
            axios.delete(url, {data: { reason }});
            setShowDeactivateModal(false);
            navigate("/admin/gigs");

        }catch (error : unknown){
            if(axios.isAxiosError(error) && error.response && error.response.status === 400) {
            setShowReasonError(error.response.data.errors?.Reason || []);
           }
          else {
          console.error("Error deactivateng gig:", error);
         }
       };
}

    return (
        <>
            <CategoriesActionButton onSeeButtonClick={handleSeeButtonClick} onDeactivateModalVisibility={handleDeactivateModalVisibility}></CategoriesActionButton>           
            {showDeactivateModal && (<DeactivateModal id={id} onClose={handleDeactivateModalVisibility} onDeactivateSuccess={onDeactivateSuccess} reason={reason} setReason={setReason} error={error}/>)}
        </>
    );
};
