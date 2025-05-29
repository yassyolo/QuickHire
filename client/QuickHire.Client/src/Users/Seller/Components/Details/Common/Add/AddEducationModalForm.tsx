import { useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { AddDetailsModal } from "./AddDetailsForm";
import { EducationForm } from "../Forms/EducationForm";

export function EducationDetailsModalForm() {
    const [institution, setInstitution] = useState("");
    const [degree, setDegree] = useState("");
    const [endYear, setEndYear] = useState("");
    const [major, setMajor] = useState("");
    const [showInstituitonTooltip, handleInstitutionTooltip] = useTooltip();
    const [showDegreeTooltip, handleDegreeTooltip] = useTooltip();
    const [showEndYearTooltip, handleEndYearTooltip] = useTooltip();
    const [showMajorTooltip, handleMajorTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ Institution?: string[]; Degree?: string[]; EndYear?: string[]; Major?: string[] }>({});

    const handleAdd = async () => {
        try{
            const url = "https://localhost:5001/api/education";
            const data = {
                institution: institution,
                degree: degree,
                endYear: endYear,
                major: major
            };
            const response = await axios.post(url, data);
            if(response.status === 200){
                setInstitution("");
                setDegree("");
                setEndYear("");
                setMajor("");
            }
        }
        catch(error : unknown){
            if(axios.isAxiosError(error) && error.response && error.response.status === 400){
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors({
                    Institution: error.response.data.errors?.Name || [], 
                    EndYear: error.response.data.errors?.EndYear || [], 
                    Degree: error.response.data.errors?.Degree || [],
                    Major: error.response.data.errors?.Major || []
                });
            }
            else{
                console.error("An unexpected error occurred:", error);
            }
        }
    }

    return(
        <AddDetailsModal  onSave={handleAdd}>
           <EducationForm institution={institution} setInstitution={setInstitution} degree={degree} setDegree={setDegree} endYear={endYear} setEndYear={setEndYear} major={major} setMajor={setMajor} validationErrors={validationErrors} showInstituitonTooltip={showInstituitonTooltip} handleInstitutionTooltip={handleInstitutionTooltip} showDegreeTooltip={showDegreeTooltip} handleDegreeTooltip={handleDegreeTooltip } showEndYearTooltip={showEndYearTooltip} handleEndYearTooltip={handleEndYearTooltip} showMajorTooltip={showMajorTooltip} handleMajorTooltip={handleMajorTooltip}></EducationForm>
        </AddDetailsModal>
    )
}