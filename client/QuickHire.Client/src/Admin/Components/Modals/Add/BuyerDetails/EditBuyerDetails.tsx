import { ChangeEvent, useCallback, useEffect, useState } from "react";
import axios from "../../../../../axiosInstance";
import { isAxiosError } from "axios";
import { ImagePreview } from "../../../../../Shared/Images/ImagePreview/ImagePreview";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { Modal } from "../../Common/Modal";
import { ModalActions } from "../../Common/ModalActions";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";

export interface EditBuyerDetailsModalProps {
  showModal: boolean;
  onClose: () => void;
  onEditBuyerDetailsSuccess: (imageUrl: string, description: string) => void;
  initialDescription: string;
  initialImageUrl: string;
}

export function EditBuyerDetailsModal({
  onClose,
  onEditBuyerDetailsSuccess,
  showModal,
  initialDescription,
  initialImageUrl,
}: EditBuyerDetailsModalProps) {
  const [description, setDescription] = useState<string>(initialDescription);
  const [image, setImage] = useState<File | null>(null);
  const [newImagePreviewUrl, setNewImagePreviewUrl] = useState<string>(initialImageUrl || "");
  const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
  const [showImageTooltip, handleShowImageTooltip] = useTooltip();
  const [validationErrors, setValidationErrors] = useState<{ Description?: string[]; Image?: string[] }>({});

  useEffect(() => {
    if (showModal) {
      setDescription(initialDescription);
      setNewImagePreviewUrl(initialImageUrl || "");
      setImage(null);
      setValidationErrors({});
    }
  }, [showModal, initialDescription, initialImageUrl]);

  const handleImageInputChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      setImage(file);
      const reader = new FileReader();
      reader.onloadend = () => setNewImagePreviewUrl(reader.result as string);
      reader.readAsDataURL(file);
    }
  }, []);

  const handleEditBuyerDetails = async () => {
    try {
      const formData = new FormData();
      formData.append("description", description);
      if (image) formData.append("image", image);

      const result = await axios.put("https://localhost:7267/buyers/profile", formData);

      if (result.status === 200) {
        onEditBuyerDetailsSuccess(result.data.imageUrl, description);
        onClose();
      }
    } catch (error) {
      if (isAxiosError(error) && error.response?.status === 400) {
        setValidationErrors({
          Description: error.response.data.errors?.Description || [],
          Image: error.response.data.errors?.Image || [],
        });
      } else {
        console.error("Error editing buyer details:", error);
      }
    }
  };

  const handleDescriptionChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setDescription(event.target.value);
    setValidationErrors((prev) => ({ ...prev, Description: [] }));
  };

  if (!showModal) return null;

  return (
    <Modal>
                <div aria-label="Add modal" className="modal-title">Edit details</div>
                <div id="modal-body" className="modal-body">
                    <FormGroup
        error={validationErrors.Description}
        id={"description"}
        label={"Tell us about yourself"}
        type={"text"}
        value={description}
        onChange={handleDescriptionChange}
        placeholder={"Enter Description"}
        ariaDescribedBy={"description-help"}
        onShowTooltip={handleShowDescriptionTooltip}
        showTooltip={showDescriptionTooltip}
        tooltipDescription={"Briefly introduce yourself or your project. Highlight key details to help others understand your background or needs."}
      />
      <FormGroup
        error={validationErrors.Image}
        id={"profile-image"}
        label={"Image"}
        type={"file"}
        onChange={handleImageInputChange}
        placeholder={"Upload Image"}
        ariaDescribedBy={"image-help"}
        onShowTooltip={handleShowImageTooltip}
        showTooltip={showImageTooltip}
        tooltipDescription={"Upload a relevant image for your profile. Ensure it's clear and represents you accurately."}
      />
      {newImagePreviewUrl && <ImagePreview alt={"Preview"} src={newImagePreviewUrl} />}
                </div>
                <ModalActions id={"add-main-category-actions"}>
                    <ActionButton text="Back" onClick={onClose} className="back-button" ariaLabel="Back Button"/>
                    <ActionButton text="Continue" onClick={handleEditBuyerDetails} className="continue-button" ariaLabel="Continue Button"/>
                </ModalActions>           
            </Modal>
  );
}
