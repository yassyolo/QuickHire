import { useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { AddDetailsModal } from "./AddDetailsForm";
import { EducationForm } from "../Forms/EducationForm";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";

export interface Education {
  id: number;
  institution: string;
  degree: string;
  endYear: string;
  major: string;
}

interface EducationDetailsModalFormProps {
  onSuccess: (newEducation: Education) => void;
}

export function AddEducationDetailsModalForm({ onSuccess }: EducationDetailsModalFormProps) {
  const [institution, setInstitution] = useState("");
  const [degree, setDegree] = useState("");
  const [endYear, setEndYear] = useState("");
  const [major, setMajor] = useState("");

  const [validationErrors, setValidationErrors] = useState<{
    Institution?: string[];
    Degree?: string[];
    EndYear?: string[];
    Major?: string[];
  }>({});

  const [showInstitutionTooltip, handleInstitutionTooltip] = useTooltip();
  const [showDegreeTooltip, handleDegreeTooltip] = useTooltip();
  const [showEndYearTooltip, handleEndYearTooltip] = useTooltip();
  const [showMajorTooltip, handleMajorTooltip] = useTooltip();

  const handleSave = async () => {
    try {
      const url = "https://localhost:5001/api/education";
      const data = { institution, degree, endYear, major };

      const response = await axios.post<Education>(url, data);

      if (response.status === 200) {
        onSuccess(response.data); // send added education to parent
        handleClear(); // reset form
      }
    } catch (error: unknown) {
      if (axios.isAxiosError(error) && error.response?.status === 400) {
        const errors = error.response.data.errors || {};
        setValidationErrors({
          Institution: errors.Institution || [],
          Degree: errors.Degree || [],
          EndYear: errors.EndYear || [],
          Major: errors.Major || [],
        });
      } else {
        console.error("An unexpected error occurred:", error);
      }
    }
  };

  const handleClear = () => {
    setInstitution("");
    setDegree("");
    setEndYear("");
    setMajor("");
    setValidationErrors({});
  };

  return (
    <>
      <AddDetailsModal onSave={handleSave}>
        <EducationForm
          institution={institution}
          setInstitution={setInstitution}
          degree={degree}
          setDegree={setDegree}
          endYear={endYear}
          setEndYear={setEndYear}
          major={major}
          setMajor={setMajor}
          validationErrors={validationErrors}
          showInstituitonTooltip={showInstitutionTooltip}
          handleInstitutionTooltip={handleInstitutionTooltip}
          showDegreeTooltip={showDegreeTooltip}
          handleDegreeTooltip={handleDegreeTooltip}
          showEndYearTooltip={showEndYearTooltip}
          handleEndYearTooltip={handleEndYearTooltip}
          showMajorTooltip={showMajorTooltip}
          handleMajorTooltip={handleMajorTooltip}
        />
      </AddDetailsModal>

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleSave} onClear={handleClear} />
      </div>
    </>
  );
}
