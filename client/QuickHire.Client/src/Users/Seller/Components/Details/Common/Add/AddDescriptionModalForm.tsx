import { useCallback, useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { AddDetailsModal } from "./AddDetailsForm";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";
import { NewAddedDescription } from "./NewAddedItems/Description/NewAddedDescription";
import "./AddDescriptionModalForm.css";


interface AddDescriptionModalFormProps {
    onSuccess: (description: string) => void;
}

export function AddDescriptionModalForm({onSuccess}: AddDescriptionModalFormProps) {
    const [description, setDescription] = useState<string>("");
    const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ Description?: string[] }>({});
    const [newDescription, setNewDescription] = useState<string>("");

    const handleDescriptionChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setDescription(event.target.value);
        setValidationErrors({ Description: [] });
    }, []);

    const onAdd = async () => {
        if (!description) {
            setValidationErrors({ Description: ["Description is required."] });
            return;
        }
        setNewDescription(description);
        setDescription("");
        }

    const handleOnSave = async () => {
        try{
            await axios.post("https://localhost:7267/seller/description", { description: newDescription });
            onSuccess(newDescription);
            setNewDescription("");

        }
        catch (error) {
            console.error("Error saving description:", error);
        }
    }
    const handleClear = () => {
        setDescription("");
        setNewDescription("");
        setValidationErrors({});
    }

    const handleRemoveNewDescription = () => {
        setNewDescription("");
        setValidationErrors({});
        setDescription("");
    }
    const handleEditNewDescription = () => {
    setDescription(newDescription); 
    setNewDescription(""); 
};
 
        return (
  <div className="add-form-wrapper">
    <AddDetailsModal onSave={onAdd}>
      <div className="d-flex flex-column gap-3">
        <FormGroup
          id={"description"}
          label={"Description"}
          tooltipDescription={"Enter a brief description of yourself."}
          type={"text"}
          value={description}
          onChange={handleDescriptionChange}
          placeholder={"Enter Description"}
          ariaDescribedBy={"description-help"}
          onShowTooltip={handleShowDescriptionTooltip}
          showTooltip={showDescriptionTooltip}
          error={validationErrors.Description || []}
        />
      </div>

     
    </AddDetailsModal>

     {newDescription?.trim() && (
        <div className="new-descriptions">
          <NewAddedDescription
            description={newDescription}
            onRemove={handleRemoveNewDescription}
            onEdit={handleEditNewDescription}
          />
        </div>
      )}

    <div className="description-buttons">
      <DetailsModalButtons onSave={handleOnSave} onClear={handleClear} />
    </div>
  </div>
);

    
}