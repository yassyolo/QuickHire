import { ChangeEvent, useCallback, useEffect, useState } from "react";
import { AddModal } from "../Common/AddModal";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import { FAQFormGroup } from "../../../Forms/FAQ/FAQFormGroup";
import { FAQ } from "../../../../Gigs/Common/FAQ/FAQList/FAQItem/FAQ";
import "./AddMainCategoryModal.css";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";

export interface AddMainCategoryModalProps {
    title: string;
    showModal: boolean;
    onClose: () => void;
    onAddMainCategorySuccess: () => void;
}

interface Faq{
    question: string;
    answer: string;
}

export function AddMainCategoryModal({ title, showModal, onClose, onAddMainCategorySuccess }: AddMainCategoryModalProps) {
    const [name, setName] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [showNameTooltip, handleShowNameTooltip] = useTooltip();
    const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
    const [showFAQTooltip, handleShowFAQTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ Name?: string[]; Description?: string[] }>({});
    const [faqValidationErrors, setFaqValidationErrors] = useState<{ Question?: string[]; Answer?: string[] }>({});
    const [faqs, setFaqs] = useState<Faq[]>([]);

    useEffect(() => {
        if (!showModal) {
            setName("");
            setDescription("");
            setValidationErrors({});
            setFaqValidationErrors({});
            setFaqs([]);
        }
    }, [showModal]);

    const handleNameChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setName(event.target.value), []);
    const handleDescriptionChange = useCallback((event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setDescription(event.target.value), []);

    const handleAddFAQ = useCallback((question: string, answer: string) => {
        const errors: { Question?: string[]; Answer?: string[] } = {};

    if (!question || question.trim().length < 2 || question.trim().length > 100) {
        errors.Question = ["Question must be between 2 and 100 characters."];
    }

    if (!answer || answer.trim().length < 2 || answer.trim().length > 100) {
        errors.Answer = ["Answer must be between 2 and 100 characters."];
    }

    if (errors.Question || errors.Answer) {
        setFaqValidationErrors(errors);
        return;
    }

    setFaqValidationErrors({});
    
    const newFAQ: Faq = { question: question.trim(), answer: answer.trim() };
    setFaqs((prevFAQs) => [...prevFAQs, newFAQ]);
    }, []);

    const handleRemoveFAQ = useCallback((index: number) => setFaqs((prevFAQs) => prevFAQs.filter((_, i) => i !== index)), []);

    const handleSubmit = useCallback(async () => {
        setValidationErrors({}); 

        try {
            const url = `https://localhost:7267/admin/main-categories`;

            await axios.post(url, { name, description, faqs});

            onAddMainCategorySuccess();
            onClose();
        } catch (error: unknown) {
            if (
                isAxiosError(error) &&
                error.response &&
                error.response.status === 400 &&
                typeof error.response.data === "object" &&
                error.response.data !== null
            ) {
                const errors = (error.response.data as { errors?: { Name?: string[]; Description?: string[] } })?.errors || {};
                console.error("Validation Errors:", errors);

                setValidationErrors({
                    Name: errors.Name || [], 
                    Description: errors.Description || [], 
                });
            } else {
                console.error("Error adding Main Category:", error);
            }
        }
    }, [name, description, faqs, onAddMainCategorySuccess, onClose]);

    if (!showModal) return null;

    return (
        <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
            <FormGroup id={"main-category-name"} label={"Name"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={name} onChange={handleNameChange} placeholder={"Enter Name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} error={validationErrors.Name || []} />
            <FormGroup id={"main-category-description"} label={"Description"} tooltipDescription={"Write a short, catchy description that grabs attention."} type={"text"} value={description} onChange={handleDescriptionChange} placeholder={"Enter Description"} ariaDescribedBy={"description-help"} onShowTooltip={handleShowDescriptionTooltip} showTooltip={showDescriptionTooltip} error={validationErrors.Description || []} />
            <FAQFormGroup showAddButton={true} id={"faq-input"} label={"Add FAQ"} tooltipDescription={"Add a common question and its answer for this category."} onAdd={handleAddFAQ} questionPlaceholder={"Enter question"} answerPlaceholder={"Enter answer"} ariaDescribedBy={"faq-help"} onShowTooltip={handleShowFAQTooltip}  showTooltip={showFAQTooltip} questionError={faqValidationErrors.Question ? faqValidationErrors.Question : []} answerError={faqValidationErrors.Answer ? faqValidationErrors.Answer : []}/>
            {faqs.length > 0 && (
                <div className="faq-list" aria-label={"faq-list"}>
                    <div className="faq-list-title">FAQ's</div>
                    {faqs.map((faq, index) => (
                        <div key={index} className="faq-row"> <FAQ question={faq.question} answer={faq.answer} id={0} showActions={false}></FAQ>
                        <ActionButton text={<i className="bi bi-x" style={{fontSize: "25px", color: "red"}}></i>} onClick={() => handleRemoveFAQ(index)} className={"remove-row-button"} ariaLabel={"Remove FAQ Button"} 
                        /></div>                    
                    ))}
                </div>
            )}

        </AddModal>
    );
}
