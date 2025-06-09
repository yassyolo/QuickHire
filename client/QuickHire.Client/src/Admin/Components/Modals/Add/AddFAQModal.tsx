import { useCallback, useEffect, useState } from "react";
import { AddModal } from "./Common/AddModal";
import axios from "axios";
import "./AddMainCategoryModal.css";
import { useTooltip } from "../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../Shared/Forms/FormGroup";

export interface AddFaqModalProps {
    title: string;
    showModal: boolean;
    onClose: () => void;
    onAddFAQSuccess: (id: number, question: string, answer: string) => void;
    mainCategoryId?: number;
    gigId?: number;
}

interface Faq{
    id: number;
    question: string;
    answer: string;
}

export function AddFAQModal({ title, showModal, onClose, onAddFAQSuccess , mainCategoryId, gigId}: AddFaqModalProps) {
    const [question, setQuestion] = useState<string>("");
    const [answer, setAnswer] = useState<string>("");
    const [showQuestionTooltip, handleShowQuestionTooltip] = useTooltip();
    const [showAnswerTooltip, handleShowAnswerTooltip] = useTooltip();
    const handleQuestionChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setQuestion(event.target.value);
        setValidationErrors({ Question: [] });
    }, []);
    const handleAnswerChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setAnswer(event.target.value);
        setValidationErrors({ Answer: [] });
    }, []);
    const [validationErrors, setValidationErrors] = useState<{ Question?: string[]; Answer?: string[] }>({});

    useEffect(() => {
        if (!showModal) {
            setQuestion("");
            setAnswer("");           
            setValidationErrors({});;
        }
    }, [showModal]);

    const handleSubmit = useCallback(async () => {
        setValidationErrors({}); 

        try {
            const url = `https://localhost:7267/faqs`;

        const data: { question: string; answer: string; mainCategoryId?: number; gigId?: number } = {
            question,
            answer,
        };

        if (mainCategoryId != null) {
            data.mainCategoryId = mainCategoryId;
        }

        if (gigId != null) {
            data.gigId = gigId;
        }

        const response = await axios.post(url, data);
            
            const faq = response.data as Faq;

            onAddFAQSuccess(faq.id, faq.question, faq.answer);
            onClose();

            setQuestion("");
            setAnswer("");
            setValidationErrors({});
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);

                setValidationErrors({
                    Question: error.response.data.errors?.Question || [], 
                    Answer: error.response.data.errors?.Answer || [], 
                });
            } else {
                console.error("Error adding FAQ:", error);
            }
        }
    }, [question, answer, onAddFAQSuccess, onClose]);
    if (!showModal) return null;

    return (
        <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
            <FormGroup id={"question"} label={"Question"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={question} onChange={handleQuestionChange} placeholder={"Enter Question"} ariaDescribedBy={"question-help"} onShowTooltip={handleShowQuestionTooltip} showTooltip={showQuestionTooltip} error={validationErrors.Question || []} />
            <FormGroup id={"answer"} label={"Answer"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={answer} onChange={handleAnswerChange} placeholder={"Enter Answer"} ariaDescribedBy={"answer-help"} onShowTooltip={handleShowAnswerTooltip} showTooltip={showAnswerTooltip} error={validationErrors.Answer || []} />               
        </AddModal>
    );
}
