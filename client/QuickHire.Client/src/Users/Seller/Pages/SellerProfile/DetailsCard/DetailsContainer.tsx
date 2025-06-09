import React, { useState } from "react";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import './DetailsContainer.css';

export interface DetailsContainerProps {
    title: string;
    noContent: string;
    children: React.ReactNode | null;
    editModal: React.ReactNode;
    addModal: React.ReactNode;
    onAddModalShow: () => void;
    onEditModalShow: () => void; 
}

export function DetailsComponent({ title, noContent, children, editModal, addModal, onAddModalShow, onEditModalShow}: DetailsContainerProps) {
    const [showEditModal, setShowEditModal] = useState(false);
    const [showAddModal, setAddModal] = useState(false);

const handleAddModalVisibility = () => {
    onAddModalShow(); 
    setAddModal(!showAddModal); 
};   
 const handleShowEditModalVisibility = () => {
    onEditModalShow(); 
    setShowEditModal(!showEditModal); 
};

    const hasChildren = React.Children.count(children) > 0;

    return (
        <>
        <div className="seller-details-container">
            <div className="seller-details-title">{title}</div>

            {!hasChildren && <div className="seller-details-no-content">{noContent}</div>}
            {hasChildren && <div className="seller-details-content">{children}</div>}

            {hasChildren ? (
                <ActionButton text={`+ Edit ${title.toLowerCase()}`}
                    onClick={handleShowEditModalVisibility} className="edit-seller-details"
                    ariaLabel="Edit Seller Details Button"/>
            ) : (
                <ActionButton
                    text={`+ Add ${title.toLowerCase()}`} onClick={handleAddModalVisibility}
                    className="add-seller-details" ariaLabel="Add Seller Details Button"/>
            )}


        </div>
            {showEditModal && editModal}
            {showAddModal && addModal}
        </>
    );
}
