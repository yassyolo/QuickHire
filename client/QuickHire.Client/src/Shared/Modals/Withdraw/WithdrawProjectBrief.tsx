import axios from "../../../axiosInstance";
import { Modal } from "../Common/Modal";
import { ModalActions } from "../Common/ModalActions";
import { ActionButton } from "../../Buttons/ActionButton/ActionButton";

export interface DeleteGigProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

export function WithdrawProjectBrief({showModal, onClose, onDeactivateSuccess, id,}: DeleteGigProps) {
  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/buyers/project-briefs/delete/${id}`;

      await axios.delete(url);
      onDeactivateSuccess(id);
      onClose();
    } catch (error: unknown) {
      
        console.error("Error deleting Gig:", error);
    }
  };

 return (
  <>
    {showModal && 
      
         <Modal>
                    <div aria-label="deactivate-modal" className="modal-title">Are you sure you want to withdraw this project brief?</div>
                    <div id="modal-info" className="modal-info">This action is permanent and cannot be undone.</div>
                    <ModalActions id={"deactivate-main-category-actions"}>
                        <ActionButton text={"Back"} onClick={onClose} className={"back-button"} ariaLabel={"Back Button"} />
                        <ActionButton text={"Continue"} onClick={handleContinue} className={"continue-button"} ariaLabel={"Continue Button"} />
                    </ModalActions>               
        </Modal>
      
    }
  </>
);

}

