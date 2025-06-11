import { useEffect, useState } from "react";
import "./FAQList.css";
import { ActionButton } from "../../Buttons/ActionButton/ActionButton";
import { AddFAQModal } from "../../../Admin/Components/Modals/Add/FAQ/AddFAQModal";
import { FAQ } from "../FAQItem/FAQ";
import axios from "../../../axiosInstance";


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

            const response = await axios.get<FAQ[]>(url);
            if (response.status !== 200) {
                throw new Error(`Failed to fetch FAQs: ${response.statusText}`);
            }
            const data = response.data.map((faq) => ({
                id: faq.id,
                question: faq.question,
                answer: faq.answer,
            }));
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
            <div className="d-flex flex-row justify-content-between align-items-center">
                <div className="faq-list-header-wrapper">
                    <div className="faq-list-header">{title} FAQs</div>
                </div>

             {showActions && <ActionButton text={"Add"} onClick={handleAddFAQModalvisibility} className={"add-faq-button"} ariaLabel={"Add FAQ Button"}></ActionButton>}
            {showAddFAQModal && <AddFAQModal title={"FAQ"} showModal={true} onClose={handleAddFAQModalvisibility} onAddFAQSuccess={handleFaqAddSuccess} mainCategoryId={mainCategoryId} gigId={gigId}></AddFAQModal>}
            </div>
            
            {faqs.map((faq) => ( <div key={faq.id} className="faq-list-item"> 
                 <FAQ question={faq.question} answer={faq.answer} id={faq.id} showActions={showActions ?? false}  onDelete={handleDeleteSucecss} onEdit={handleEditSuccess}/>
                </div>))}
        </div>
    );
}
