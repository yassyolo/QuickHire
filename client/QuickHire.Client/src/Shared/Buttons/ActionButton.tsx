import React from 'react';
import './ActionButton.css';

export interface ActionButtonProps {
    text: React.ReactNode;
    onClick: () => void;    
    className: string;
    ariaLabel: string;
    disabled?: boolean;
}

export function ActionButton({ text, onClick, className, ariaLabel, disabled }: ActionButtonProps) {
    return <button className={className} onClick={onClick} aria-label={ariaLabel} disabled={disabled}> {text} </button>
}