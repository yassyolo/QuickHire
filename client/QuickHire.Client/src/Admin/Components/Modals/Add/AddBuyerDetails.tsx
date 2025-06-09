import { ChangeEvent, useCallback, useEffect, useState } from "react";
import { AddModal } from "./Common/AddModal";
import axios from "axios";
import { ImagePreview } from "../../../../Shared/ImagePreview/ImagePreview";
import { FormGroup } from "../../../../Shared/Forms/FormGroup";
import { useTooltip } from "../../../../Shared/Tooltip/Tooltip";

export interface AddBuyerDetailsModalProps {
    showModal: boolean;
    onClose: () => void;
    onAddBuyerDetailsSuccess: (imageUrl: string, description : string) => void;
}


export function AddBuyerDetailsModal({onClose, onAddBuyerDetailsSuccess, showModal }: AddBuyerDetailsModalProps) {
    const [description, setDescription] = useState<string>("");
    const [image, setImage] = useState<File | null>(null);
    const [newImagePreviewUrl, setNewImagePreviewUrl] = useState<string>("");
    const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
    const [showImageTooltip, handleShowImageTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ Description?: string[]; Image?: string[];}>({});
  
    useEffect(() => {
        if (!showModal) {
            setDescription("");
            setImage(null);
            setNewImagePreviewUrl(""); 
            setValidationErrors({});     
        }
    }, [showModal]); 

    
    const handleImageInputChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const file = (event.target as HTMLInputElement).files?.[0];

        if (file) {
            setImage(file);
            const reader = new FileReader();
            reader.onloadend = () => setNewImagePreviewUrl(reader.result as string);
            reader.readAsDataURL(file);
        }
    }, []);

    if (!showModal) return null;


    const handleAddSubCategorySuccess = async () => {
        try {
            const formData = new FormData();

            formData.append("description", description);
            if (image) formData.append("image", image);
    
            const result = await axios.post("https://localhost:7267/buyers/profile", formData);
            if (result.status === 200) {
                const buyerDetails = {
                description: description,
                imageUrl: result.data.imageUrl
            };        
            onAddBuyerDetailsSuccess(buyerDetails.imageUrl, buyerDetails.description);
            setDescription("");
            setImage(null);
            setNewImagePreviewUrl("");
            setValidationErrors({});

            onClose();
            }
           
        }  catch (error : unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                setValidationErrors({
                    Description: error.response.data.errors?.Description || [],
                    Image: error.response.data.errors?.Image || [],
                });
            }
            else {
                console.error("Error adding buyer details:", error);
            }
        } 
    }

    const handleDescriptionChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setDescription(event.target.value);
        setValidationErrors({ Description: [] });
    };

    return (
        <AddModal title={"details"} onClose={onClose} onContinue={handleAddSubCategorySuccess}>   
            <FormGroup error={validationErrors.Description} id={"description"} label={"Tell us about yourself"} type={"text"} value={description} onChange={handleDescriptionChange} placeholder={"Enter Description"} ariaDescribedBy={"description-help"} onShowTooltip={handleShowDescriptionTooltip} showTooltip={showDescriptionTooltip} tooltipDescription={"Briefly introduce yourself or your project. Highlight key details to help others understand your background or needs."} ></FormGroup>  
            <FormGroup error={validationErrors.Image} id={"profile-image"} label={"Image"} type={"file"} onChange={handleImageInputChange} placeholder={"Upload Image"} ariaDescribedBy={"image-help"} onShowTooltip={handleShowImageTooltip} showTooltip={showImageTooltip} tooltipDescription={"Upload a relevant image for the sub-category. Ensure it's clear and represents the category accurately."} ></FormGroup>  
            {newImagePreviewUrl && <ImagePreview alt={"Preview"} src={newImagePreviewUrl} />}
        </AddModal>       
    );
}