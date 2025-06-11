import { useEffect, useState } from "react";
import { DeactivateModal } from "../Common/DeactivatePossibleModal/DeactivatePossibleModal";
import { DeactivateSubCategoryNotPossible } from "./DeactivateSubCategoryNotPossible";
import axios from "../../../../../axiosInstance";
import { isAxiosError } from "axios";
export interface DeactivateSubCategoryModalProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

export interface SubCategoryForDelete {
    id: number;
    subSubCategories: SubSubCategoriesInSubCategory[] | undefined;
}

export interface SubSubCategoriesInSubCategory {
    id: number;
    name: string;
}

export function DeactivateSubCategoryModal({showModal, onClose, onDeactivateSuccess, id,}: DeactivateSubCategoryModalProps) {
  const [reason, setReason] = useState<string>("");
  const [subCategory, setSubCategory] = useState<SubCategoryForDelete | null>(null);
  const [showReasonError, setShowReasonError] = useState<string[]>([]);

  useEffect(() => {
    if(!showModal) {
      setReason("");
      setSubCategory(null);
      setShowReasonError([]);
      return;
    }

    const fetchSubCategoryForDelete = async () => {
      try {
        const url = `https://localhost:7267/admin/sub-categories/delete/${id}`;

        const response = await axios.get<SubCategoryForDelete>(url);
        setSubCategory(response.data);
        console.log("Fetched Sub Category:", response.data);
      } catch (error) {
        console.error("Error fetching Sub Category:", error);
      }
    }

    fetchSubCategoryForDelete();
  }, [id, showModal]);

  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/admin/sub-categories`;

      await axios.delete(url, { data: { id, reason}});
      onDeactivateSuccess(id);
      onClose();
      setSubCategory(null);
    } catch (error: unknown) {
      if(isAxiosError(error) && error.response && error.response.status === 400) {
        setShowReasonError(error.response.data.errors?.Reason || []);
      }
      else {
        console.error("Error deleting Sub Category:", error);
      }
    }
  };

 return (
  <>
    {showModal && subCategory && (
      (subCategory.subSubCategories?.length ?? 0) > 0 ? (
        <DeactivateSubCategoryNotPossible id={id} onClose={onClose} subSubCategories={subCategory.subSubCategories ?? []}/>
      ) : (
        <DeactivateModal id={id} onClose={onClose} onDeactivateSuccess={handleContinue} reason={reason} setReason={setReason} error={showReasonError}        />
      )
    )}
  </>
);

}

