import { useState } from "react";
import { TagList } from "../../../Gigs/Tags/TagList";
import { FAQList } from "../../../Shared/FAQ/FAQList";
import "./GigDetails.css";
import { ActionButton } from "../../../Shared/Buttons/ActionButton";
import { DeactivateModal } from "../Modals/Deactivate/Common/DeactivatePossibleModal";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export interface GigDetailsProps {
    id: number;
}

export function GigDetails({ id }: GigDetailsProps) {
    const [showDeactivateGigModal, setShowDeactivateGigModal] = useState(false);
    const [reason, setReason] = useState("");
    const navigator = useNavigate();
    const [reasonError, setReasonError] = useState<string[]>([]);

    const handleDeactivateGigModalVisibility = () => setShowDeactivateGigModal(!showDeactivateGigModal);

    const handleDeactivateSuccess = async (id?: number) => {
        if (typeof id !== "number" || isNaN(id)) return;
        try{
            const params = new URLSearchParams();
            params.append("id", id.toString());
            const url = `https://localhost:7267/admin/gigs/${params.toString()}`;
            const response = await axios.delete(url, {
                headers: {
                    'Accept': '*/*',
                },
            });
            if(response.status === 200){
                navigator("/admin/gigs");
                setShowDeactivateGigModal(false);
            }
        }
        catch (error : unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                setReasonError(error.response.data.errors?.Reason || []);
            }
        }
        
    };

    const handleReasonChange = (newReason: string) => {
        setReason(newReason);
    }

    return (
        <div className="gig-details-container" aria-label="gig-details">
            <div className="button-details d-flex d-row justify-content-between">
                <div className="gig-details d-flex flex-column">
                    <TagList gigId={id} />
                <div className="faqs-gig-details">
                  <FAQList gigId={id} showActions={false} title={"Gig"}/> 
                </div>
                </div>
                
                <ActionButton className="deactivate-button" onClick={handleDeactivateGigModalVisibility} text={"Deactivate"} ariaLabel={"Deactivate Gig Button"}/>
{showDeactivateGigModal && (
    <DeactivateModal id={id} reason={reason} onClose={handleDeactivateGigModalVisibility} onDeactivateSuccess={handleDeactivateSuccess} setReason={handleReasonChange} error={reasonError}></DeactivateModal>
)}
            </div>
            
            
        </div>
    );
}