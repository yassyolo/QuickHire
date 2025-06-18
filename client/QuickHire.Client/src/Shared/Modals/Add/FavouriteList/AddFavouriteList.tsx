import { useCallback, useEffect, useState } from "react";
import { AddModal } from "../Common/AddModal";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
import "../MainCategory/AddMainCategoryModal.css";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";

export interface AddFavouriteListProps {
    gigId: number;
    title: string;
    showModal: boolean;
    onClose: () => void;
    onMakeGigFavourite: (gigId: number) => void;
}

export function AddFavouriteListModal({ title, showModal, onClose, gigId, onMakeGigFavourite }: AddFavouriteListProps) {
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
        
            const url = `https://localhost:7267/buyers/favourite-gigs`;
            await axios.post(url, { name, gigId });
onMakeGigFavourite(gigId);
            setName("");
            onClose();
        } catch (error) {
            if (isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors(error.response.data.errors.Name || {});
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    }, [gigId, name, onClose]);
    if (!showModal) return null;

    return (
        <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
            <FormGroup id={"name"} label={"Name"} tooltipDescription={"Give your list a clear, specific name to help you recognize it later. E.g., 'Website Ideas' or 'Marketing Gigs'."} type={"text"} value={name} onChange={handleNameChange} placeholder={"Enter new list name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} error={validationErrors.Name || []} />
        </AddModal>
    );
}
