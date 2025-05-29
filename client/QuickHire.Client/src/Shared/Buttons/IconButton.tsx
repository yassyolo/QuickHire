import { ReactElement } from 'react';
import './IconButton.css';

export interface IconButtonProps {
    icon: ReactElement;
    onClick: () => void;    
    className: string;
    ariaLabel: string;
    buttonInfo?: string;
}

export function IconButton({ icon, onClick, className, ariaLabel, buttonInfo }: IconButtonProps) {
    return (
        <div className="icon-button-wrapper">
            <button className={className} onClick={onClick} aria-label={ariaLabel}> {icon} </button>
            {buttonInfo && <span className="hover-text">{buttonInfo}</span>}
        </div>
    );
}
