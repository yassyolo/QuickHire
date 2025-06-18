import { useEffect, useState } from "react";
import axios from "../../../../axiosInstance";
import { DeactivateModalNotPossible } from "../../Deactivate/Common/DeactivateNotPossibleModal/DeactivateNotPossibleModal";
import { Modal } from "../../Common/Modal";
import { ModalActions } from "../../Common/ModalActions";
import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";

export interface DeleteGigProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

export function DeleteGigModal({showModal, onClose, onDeactivateSuccess, id,}: DeleteGigProps) {
  const [affectedItems, setAffectedItems] = useState<string[]>([]);

  useEffect(() => {
    if(!showModal) {
      setAffectedItems([]);
      return;
    }

    const fetchGigForDelete = async () => {
      try {
        const url = `https://localhost:7267/seller/gigs/delete/${id}`;

        const response = await axios.get<string[]>(url);
        setAffectedItems(response.data);
      } catch (error) {
        console.error("Error fetching Gig", error);
      }
    }

    fetchGigForDelete();
  }, [id, showModal]);

  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/seller/gigs/delete/${id}`;

      await axios.delete(url);
      onDeactivateSuccess(id);
      onClose();
      setAffectedItems([]);
    } catch (error: unknown) {
      
        console.error("Error deleting Gig:", error);
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
         <Modal>
                    <div aria-label="deactivate-modal" className="modal-title">Are you sure you want to delete this gig?</div>
                    <div id="modal-info" className="modal-info">This action is permanent and cannot be undone.</div>
                    <ModalActions id={"deactivate-main-category-actions"}>
                        <ActionButton text={"Back"} onClick={onClose} className={"back-button"} ariaLabel={"Back Button"} />
                        <ActionButton text={"Continue"} onClick={handleContinue} className={"continue-button"} ariaLabel={"Continue Button"} />
                    </ModalActions>               
        </Modal>
      )
    )}
  </>
);

}

