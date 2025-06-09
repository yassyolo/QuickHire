import { useCallback, useEffect, useState } from "react";
import { AddModal } from "./Common/AddModal";
import axios from "axios";
import "./AddMainCategoryModal.css";
import { useTooltip } from "../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../Shared/Forms/FormGroup";

export interface AddFavouriteListProps {
    gigId: number;
    title: string;
    showModal: boolean;
    onClose: () => void;
}

export function AddFavouriteListModal({ title, showModal, onClose, gigId }: AddFavouriteListProps) {
    const [name, setName] = useState<string>("");
    const [showNameTooltip, handleShowNameTooltip] = useTooltip();

    const [validationErrors, setValidationErrors] = useState<{ Name?: string[];  }>({});

    const handleNameChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setName(event.target.value);
        setValidationErrors({ Name: [] });
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
        
            const url = `https://localhost:7267/favourite-lists/add`;
             await axios.post(url, { name, gigId });

            onClose();
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors(error.response.data.errors.Name || {});
            } else {
                console.error("An unexpected error occurred:", error);
            }
        } 

       
    }, [name, onClose]);
    if (!showModal) return null;

    return (
        <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
            <FormGroup id={"name"} label={"List name"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={name} onChange={handleNameChange} placeholder={"Enter new list name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} error={validationErrors.Name || []} />
        </AddModal>
    );
}
