import { useCallback } from "react";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup";

export interface EducationFormProps {
    institution: string;
    setInstitution: (institution: string) => void;
    degree: string;
    setDegree: (degree: string) => void;
    endYear: string;
    setEndYear: (endYear: string) => void;
    major: string;
    setMajor: (major: string) => void;
    validationErrors: { Institution?: string[]; Degree?: string[]; EndYear?: string[]; Major?: string[] };
    showInstituitonTooltip: boolean;
    handleInstitutionTooltip: () => void;
    showDegreeTooltip: boolean;
    handleDegreeTooltip: () => void;
    showEndYearTooltip: boolean;
    handleEndYearTooltip: () => void;
    showMajorTooltip: boolean;
    handleMajorTooltip: () => void;
}

export function EducationForm ({institution, setInstitution, degree, setDegree, endYear, setEndYear, major,setMajor, validationErrors, showInstituitonTooltip, handleInstitutionTooltip, showDegreeTooltip, handleDegreeTooltip, showEndYearTooltip, handleEndYearTooltip, showMajorTooltip, handleMajorTooltip} : EducationFormProps) {

    const handleInstitutionChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setInstitution(event.target.value),[setInstitution]);
    const handleDegreeChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setDegree(event.target.value), [setDegree]);
    const handleEndYearChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setEndYear(event.target.value), [setEndYear]);
    const handleMajorChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setMajor(event.target.value), [setMajor]);

    return(
        <><FormGroup id={"institution"} label={"Institution"} tooltipDescription={"Enter the full official name of the educational institution."} type={"text"} value={institution} onChange={handleInstitutionChange} placeholder={"Enter Instituion"} ariaDescribedBy={"institution-help"} onShowTooltip={handleInstitutionTooltip} showTooltip={showInstituitonTooltip} error={validationErrors.Institution || []} />      
        <FormGroup id={"degree"} label={"Degree"} tooltipDescription={"Specify the degree you obtained"} type={"text"} value={degree} onChange={handleDegreeChange} placeholder={"Enter Degree"} ariaDescribedBy={"degree-help"} onShowTooltip={handleDegreeTooltip} showTooltip={showDegreeTooltip} error={validationErrors.Degree || []} />
        <FormGroup id={"end-year"} label={"End Year"} tooltipDescription={"Enter the year you completed or expect to complete your degree"} type={"text"} value={endYear} onChange={handleEndYearChange} placeholder={"Enter End Year"} ariaDescribedBy={"end-year-help"} onShowTooltip={handleEndYearTooltip} showTooltip={showEndYearTooltip} error={validationErrors.EndYear || []} />
        <FormGroup id={"major"} label={"Major"} tooltipDescription={"Indicate your field of study or specialization."} type={"text"} value={major} onChange={handleMajorChange} placeholder={"Enter Major"} ariaDescribedBy={"major-help"} onShowTooltip={handleMajorTooltip} showTooltip={showMajorTooltip} error={validationErrors.Major || []} /></>        
    )
}