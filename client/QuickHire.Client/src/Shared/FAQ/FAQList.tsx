import { useEffect, useState } from "react";
import { FAQ } from "./FAQ";
import "./FAQList.css";
import { ActionButton } from "../Buttons/ActionButton";
import { AddFAQModal } from "../../Admin/Components/Modals/Add/AddFAQModal";

export interface FAQListProps {
    mainCategoryId?: number;
    gigId?: number;
    userId?: string;
    showActions?: boolean;
    title: string;

}

export interface FAQ {
    question: string;
    answer: string;
    id: number;
}

export function FAQList({ mainCategoryId, gigId, userId, showActions, title }: FAQListProps) {
    const [faqs, setFaqs] = useState<FAQ[]>([]);
    const [showAddFAQModal, setShowAddFAQModal] = useState(false);

    const handleAddFAQModalvisibility = () => setShowAddFAQModal(!showAddFAQModal);
     
    useEffect(() => {
        const fetchFAQs = async () => {
        try {
            let url = `https://localhost:7267/faqs?`;

            if (mainCategoryId !== undefined) {
                url += `MainCategoryId=${mainCategoryId}&`;
            }
            if (gigId !== undefined) {
                url += `GigId=${gigId}&`;
            }
            if (userId !== undefined) {
                url += `UserId=${userId}&`;
            }

            url = url.slice(0, -1);

            const response = await fetch(url);
            const data = await response.json();
            setFaqs(data);
        } catch (error) {
            console.error("Error fetching FAQs:", error);
        }
    };

        fetchFAQs();
    }, [mainCategoryId, gigId, userId]);

    const handleDeleteSucecss = (id: number) => setFaqs((prev) => prev.filter((faq) => faq.id !== id));
    const handleEditSuccess = (id: number, newQuestion: string, newAnswer: string) => {
        setFaqs((prev) => prev.map((faq) => faq.id === id ? { ...faq, question: newQuestion, answer: newAnswer } : faq));
    };

    const handleFaqAddSuccess = (id: number, question: string, answer: string) => {
        setFaqs((prev) => [...prev, { id, question, answer }]);
    };

    return (
        <div className="faq-list">
            <div className="d-flex flex-row justify-content-between">
                <div className="faq-list-header">{title} FAQs</div>

             {showActions && <ActionButton text={"Add"} onClick={handleAddFAQModalvisibility} className={"add-faq-button"} ariaLabel={"Add FAQ Button"}></ActionButton>}
            {showAddFAQModal && <AddFAQModal title={"FAQ"} showModal={true} onClose={handleAddFAQModalvisibility} onAddFAQSuccess={handleFaqAddSuccess}></AddFAQModal>}
            </div>
            
            {faqs.map((faq) => ( <div key={faq.id} className="faq-list-item"> 
                 <FAQ question={faq.question} answer={faq.answer} id={faq.id} showActions={showActions ?? false}  onDelete={handleDeleteSucecss} onEdit={handleEditSuccess}/>
                </div>))}
        </div>
    );
}
