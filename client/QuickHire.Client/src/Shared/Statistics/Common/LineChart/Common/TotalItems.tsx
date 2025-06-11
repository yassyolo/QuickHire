import React from 'react';
import { DetailsRow } from '../../../../../Admin/Components/Details/Common/CategoryDetails/DetailsRow/DetailsRow';
import './TotalItems.css';  
export interface TotalItemsProps {
    label: string;
    count: string;
    icon: React.ReactNode;
}

export function TotalItems({ label, count, icon }: TotalItemsProps) {
    return (
        <div className="total-items" aria-label="total-items">
            <DetailsRow label={`Total ${label}`} value={`${count} since launch`} icon={icon} />
        </div>
    );
}