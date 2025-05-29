import'./IconActionButton.css';
import React from 'react';

interface IconActionButtonProps {
    icon: React.ReactNode;
    onClick: () => void;
    text: string;
    ariaLabel: string;
}

export function IconActionButton({ icon, onClick, text, ariaLabel }: IconActionButtonProps) {
    return (
        <button className="icon-action-button d-flex flex-row" onClick={onClick} aria-label={text} aria-labelledby={ariaLabel}>
            <span className="icon-container">{icon}</span>
            <span className="button-text">{text}</span>
        </button>
    );
}