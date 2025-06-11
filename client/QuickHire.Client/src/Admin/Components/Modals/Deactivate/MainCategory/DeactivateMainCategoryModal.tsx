import { useEffect, useState } from "react";
import { DeactivateModal } from "../Common/DeactivatePossibleModal/DeactivatePossibleModal";
import { DeactivateMainCategoryNotPossible } from "./DeactivateMainCategoryNotPossibleModal";
import axios from "../../../../../axiosInstance";
import { isAxiosError } from "axios";
export interface DeactivateMainCategoryModalProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

export interface MainCategoryForDelete {
    id: number;
    subCategories: SubCategoriesInMainCategory[] | undefined;
}

export interface SubCategoriesInMainCategory {
    id: number;
    name: string;
}

export function DeactivateMainCategoryModal({showModal, onClose, onDeactivateSuccess, id,}: DeactivateMainCategoryModalProps) {
  const [reason, setReason] = useState<string>("");
  const [mainCategory, setMainCategory] = useState<MainCategoryForDelete | null>(null);
  const [showReasonError, setShowReasonError] = useState<string[]>([]);

  useEffect(() => {
    if(!showModal) {
      setReason("");
      setMainCategory(null);
      setShowReasonError([]);
      return;
    }

    const fetchMainCategoryForDelete = async () => {
      try {
        const url = `https://localhost:7267/admin/main-categories/delete/${id}`;

        const response = await axios.get<MainCategoryForDelete>(url);
        setMainCategory(response.data);
      } catch (error) {
        console.error("Error fetching Main Category:", error);
      }
    }

    fetchMainCategoryForDelete();
  }, [id, showModal]);

  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/admin/main-categories/delete`;

      await axios.delete(url, { data: { id, reason}});
      onDeactivateSuccess(id);
      onClose();
      setMainCategory(null);
    } catch (error: unknown) {
      if(isAxiosError(error) && error.response && error.response.status === 400) {
        setShowReasonError(error.response.data.errors?.Reason || []);
      }
      else {
        console.error("Error deleting Main Category:", error);
      }
    }
  };

 return (
  <>
    {showModal && mainCategory && (
      (mainCategory.subCategories?.length ?? 0) > 0 ? (
        <DeactivateMainCategoryNotPossible id={id} onClose={onClose} subCategories={mainCategory.subCategories ?? []}/>
      ) : (
        <DeactivateModal error={showReasonError} id={id} onClose={onClose} onDeactivateSuccess={handleContinue} reason={reason} setReason={setReason}/>
      )
    )}
  </>
);

}

