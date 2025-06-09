import { useEffect, useState } from "react";
import { DeactivateModal } from "./Common/DeactivatePossibleModal";
import axios from "axios";
import { DeactivateModalNotPossible } from "./Common/DeactivateNotPossibleModal";

export interface DeactivateMainCategoryModalProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

export function DeactivateGigFilterModal({showModal, onClose, onDeactivateSuccess, id,}: DeactivateMainCategoryModalProps) {
  const [reason, setReason] = useState<string>("");
  const [affectedItems, setAffectedItems] = useState<string[]>([]);
  const [showReasonError, setShowReasonError] = useState<string[]>([]);

  useEffect(() => {
    if(!showModal) {
      setReason("");
      setAffectedItems([]);
      setShowReasonError([]);
      return;
    }

    const fetchMainCategoryForDelete = async () => {
      try {
        const url = `https://localhost:7267/admin/sub-sub-categories/filters/delete/${id}`;

        const response = await axios.get<string[]>(url);
        setAffectedItems(response.data);
      } catch (error) {
        console.error("Error fetching Gig Filters", error);
      }
    }

    fetchMainCategoryForDelete();
  }, [id, showModal]);

  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/admin/sub-sub-categories/filters/delete`;

      await axios.delete(url, { data: { id, reason}});
      onDeactivateSuccess(id);
      onClose();
      setAffectedItems([]);
    } catch (error: unknown) {
      if(axios.isAxiosError(error) && error.response && error.response.status === 400) {
        setShowReasonError(error.response.data.errors?.Reason || []);
      }
      else {
        console.error("Error deleting Main Category:", error);
      }
    }
  };

 return (
  <>
    {showModal && (
      (affectedItems?.length ?? 0) > 0 ? (
        <DeactivateModalNotPossible id={id} onClose={onClose} affectedItems={"Filters"}>
            {affectedItems.map((item, index) => (
                <li key={index}>{item}</li>
            ))}
        </DeactivateModalNotPossible>
      ) : (
        <DeactivateModal error={showReasonError} id={id} onClose={onClose} onDeactivateSuccess={handleContinue} reason={reason} setReason={setReason}/>
      )
    )}
  </>
);

}

