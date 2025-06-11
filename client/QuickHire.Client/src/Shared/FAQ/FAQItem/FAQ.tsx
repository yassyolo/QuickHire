import { useState } from "react";
import "./FAQ.css";
import { IconButton } from "../../Buttons/IconButton/IconButton";
import { DeactivateModal } from "../../../Admin/Components/Modals/Deactivate/Common/DeactivatePossibleModal/DeactivatePossibleModal";
import axios from "axios";
import { EditFAQModal } from "../../../Admin/Components/Modals/Edit/EditFAQModal";

export interface FAQProps {
    id: number;
    question: string;
    answer: string;
    showActions: boolean;
    onDelete?: (id: number) => void;
    onEdit?: (id: number, newQuestion: string, newAnswer: string) => void;
}

export function FAQ({ question, answer, id, showActions, onDelete, onEdit }: FAQProps) {
    const [isOpen, setIsOpen] = useState(false);
    const [showEditFAQModal, setEditFAQ] = useState(false);
    const [showDeleteFAQModal, setShowDeleteFAQModal] = useState(false);
    const [reason, setReason] = useState("");
    const [reasonError, setReasonError] = useState<string[]>([]);

    const toggleAnswer = () => setIsOpen((prev) => !prev);
    const toggleEditModal = () => setEditFAQ((prev) => !prev);
    const toggleDeleteModal = () => setShowDeleteFAQModal((prev) => !prev);

    const handleDeactivateFAQSuccess = async () => {
        setReasonError([]);
        try {
            await axios.delete("https://localhost:7267/faqs", {
                data: { id, reason },
            });

            onDelete?.(id);
        } catch (error) {
            if (axios.isAxiosError(error) && error.response?.status === 400) {
                setReasonError(error.response.data.Reason);
            }
        } finally {
            setReason("");
            setReasonError([]);
            setShowDeleteFAQModal(false);
        }
    };

    return (
        <div className="faq">
            <div className="faq-title-button">
                <div className="faq-question">{question}</div>
                <div className={`faq-button ${isOpen ? "open" : ""}`} onClick={toggleAnswer}>
                    {isOpen ? (
                        <i className="bi bi-chevron-up"></i>
                    ) : (
                        <i className="bi bi-chevron-down"></i>
                    )}
                </div>

                {showActions && (
                    <div className="faq-actions">
                        <IconButton
                            icon={<i className="bi bi-pencil" style={{ fontSize: "18px" }}></i>}
                            onClick={toggleEditModal}
                            className="faq-edit-button"
                            ariaLabel="Edit FAQ"
                        />
                        <IconButton
                            icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }}></i>}
                            onClick={toggleDeleteModal}
                            className="faq-delete-button"
                            ariaLabel="Delete FAQ"
                        />
                    </div>
                )}

                {showEditFAQModal && onEdit && (
                    <EditFAQModal
                        id={id}
                        showModal={showEditFAQModal}
                        onClose={toggleEditModal}
                        onEditSuccess={onEdit}
                        initialQuestion={question}
                        initialAnswer={answer}
                    />
                )}

                {showDeleteFAQModal && (
                    <DeactivateModal
                        id={id}
                        reason={reason}
                        onClose={toggleDeleteModal}
                        onDeactivateSuccess={handleDeactivateFAQSuccess}
                        setReason={setReason}
                        error={reasonError}
                    />
                )}
            </div>

            <div className={`faq-answer-text ${isOpen ? "open" : ""}`}>{answer}</div>
        </div>
    );
}
