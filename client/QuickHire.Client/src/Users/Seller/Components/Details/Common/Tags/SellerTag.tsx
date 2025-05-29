import { useState } from 'react';
import './SellerTag.css'; 
export interface SellerTagProps {
    id: number;
    title: string;
    icon: string;
    children: React.ReactNode;
    showActionsButtons: boolean;
    onDeleteRowClick: () => void;
}

export function SellerTag({ title, icon, children, showActionsButtons, id, onDeleteRowClick}: SellerTagProps) {
    const [showActions, setShowActions] = useState(false);

    const handleShowActions = () => {
        setShowActions(!showActions);
    }

    const handleEditRowClick = () => {
        // Logic for editing the row can be added here
        console.log(`Edit row with ID: ${id}`);
    };
    return (
        <div className="seller-tag">
            <span className="seller-tag-icon">
                <img src={icon} alt={`${title} Icon`} />
            </span>
            <div className="d-flex flex-row justify-content-between">
            <div className="seller-info">
                <span className="seller-tag-title">{title}</span>
                {children}
            </div>
            {showActionsButtons && (
                <div className="seller-tag-actions" onClick={handleShowActions}>...</div>
            )}
            {showActions && (
                <div className="seller-tag-actions-dropdown">
                    <div className="seller-tag-action" onClick={handleEditRowClick}>Edit</div>
                    <div className="seller-tag-action" onClick={onDeleteRowClick}>Delete</div>
                </div>
            )}
            </div>
            
        </div>
    );
}