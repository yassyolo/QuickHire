import { ChangeEvent, useCallback, useEffect, useState } from "react";
import { EditModal } from "../Common/EditModal"; 
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";

export interface EditFilterModalProps {
  id: number;
  showModal: boolean;
  onClose: () => void;
  onEditSuccess(id: number, name: string): void;
  onEditSuccessAndClose() : void;
  name: string; 
}

export function EditFiterModal({onEditSuccess, showModal, id, onClose, name, onEditSuccessAndClose} : EditFilterModalProps) {
  const [newName, setNewName] = useState<string>("");
  const [showNameTooltip, handleShowNameTooltip] = useTooltip();
  const [validationErrors, setValidationErrors] = useState<{ Name?: string[];}>({});

  useEffect(() => {
    if (showModal) {
      setNewName(name);
      setValidationErrors({});
    }
  }, [showModal, name]);

  const handleNameChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => { setNewName(event.target.value); }, []);


  const onEditSuccessInternal = useCallback(async () => {
    setValidationErrors({});
    try {
         const url = `https://localhost:7267/admin/sub-sub-categories/filters`;

    const formData = new FormData();
    formData.append("Id", id.toString());
    formData.append("Name", newName ); 
    await axios.put(url, formData);
    
          onEditSuccess(id, newName);
          onClose();
          onEditSuccessAndClose();
        }  catch (error: unknown) {
      if (isAxiosError(error) && error.response?.status === 400) {
        setValidationErrors({
          Name: error.response.data.errors?.Name || [],
        });
      } else {
        console.error("Unexpected error editing category:", error);
      }
  }}
  , [newName, id, onEditSuccess]);

  if (!showModal) return null;

  return (
    <EditModal onClose={onClose} id={id} onContinue={onEditSuccessInternal}>
      <FormGroup id={"gig-filter-name"} label={"New Name"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={newName} onChange={handleNameChange} placeholder={"Enter Name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} error={validationErrors.Name}></FormGroup>
    </EditModal>
  );
};
