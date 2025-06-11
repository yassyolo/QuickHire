import { InfoButton } from "../../../Buttons/InfoButton/InfoButton";
import "./ToolTiContainer.css";

export interface TooltipContainerProps {
    showTooltip: boolean;
    ariaDecribedBy: string;
    onShowTooltip: () => void;
    description: string;
}

export function TooltipContainer ({showTooltip, onShowTooltip, ariaDecribedBy, description} : TooltipContainerProps) {
    return(
    <div className="tooltip-container ">
        <InfoButton ariaDecribedBy={ariaDecribedBy} onMouseEnter={onShowTooltip} />
        {showTooltip && ( <div className="tooltip-box" role="tooltip">{description}</div>)}
    </div>
    );
}