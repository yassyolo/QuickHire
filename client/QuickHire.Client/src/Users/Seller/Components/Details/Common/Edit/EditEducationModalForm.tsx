import { useEffect, useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { AddDetailsModal } from "../Add/AddDetailsForm";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";
import { NewAddedEducation } from "../Add/NewAddedItems/Education/NewAddedEducation";

export interface Education {
  id: number;
  institution: string;
  degree: string;
  endYear: string;
  major: string;
}

interface EditEducationModalFormProps {
  existing: Education[];
  onSuccess: (updated: Education[]) => void;
}

export function EditEducationModalForm({ existing, onSuccess }: EditEducationModalFormProps) {
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

  const [items, setItems] = useState<Education[]>([]);
  const [editing, setEditing] = useState<Education | null>(null);

  const [tooltipInstitution, showTooltipInstitution] = useTooltip();
  const [tooltipDegree, showTooltipDegree] = useTooltip();
  const [tooltipEndYear, showTooltipEndYear] = useTooltip();
  const [tooltipMajor, showTooltipMajor] = useTooltip();

  useEffect(() => {
    setItems(existing);
  }, [existing]);

  const clearForm = () => {
    setInstitution("");
    setDegree("");
    setEndYear("");
    setMajor("");
    setEditing(null);
    setValidationErrors({});
  };

  const handleAddOrUpdate = () => {
    const errors: typeof validationErrors = {};
    if (!institution.trim()) errors.Institution = ["Institution is required."];
    if (!degree.trim()) errors.Degree = ["Degree is required."];
    if (!endYear.trim()) errors.EndYear = ["End year is required."];
    if (!major.trim()) errors.Major = ["Major is required."];

    if (Object.keys(errors).length > 0) {
      setValidationErrors(errors);
      return;
    }

    if (editing) {
      setItems((prev) =>
        prev.map((e) => (e.id === editing.id ? { ...editing, institution, degree, endYear, major } : e))
      );
    } else {
      const tempId = Math.floor(Math.random() * -1000);
      const newEdu: Education = { id: tempId, institution, degree, endYear, major };
      setItems((prev) => [...prev, newEdu]);
    }

    clearForm();
  };

  const handleEdit = (edu: Education) => {
    setEditing(edu);
    setInstitution(edu.institution);
    setDegree(edu.degree);
    setEndYear(edu.endYear);
    setMajor(edu.major);
  };

  const handleRemove = (edu: Education) => {
    setItems((prev) => prev.filter((e) => e.id !== edu.id));
    if (editing?.id === edu.id) clearForm();
  };

  const handleSave = async () => {
    try {
      await axios.put("https://localhost:7267/seller/profile/educations", {
        Educations: items.map((e) => ({
          Id: e.id,
          Institution: e.institution,
          Degree: e.degree,
          EndYear: e.endYear,
          Major: e.major,
        })),
      });

      onSuccess(items);
      clearForm();
    } catch (error) {
      console.error("Error saving educations:", error);
    }
  };

  return (
    <div className="add-form-wrapper">
      <AddDetailsModal onSave={handleAddOrUpdate}>
        <FormGroup
          id="institution"
          label="Institution"
          tooltipDescription="Enter the name of the institution."
          type="text"
          value={institution}
          onChange={(e) => setInstitution(e.target.value)}
          placeholder="Enter Institution"
          ariaDescribedBy="institution-help"
          onShowTooltip={showTooltipInstitution}
          showTooltip={tooltipInstitution}
          error={validationErrors.Institution || []}
        />
        <FormGroup
          id="degree"
          label="Degree"
          tooltipDescription="Enter the degree earned."
          type="text"
          value={degree}
          onChange={(e) => setDegree(e.target.value)}
          placeholder="Enter Degree"
          ariaDescribedBy="degree-help"
          onShowTooltip={showTooltipDegree}
          showTooltip={tooltipDegree}
          error={validationErrors.Degree || []}
        />
        <FormGroup
          id="endYear"
          label="End Year"
          tooltipDescription="Enter the year of graduation."
          type="text"
          value={endYear}
          onChange={(e) => setEndYear(e.target.value)}
          placeholder="Enter End Year"
          ariaDescribedBy="endyear-help"
          onShowTooltip={showTooltipEndYear}
          showTooltip={tooltipEndYear}
          error={validationErrors.EndYear || []}
        />
        <FormGroup
          id="major"
          label="Major"
          tooltipDescription="Enter your major field of study."
          type="text"
          value={major}
          onChange={(e) => setMajor(e.target.value)}
          placeholder="Enter Major"
          ariaDescribedBy="major-help"
          onShowTooltip={showTooltipMajor}
          showTooltip={tooltipMajor}
          error={validationErrors.Major || []}
        />
      </AddDetailsModal>

      {items.map((edu) => (
        <NewAddedEducation
          key={edu.id}
          institution={edu.institution}
          degree={edu.degree}
          endYear={edu.endYear}
          major={edu.major}
          onEdit={() => handleEdit(edu)}
          onRemove={() => handleRemove(edu)}
        />
      ))}

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleSave} onClear={clearForm} />
      </div>
    </div>
  );
}
