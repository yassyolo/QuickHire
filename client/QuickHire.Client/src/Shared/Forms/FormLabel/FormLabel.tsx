import { TooltipContainer } from "../Common/Tooltips/TooltipContainer";
import "./FormLabel.css";

export interface FormLabelProps {
    id: string;
    label: string;
    tooltipDescription?: string;
    onShowTooltip?: (show: boolean) => void;
    showTooltip?: boolean;
    ariaDescribedBy?: string;
}

export function FormLabel({ id, label, tooltipDescription, onShowTooltip, showTooltip, ariaDescribedBy }: FormLabelProps) {
    return(
        <>
        <div className="form-label-tooltip">
            <label htmlFor={id} className="form-label" aria-describedby={ariaDescribedBy}>{label}</label>
            {tooltipDescription && (
                <TooltipContainer onShowTooltip={() => onShowTooltip && onShowTooltip(true)} showTooltip={!!showTooltip} description={tooltipDescription} ariaDecribedBy={ariaDescribedBy || id}/>
            )}
        </div>
        </>
    )
}