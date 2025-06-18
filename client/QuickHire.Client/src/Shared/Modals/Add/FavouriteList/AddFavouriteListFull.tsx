import { useCallback, useEffect, useState } from "react";
import { AddModal } from "../Common/AddModal";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
import "../MainCategory/AddMainCategoryModal.css";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";

export interface AddFavouriteListProps {
    title: string;
    showModal: boolean;
    onClose: () => void;
    handleOnAddSuccess: (name: string, description: string) => void;
}

export function AddFavouriteListFillModal({ title, showModal, onClose, handleOnAddSuccess }: AddFavouriteListProps) {
    const [name, setName] = useState<string>("");
    const [showNameTooltip, handleShowNameTooltip] = useTooltip();
    const [description, setDescription] = useState<string>("");
    const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();

    const [validationErrors, setValidationErrors] = useState<{ Name?: string[];  Description?: string[] }>({});

    const handleNameChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setName(event.target.value);
        setValidationErrors({ Name: [] });
    }, []);

    const handleDescriptionChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setDescription(event.target.value);
        setValidationErrors({ Description: [] });
    }, []);

    useEffect(() => {
        if (!showModal) {
            setName("");           
            setValidationErrors({});;
        }
    }, [showModal]);

    const handleSubmit = useCallback(async () => {
        setValidationErrors({});
        try {
            const url = `https://localhost:7267/buyers/favourite-gigs`;
             await axios.post(url, { name, description });
            setName("");
            setDescription("");
            setValidationErrors({});
            handleOnAddSuccess(name, description);

            onClose();
        } catch (error: unknown) {
            if (isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors({
                    Name: error.response.data.errors.Name || [],
                    Description: error.response.data.errors.Description || []
                });
            } else {
                console.error("An unexpected error occurred:", error);
            }
        } 

       
    }, [name, description, handleOnAddSuccess, onClose]);
    if (!showModal) return null;

    return (
        <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
            <FormGroup id={"name"} label={"Name"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={name} onChange={handleNameChange} placeholder={"Enter new list name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} error={validationErrors.Name || []} />
            <FormGroup id={"description"} label={"Description"} tooltipDescription={"Provide a brief description of the list."} type={"text"} value={description} onChange={handleDescriptionChange} placeholder={"Enter list description"} ariaDescribedBy={"description-help"} onShowTooltip={handleShowDescriptionTooltip} showTooltip={showDescriptionTooltip} error={validationErrors.Description || []} />
        </AddModal>
    );
}
