import { useEffect, useState } from "react";
import { DeactivateModal } from "../Common/DeactivatePossibleModal/DeactivatePossibleModal";
import { DeactivateSubSubCategoryNotPossible } from "./DeactivateSubSubCategoryNotPossible";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
interface DeactivateSubSubCategoryModalProps {
    onClose: () => void;
    onDeactivateSuccess: (id: number) => void;
    showModal: boolean;
    id: number;
}

interface SubSubCategoryForDelete {
    id: number;
    gigs: number;
    subSubCategoryFilters: FiltersInSubSubCategory[] | undefined;
}

export interface FiltersInSubSubCategory {
    id: number;
    title: string;
    items: FilterItem[];
}

export interface FilterItem {
    id: number;
    value: string;
}

export function DeactivateSubSubCategoryModal({showModal, onClose, onDeactivateSuccess, id,}: DeactivateSubSubCategoryModalProps) {
  const [reason, setReason] = useState<string>("");
  const [subSubCategory, setSubSubCategory] = useState<SubSubCategoryForDelete | null>(null);
  const [showReasonError, setShowReasonError] = useState<string[]>([]);

    useEffect(() => {
    if(!showModal) {
      setReason("");
      setSubSubCategory(null);
      setShowReasonError([]);
      return;
    }

    const fetchSubCategoryForDelete = async () => {
      try {
        const url = `https://localhost:7267/admin/sub-sub-categories/delete/${id}`;

        const response = await axios.get<SubSubCategoryForDelete>(url);
        setSubSubCategory(response.data);
        console.log("Fetched Sub Sub Category:", response.data);
      } catch (error) {
        console.error("Error fetching Sub Sub Category:", error);
      }
    }

    fetchSubCategoryForDelete();
  }, [id, showModal]);

  const handleContinue = async () => {
    try {
      const url = `https://localhost:7267/admin/sub-sub-categories`;

      await axios.delete(url, { data: { id, reason}});
      onDeactivateSuccess(id);
      onClose();
      setSubSubCategory(null);
    } catch (error: unknown) {
      if(isAxiosError(error) && error.response && error.response.status === 400) {
        setShowReasonError(error.response.data.errors?.Reason || []);
      }
      else {
        console.error("Error deleting Sub Sub Category:", error);
      }
    }
  };

 return (
  <>
    {showModal && subSubCategory && (
      (subSubCategory.subSubCategoryFilters?.length ?? 0) > 0 ? (
        <DeactivateSubSubCategoryNotPossible id={id} onClose={onClose} filters={subSubCategory.subSubCategoryFilters ?? []}/>
      ) : (
        <DeactivateModal id={id} onClose={onClose} onDeactivateSuccess={handleContinue} reason={reason} setReason={setReason} error={showReasonError}        />
      )
    )}
  </>
);

}

