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
    const handleClick = (e: React.MouseEvent) => {
    e.stopPropagation();  
    onClick();
  };
    return (
        <div className="icon-button-wrapper">
            <button className={className} onClick={handleClick} aria-label={ariaLabel}> {icon} </button>
            {buttonInfo && <span className="hover-text">{buttonInfo}</span>}
        </div>
    );
}
