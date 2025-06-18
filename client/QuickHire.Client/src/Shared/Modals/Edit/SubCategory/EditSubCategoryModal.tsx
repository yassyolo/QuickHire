import { ChangeEvent, useCallback, useEffect, useState } from "react";
import { EditModal } from "../Common/EditModal";
import { ImagePreview } from "../../../Images/ImagePreview/ImagePreview";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";

export interface EditSubCategoryModalProps {
    id: number;
    showModal: boolean;
    name: string;
    imageUrl: string;
    onClose: () => void;
    onEditSuccess: (id: number, newName: string, newImageUrl: string) => void;
}

export function EditSubCategoryModal({ onEditSuccess, showModal, id, onClose, name, imageUrl}: EditSubCategoryModalProps) {
    const [newName, setNewName] = useState<string>("");
    const [newImageFile, setNewImageFile] = useState<File | null>(null);
    const [newImagePreviewUrl, setNewImagePreviewUrl] = useState<string>("");
    const [showNameTooltip, handleShowNameTooltip] = useTooltip();
    const [showImageTooltip, handleShowImageTooltip] = useTooltip();
    const [nameValidationErrors, setNameValidationErrors] = useState<string[]>([]);

    const handleNameChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {setNewName(event.target.value);}, []);

    const handleImageChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const file = (event.target as HTMLInputElement).files?.[0];
        if (file) {
          setNewImageFile(file);
            
          const reader = new FileReader();
          reader.onloadend = () => setNewImagePreviewUrl(reader.result as string);
          reader.readAsDataURL(file);
        }
    }, []);  
    
    useEffect(() => {
    if (showModal) {
        setNewName(name);
        setNewImageFile(null);
        setNewImagePreviewUrl(imageUrl);
        setNameValidationErrors([]);
    }
}, [id, imageUrl, name, showModal]);


    const onEditSuccessInternal = useCallback(async () => {
     try {
          const url = `https://localhost:7267/admin/sub-categories`;

    const formData = new FormData();
    formData.append("Id", id.toString());
    formData.append("Name", newName ); 
if (newImageFile) {
  formData.append("Image", newImageFile);
}
    const result = await axios.put(url, formData);
    onClose();

    onEditSuccess(id, newName, result.data.imageUrl || newImagePreviewUrl);
    setNewName("");
    setNewImageFile(null);
    setNewImagePreviewUrl("");
  setNameValidationErrors([]);

    } catch (error: unknown) {
    if (isAxiosError(error) && error.response && error.response.status === 400) {
      setNameValidationErrors(error.response.data.errors?.Name || []);
    }
    else {
      console.error("Error editing Sub Category:", error);
    }
  }
}, [id, newName, newImageFile, onEditSuccess, newImagePreviewUrl, onClose]);


    return (
        <EditModal id={id} onClose={onClose} onContinue={onEditSuccessInternal}>
            <FormGroup error={nameValidationErrors} id={"new-sub-category-name"} label={"New Name"} value={newName} tooltipDescription={"Use a clear, descriptive name."} type={"text"} onChange={handleNameChange} placeholder={"Enter New Name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip}></FormGroup>
            <FormGroup id={"new-sub-category-image"} label={"New Image"} type={"file"} tooltipDescription={"Upload a relevant image for the sub-category. Ensure it's clear and represents the category accurately."} onChange={handleImageChange} placeholder={"Select New Image"} ariaDescribedBy={"image-help"} onShowTooltip={handleShowImageTooltip} showTooltip={showImageTooltip}></FormGroup>

        <div className="image-preview">
        {(newImagePreviewUrl) && (
            <ImagePreview src={newImagePreviewUrl} alt="Preview"  /> )}
        </div>
        </EditModal>
    );

}