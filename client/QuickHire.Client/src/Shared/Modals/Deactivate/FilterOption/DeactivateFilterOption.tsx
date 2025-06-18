import { useEffect, useState } from "react";
import { DeactivateModal } from "../Common/DeactivatePossibleModal/DeactivatePossibleModal";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";

import { DeactivateModalNotPossible } from "../Common/DeactivateNotPossibleModal/DeactivateNotPossibleModal";

export interface DeactivateMainCategoryModalProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

export function DeactivateFilterOptionsModal({showModal, onClose, onDeactivateSuccess, id,}: DeactivateMainCategoryModalProps) {
  const [reason, setReason] = useState<string>("");
  const [affectedItems, setAffectedItems] = useState<string>("");
  const [showReasonError, setShowReasonError] = useState<string[]>([]);

  useEffect(() => {
    if(!showModal) {
      setReason("");
      setAffectedItems("");
      setShowReasonError([]);
      return;
    }

    const fetchMainCategoryForDelete = async () => {
      try {
        const url = `https://localhost:7267/admin/sub-sub-categories/filters/options/delete/${id}`;

        const response = await axios.get<string>(url);
        setAffectedItems(response.data);
      } catch (error) {
        console.error("Error fetching Gig Filters", error);
      }
    }

    fetchMainCategoryForDelete();
  }, [id, showModal]);

  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/admin/sub-sub-categories/filters/options/delete`;

const formData = new URLSearchParams();
formData.append("Reason", reason);
formData.append("Id", id.toString());

await axios.delete(url, {
  data: formData,
  headers: {
    'Content-Type': 'application/x-www-form-urlencoded'
  }
});

      setShowReasonError([]);
      onDeactivateSuccess(id);
      onClose();
      setAffectedItems("");
    } catch (error: unknown) {
      if(isAxiosError(error) && error.response && error.response.status === 400) {
        setShowReasonError(error.response.data.errors?.Reason || []);
      }
      else {
        console.error("Error deleting Filter option", error);
      }
    }
  };

 return (
  <>
    {showModal && (
      (affectedItems?.length ?? 0) > 0 ? (
        <DeactivateModalNotPossible id={id} onClose={onClose} affectedItems={"gigs"}>
           {affectedItems} 
        </DeactivateModalNotPossible>
      ) : (
        <DeactivateModal error={showReasonError} id={id} onClose={onClose} onDeactivateSuccess={handleContinue} reason={reason} setReason={setReason}/>
      )
    )}
  </>
);

}

