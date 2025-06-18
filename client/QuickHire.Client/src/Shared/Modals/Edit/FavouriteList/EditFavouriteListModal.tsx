import { ChangeEvent, useEffect, useState, useCallback } from "react";
import { EditModal } from "../Common/EditModal"; 
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
export interface EditFavouriteListModalProps {
  id: number;
  showModal: boolean;
  onClose: () => void;
  onEditSuccess(id: number, name: string, description: string): void;
  initialName: string;
  initialDescription: string;
}

export interface MainCategoryForEdit {
  id: number;
  name: string;
  description: string;
}

export function EditFavouriteListModal({ onEditSuccess, showModal, id, onClose, initialName, initialDescription }: EditFavouriteListModalProps) {
  const [form, setForm] = useState({ name: "", description: "" });
  const [showNameTooltip, handleShowNameTooltip] = useTooltip();
  const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
  const [validationErrors, setValidationErrors] = useState<{ Name?: string[]; Description?: string[] }>({});

  useEffect(() => {
  if (showModal) {
    setForm({ name: initialName, description: initialDescription });
    setValidationErrors({});
  }
}, [showModal, initialName, initialDescription]);

  const handleNameChange = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => { setForm(prev => ({ ...prev, name: e.target.value })); }, []);
  const handleDescriptionChange = useCallback((e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) =>  { setForm(prev => ({ ...prev, description: e.target.value }));}, []);
  const onEditSuccessInternal = useCallback(async () => {
    setValidationErrors({});
    try {
     await axios.put(`https://localhost:7267/buyers/favourite-gigs`, {
        id,
        name: form.name,
        description: form.description,
      });

      onClose();
      onEditSuccess(id, form.name, form.description);
      setForm({ name: "", description: "" });
    } catch (error: unknown) {
      if (isAxiosError(error) && error.response?.status === 400) {
        setValidationErrors({
          Name: error.response.data.errors?.Name || [],
          Description: error.response.data.errors?.Description || [],
        });
      } else {
        console.error("Unexpected error editing favourite list:", error);
      }
    } 
  }, [form, id, onClose, onEditSuccess]);

  if (!showModal) return null;

  return (
    <EditModal onClose={onClose} id={id} onContinue={onEditSuccessInternal} showId={false}>
        <FormGroup id={"name"} label={"List name"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={form.name} onChange={handleNameChange} placeholder={"Enter new list name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} error={validationErrors.Name || []} />
            <FormGroup id={"description"} label={"Description"} tooltipDescription={"Provide a brief description of the list."} type={"text"} value={form.description} onChange={handleDescriptionChange} placeholder={"Enter list description"} ariaDescribedBy={"description-help"} onShowTooltip={handleShowDescriptionTooltip} showTooltip={showDescriptionTooltip} error={validationErrors.Description || []} />
    </EditModal>
  );
}
