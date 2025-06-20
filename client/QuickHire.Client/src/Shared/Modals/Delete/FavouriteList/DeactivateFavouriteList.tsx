import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";
import { Modal } from "../../Common/Modal";
import axios from "../../../../axiosInstance";
import { ModalActions } from "../../Common/ModalActions";
import "./DeactivateFavouriteList.css";

interface DeactivateFavouriteListProps {
    favouriteListId: number;
    deactivateFavouriteList: (id: number) => void;
    onClose: () => void;
    favouriteListTitle: string;
}


export function DeactivateFavouriteList({ favouriteListId, deactivateFavouriteList, onClose, favouriteListTitle }: DeactivateFavouriteListProps) {
    const handleDeactivate = async () => {
        try {
            const url = `https://localhost:7267/buyers/favourite-list/${favouriteListId}`;
            await axios.delete(url);
            deactivateFavouriteList(favouriteListId);
        }
        catch (error) {
            console.error("Error deactivating favourite list:", error);
        }
    }

    return (
        <Modal>
            <div className="deactivate-favourite-list-header">Deactivate {favouriteListTitle}</div>
            <p className="deactivate-favourite-list-description">Are you sure you want to deactivate this favourite list?</p>
            <ModalActions id={"deactivate-main-category-actions"}>
                            <ActionButton text={"Back"} onClick={onClose} className={"back-button"} ariaLabel={"Back Button"} />
                            <ActionButton text={"Continue"} onClick={handleDeactivate} className={"continue-button"} ariaLabel={"Continue Button"} />
                        </ModalActions>
        </Modal>
            
    );
}