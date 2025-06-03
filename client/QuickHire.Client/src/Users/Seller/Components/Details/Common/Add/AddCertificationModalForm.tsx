import { useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { AddDetailsModal } from "./AddDetailsForm";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";

export interface Certification {
  id: number;
  certification: string;
  issuer: string;
  date: string;
}

interface AddCertificationModalFormProps {
  onSuccess: (newCertification: Certification) => void;
}

export function AddCertificationModalForm({ onSuccess }: AddCertificationModalFormProps) {
  const [certification, setCertification] = useState<string>("");
  const [issuer, setIssuer] = useState<string>("");
  const [date, setDate] = useState<string>("");

  const [validationErrors, setValidationErrors] = useState<{Certification?: string[];
    Issuer?: string[];
    Date?: string[];
  }>({});

  const [showCertificationTooltip, handleShowCertificationTooltip] = useTooltip();
  const [showIssuerTooltip, handleShowIssuerTooltip] = useTooltip();
  const [showDateTooltip, handleShowDateTooltip] = useTooltip();

  const handleClear = () => {
    setCertification("");
    setIssuer("");
    setDate("");
    setValidationErrors({});
  };

  const onAdd = async () => {
    try {
      const url = "https://localhost:7267/users/certification/add";
      const data = { certification, issuer, date };

      const response = await axios.post<Certification>(url, data);

      if (response.status === 200) {
        onSuccess(response.data); // Notify parent with new certification
        handleClear(); // Reset form
      }
    } catch (error: unknown) {
      if (axios.isAxiosError(error) && error.response?.status === 400) {
        const errors = error.response.data.errors || {};
        setValidationErrors({
          Certification: errors.Certification || [],
          Issuer: errors.Issuer || [],
          Date: errors.Date || [],
        });
      } else {
        console.error("Unexpected error:", error);
      }
    }
  };

  return (
    <>
      <AddDetailsModal onSave={onAdd}>
        <FormGroup
          id="certification"
          label="Certification"
          tooltipDescription="Enter the name of the certification."
          type="text"
          value={certification}
          onChange={(e) => setCertification(e.target.value)}
          placeholder="Enter Certification"
          ariaDescribedBy="certification-help"
          onShowTooltip={handleShowCertificationTooltip}
          showTooltip={showCertificationTooltip}
          error={validationErrors.Certification || []}
        />
        <FormGroup
          id="issuer"
          label="Issuer"
          tooltipDescription="Enter the name of the organization or institution that issued the certification."
          type="text"
          value={issuer}
          onChange={(e) => setIssuer(e.target.value)}
          placeholder="Enter Issuer"
          ariaDescribedBy="issuer-help"
          onShowTooltip={handleShowIssuerTooltip}
          showTooltip={showIssuerTooltip}
          error={validationErrors.Issuer || []}
        />
        <FormGroup
          id="date"
          label="Date"
          tooltipDescription="Enter the date you received the certification."
          type="text"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          placeholder="Enter Date"
          ariaDescribedBy="date-help"
          onShowTooltip={handleShowDateTooltip}
          showTooltip={showDateTooltip}
          error={validationErrors.Date || []}
        />
      </AddDetailsModal>

      <div className="description-buttons">
        <DetailsModalButtons onSave={onAdd} onClear={handleClear} />
      </div>
    </>
  );
}
