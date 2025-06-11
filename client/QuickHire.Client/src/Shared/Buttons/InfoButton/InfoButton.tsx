import './InfoButton.css';

export interface InfoButtonProps {
    ariaDecribedBy: string;
    onMouseEnter: () => void;
}

export function InfoButton ({ ariaDecribedBy, onMouseEnter }: InfoButtonProps) {
    return <button className="info-button" aria-describedby={ariaDecribedBy} onMouseEnter={onMouseEnter}> <i className="bi bi-info-circle"></i> </button>
}