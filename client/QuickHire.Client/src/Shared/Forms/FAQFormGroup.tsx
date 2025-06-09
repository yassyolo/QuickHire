import { ChangeEvent, useCallback, useEffect, useState } from "react";
import { ActionButton } from "../Buttons/ActionButton/ActionButton";
import "./FAQFormGroup.css";
import { FormLabel } from "./FormLabel";

interface FaqProps {
    id: string;
    label: string;
    tooltipDescription: string;
    onAdd?: (question: string, answer: string) => void;
    onEdit?: (id: number, question: string, answer: string) => void;
    showAddButton?: boolean;
    autoSubmit?: boolean; 
    questionPlaceholder: string;
    answerPlaceholder: string;
    ariaDescribedBy: string;
    onShowTooltip: () => void;
    showTooltip: boolean;
    answerError?: string[];
    questionError?: string[];
    editingId?: number;
    initialQuestion?: string;
    initialAnswer?: string;
}

export function FAQFormGroup({
    id,
    label,
    tooltipDescription,
    onAdd,
    onEdit,
    showAddButton,
    autoSubmit = false, 
    questionPlaceholder,
    answerPlaceholder,
    ariaDescribedBy,
    onShowTooltip,
    showTooltip,
    answerError,
    questionError,
    editingId,
    initialQuestion = "",
    initialAnswer = "",
}: FaqProps) {
    const [question, setQuestion] = useState<string>(initialQuestion);
    const [answer, setAnswer] = useState<string>(initialAnswer);

    useEffect(() => {
        setQuestion(initialQuestion);
        setAnswer(initialAnswer);
    }, [initialQuestion, initialAnswer]);

    const handleQuestionChange = useCallback(
        (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
            setQuestion(event.target.value);
        },
        []
    );

    const handleAnswerChange = useCallback(
        (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
            setAnswer(event.target.value);
        },
        []
    );

    const handleSubmit = useCallback(() => {
        if (editingId !== undefined && onEdit) {
            onEdit(editingId, question, answer);
        } else if (onAdd) {
            onAdd(question, answer);
            setQuestion("");
            setAnswer("");
        }
    }, [editingId, onEdit, onAdd, question, answer]);

    useEffect(() => {
        const canAutoSubmit =
            autoSubmit &&
            !editingId &&
            onAdd &&
            question.trim().length > 0 &&
            answer.trim().length > 0;
        if (canAutoSubmit) {
            handleSubmit();
        }
    }, [question, answer, autoSubmit, onAdd, editingId, handleSubmit]);

    const isDisabled = question.trim().length === 0 || answer.trim().length === 0;
    const isEditing = editingId !== undefined;
    const buttonLabel = isEditing ? "Save" : "Add";

    return (
        <div className="form-group">
            <FormLabel
                id={id}
                label={label}
                tooltipDescription={tooltipDescription}
                onShowTooltip={onShowTooltip}
                showTooltip={showTooltip}
                ariaDescribedBy={ariaDescribedBy}
            />
            <div className="add-faq">
                <input
                    id={`${id}-question`}
                    type="text"
                    className={`form-control ${questionError?.length ? "error" : ""}`}
                    placeholder={questionPlaceholder}
                    value={question}
                    onChange={handleQuestionChange}
                    aria-describedby={ariaDescribedBy}
                />
                {questionError && questionError.length > 0 && (
                    <div className="validation-error" key={questionError[0]}>
                        {questionError[0]}
                    </div>
                )}


                <textarea
                    id={`${id}-answer`}
                    className={`form-control ${answerError?.length ? "error" : ""}`}
                    placeholder={answerPlaceholder}
                    value={answer}
                    onChange={handleAnswerChange}
                    aria-describedby={ariaDescribedBy}
                />
                {answerError && answerError.length > 0 && (<div className="validation-error" key={answerError[0]}>{answerError[0]}</div>)}

                {showAddButton && (
                    <ActionButton
                        text={buttonLabel}
                        onClick={handleSubmit}
                        className="add-faq-button"
                        ariaLabel={`${buttonLabel} FAQ Button`}
                        disabled={isDisabled}
                    />
                )}
            </div>
        </div>
    );
}
