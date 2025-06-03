import { useCallback, useState, useEffect } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";
import { NewAddedDescription } from "../Add/NewAddedItems/Description/NewAddedDescription";
import { AddDetailsModal } from "../Add/AddDetailsForm";

interface EditDescriptionModalFormProps {
  initialDescription: string;
  onEditSuccess: (newDescription: string) => void;
}

export function EditDescriptionModalForm({ initialDescription, onEditSuccess,}: EditDescriptionModalFormProps) {
  const [description, setDescription] = useState<string>("");
  const [newDescription, setNewDescription] = useState<string>("");
  const [validationErrors, setValidationErrors] = useState<{ Description?: string[] }>({});
  const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();

  useEffect(() => {
    setNewDescription(initialDescription);
  }, [initialDescription]);

  const handleDescriptionChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setDescription(event.target.value);
    setValidationErrors({ Description: [] });
  }, []);

  const onAdd = () => {
    if (!description.trim()) {
      setValidationErrors({ Description: ["Description is required."] });
      return;
    }
    setNewDescription(description);
    setDescription("");
  };

  const handleOnSave = async () => {
    try {
      await axios.put(`https://localhost:7267/seller/profile/description`, {
        description: newDescription,
      });
      onEditSuccess(newDescription);
    } catch (error) {
      console.error("Error editing description:", error);
    }
  };

  const handleClear = () => {
    setDescription("");
    setNewDescription(initialDescription);
    setValidationErrors({});
  };

  const handleRemoveNewDescription = () => {
    setNewDescription("");
    setDescription("");
    setValidationErrors({});
  };

  const handleEditNewDescription = () => {
    setDescription(newDescription);
    setNewDescription("");
  };

  return (
    <div className="add-form-wrapper">
      <AddDetailsModal onSave={onAdd}>
          <FormGroup
            id={"description"}
            label={"Description"}
            tooltipDescription={"Enter a brief description of yourself."}
            type={"text"}
            value={description}
            onChange={handleDescriptionChange}
            placeholder={"Edit Description"}
            ariaDescribedBy={"description-help"}
            onShowTooltip={handleShowDescriptionTooltip}
            showTooltip={showDescriptionTooltip}
            error={validationErrors.Description || []}
          />
      </AddDetailsModal>

              {newDescription?.trim() && (
          <div className="new-descriptions">
            <NewAddedDescription description={newDescription} onRemove={handleRemoveNewDescription} onEdit={handleEditNewDescription}/>
          </div>
        )}

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleOnSave} onClear={handleClear} />
      </div>
    </div>
  );
}
