import { ChangeEvent, useEffect, useState, useCallback } from "react";
import { EditModal } from "./Common/EditModal"; 
import axios from "axios";
import { FormGroup } from "../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../Shared/Forms/Common/Tooltips/Tooltip";

export interface EditMainCategoryModalProps {
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

export function EditMainCategoryModal({ onEditSuccess, showModal, id, onClose, initialName, initialDescription }: EditMainCategoryModalProps) {
  const [form, setForm] = useState({ name: "", description: "" });
  const [showNameTooltip, triggerNameTooltip] = useTooltip();
  const [showDescriptionTooltip, triggerDescriptionTooltip] = useTooltip();
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
     await axios.put(`https://localhost:7267/admin/main-categories/edit`, {
        id,
        name: form.name,
        description: form.description,
      });

      onClose();
      onEditSuccess(id, form.name, form.description);
      setForm({ name: "", description: "" });
    } catch (error: unknown) {
      if (axios.isAxiosError(error) && error.response?.status === 400) {
        setValidationErrors({
          Name: error.response.data.errors?.Name || [],
          Description: error.response.data.errors?.Description || [],
        });
      } else {
        console.error("Unexpected error editing category:", error);
      }
    } 
  }, [form, id, onClose, onEditSuccess]);

  if (!showModal) return null;

  return (
    <EditModal onClose={onClose} id={id} onContinue={onEditSuccessInternal}>
      <FormGroup error={validationErrors.Name || []} id="new-main-category-name" label="New name" tooltipDescription="Use a clear, descriptive name." type="text" value={form.name} onChange={handleNameChange} placeholder="Enter Name" ariaDescribedBy="name-help" onShowTooltip={triggerNameTooltip} showTooltip={showNameTooltip}/>
      <FormGroup error={validationErrors.Description || []} id="new-main-category-description" label="New description" tooltipDescription="Write a short, catchy description that grabs attention." type="text" value={form.description} onChange={handleDescriptionChange} placeholder="Enter Description" ariaDescribedBy="description-help" onShowTooltip={triggerDescriptionTooltip} showTooltip={showDescriptionTooltip}/>
    </EditModal>
  );
}
