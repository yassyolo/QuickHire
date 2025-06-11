import { ChangeEvent } from "react";
import { FormLabel } from "../FormLabel/FormLabel";

interface FormGroupProps {
    id: string;
    label: string;
    tooltipDescription?: string;
    type: "text" | "file" | "textarea" | "password" | "date";
    value?: string | File | undefined;
    onChange: (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => void;
    placeholder?: string;
    ariaDescribedBy?: string;
    onShowTooltip?: () => void;
    showTooltip?: boolean;
    error?: string[] | undefined;
}

export function FormGroup({ id, label, tooltipDescription, type, value, onChange, placeholder, ariaDescribedBy, onShowTooltip, showTooltip, error } : FormGroupProps) {   
    function formatDateToYYYYMMDD(dateString: string): string {
  if (!dateString) return "";
  const match = /^(\d{2})-(\d{2})-(\d{4})$/.exec(dateString);
  if (match) {
    const [, day, month, year] = match;
    return `${year}-${month}-${day}`;
  }
  return dateString; 
}

    return (
        <>
        <div className="form-group">
            <FormLabel id={id} label={label} tooltipDescription={tooltipDescription} onShowTooltip={onShowTooltip} showTooltip={showTooltip} ariaDescribedBy={ariaDescribedBy} />

            {type === "text" && (
                <input id={id} type="text" className={`form-control ${error && error.length > 0 ? 'error' : ''}`} placeholder={placeholder} value={value as string} onChange={onChange} aria-describedby={ariaDescribedBy}/>
            )}
            {type === "file" && (
                <input id={id} type="file" className={`form-control ${error && error.length > 0 ? 'error' : ''}`} accept="image/*" onChange={onChange} aria-describedby={ariaDescribedBy} />
            )}
            {type === "textarea" && (
                <textarea id={id} className={`form-control ${error && error.length > 0 ? 'error' : ''}`} placeholder={placeholder} value={value as string} onChange={onChange} aria-describedby={ariaDescribedBy}/>
            )}
            {type === "password" && (
                <input id={id} type="password" className={`form-control ${error && error.length > 0 ? 'error' : ''}`} placeholder={placeholder} value={value as string} onChange={onChange} aria-describedby={ariaDescribedBy}/>
            )}
            {type === "date" && (
                <input id={id} type="date" className="form-control" placeholder={placeholder} value={formatDateToYYYYMMDD(value as string)} onChange={onChange} aria-describedby={ariaDescribedBy}/>
            )}

            {error && error.length > 0 && (
                <div className="validation-error" key={error[0]} >{error[0]}</div> 
            )}
        </div>
       </>
    );
}
