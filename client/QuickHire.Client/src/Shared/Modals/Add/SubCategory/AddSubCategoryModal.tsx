import { ChangeEvent, useCallback, useEffect, useState } from "react";
import { AddModal } from "../Common/AddModal";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
import { ImagePreview } from "../../../Images/ImagePreview/ImagePreview";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import { SelectDropdown } from "../../../Dropdowns/Select/SelectDropdown";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import { SubCategoriesInMainCategory } from "../../../../Admin/Pages/Details/Categories/MainCategory/MainCategoryDetails";

export interface AddSubCategoryModalProps {
    showModal: boolean;
    onClose: () => void;
    onAddSubCategorySuccess: (subCategory: SubCategoriesInMainCategory) => void;
    showCategoriesPopulate: boolean;
    submitedMainCategoryId: number;}

export interface MainCategoryPopulate{
    id: number;
    name: string;
}

export function AddSubCategoryModal({onClose, onAddSubCategorySuccess, showModal, showCategoriesPopulate, submitedMainCategoryId }: AddSubCategoryModalProps) {
    const [name, setName] = useState<string>("");
    const [image, setImage] = useState<File | null>(null);
    const [newImagePreviewUrl, setNewImagePreviewUrl] = useState<string>("");
    const [showNameTooltip, handleShowNameTooltip] = useTooltip();
    const [showImageTooltip, handleShowImageTooltip] = useTooltip();
    const [showMainCategoryTooltip, handleShowMainCategoryTooltip] = useTooltip();
    const [categories, setCategories] = useState<MainCategoryPopulate[]>([]);
    const [selectedMainCategoryId, setSelectedMainCategoryId] = useState<number | undefined>(undefined);
    const [validationErrors, setValidationErrors] = useState<{ Name?: string[]; Image?: string[]; MainCategoryId?: string }>({});


    const handleSelectMainCategoryId = useCallback((value: number | undefined) => setSelectedMainCategoryId(value), []);

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get<MainCategoryPopulate[]>("https://localhost:7267/main-categories/populate");
                setCategories(response.data);
            } catch (error) {
                console.error("Error fetching main categories:", error);
            }
        };
        fetchCategories();
    }, []);
  
    useEffect(() => {
        if (!showModal) {
            setName("");
            setImage(null);
            setNewImagePreviewUrl(""); 
            setSelectedMainCategoryId(undefined);
            setValidationErrors({});     
        }
    }, [showModal]); 

    const handleNameInputChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {setName(event.target.value);}, []);
    
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

            formData.append("name", name);
            if (image) formData.append("image", image);
            if (showCategoriesPopulate && selectedMainCategoryId !== undefined) {
                formData.append("mainCategoryId", selectedMainCategoryId.toString());
            } else if (!showCategoriesPopulate) {
                formData.append("mainCategoryId", submitedMainCategoryId.toString());
            }
    
            const result = await axios.post("https://localhost:7267/admin/sub-categories", formData);
            if (result.status === 200) {
                const newSubCategory = {
                id: result.data.id,
                name: name,
                imageUrl: result.data.imageUrl
            };        
            onAddSubCategorySuccess(newSubCategory);
            onClose();
            }
           
        }  catch (error : unknown) {
            if (isAxiosError(error) && error.response && error.response.status === 400) {
                setValidationErrors({
                    Name: error.response.data.errors?.Name || [],
                    Image: error.response.data.errors?.Image || [],
                    MainCategoryId: error.response.data.errors?.MainCategoryId || []
                });
            }
            else {
                console.error("Error adding sub category:", error);
            }
        } 
    }

    return (
        <AddModal title={"sub category"} onClose={onClose} onContinue={handleAddSubCategorySuccess}>   
            <FormGroup error={validationErrors.Name} id={"sub-category-name"} label={"Name"} type={"text"} value={name} onChange={handleNameInputChange} placeholder={"Enter Name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} tooltipDescription={"Use a clear, descriptive name."} ></FormGroup>  
            <FormGroup error={validationErrors.Image} id={"sub-category-image"} label={"Image"} type={"file"} onChange={handleImageInputChange} placeholder={"Upload Image"} ariaDescribedBy={"image-help"} onShowTooltip={handleShowImageTooltip} showTooltip={showImageTooltip} tooltipDescription={"Upload a relevant image for the sub-category. Ensure it's clear and represents the category accurately."} ></FormGroup>  
            {newImagePreviewUrl && <ImagePreview alt={"Preview"} src={newImagePreviewUrl} />}
            {showCategoriesPopulate &&  <SelectDropdown id="main-category" label="Main category" options={categories} value={selectedMainCategoryId} onChange={handleSelectMainCategoryId} getOptionLabel={(opt) => opt.name} getOptionValue={(opt) => opt.id} tooltipDescription={"Choose the main category that this subcategory belongs to. This helps organize your items logically."} showTooltip={showMainCategoryTooltip} ariaDescribedBy={"dropdown-help"} onShowTooltip={handleShowMainCategoryTooltip}/>
}
        </AddModal>       
    );
}