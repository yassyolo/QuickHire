import { useEffect, useState } from "react";
import axios from "../../../../../axiosInstance";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { DetailsModalButtons } from "../Modals/ModalButtons/DetailsModalButtons";
import { AddDetailsModal } from "./AddDetailsForm";
import { NewAddedCertification } from "../NewAddedItems/Certification/NewAddedCertification";

export interface Certification {
  id: number;
  certification: string;
  issuer: string;
  date: string;
}

interface EditCertificationModalFormProps {
  initialCertifications: Certification[];
  onEditSuccess: (updatedCertifications: Certification[]) => void;
}

export function EditCertificationModalForm({ initialCertifications, onEditSuccess}: EditCertificationModalFormProps) {
  const [certification, setCertification] = useState("");
  const [issuer, setIssuer] = useState("");
  const [date, setDate] = useState("");

  const [validationErrors, setValidationErrors] = useState<{ Certification?: string[]; Issuer?: string[]; Date?: string[];}>({});

  const [newCertifications, setNewCertifications] = useState<Certification[]>([]);
  const [certToEdit, setCertToEdit] = useState<Certification | null>(null);

  const [showCertificationTooltip, handleShowCertificationTooltip] = useTooltip();
  const [showIssuerTooltip, handleShowIssuerTooltip] = useTooltip();
  const [showDateTooltip, handleShowDateTooltip] = useTooltip();

  useEffect(() => {
    setNewCertifications(initialCertifications);
    console.log("Initial certifications set:", initialCertifications);
  }, [initialCertifications]);

  const clearForm = () => {
    setCertification("");
    setIssuer("");
    setDate("");
    setCertToEdit(null);
    setValidationErrors({});
  };

  const onAddOrUpdate = () => {
    const errors: typeof validationErrors = {};
    if (!certification.trim()) errors.Certification = ["Certification name is required."];
    if (!issuer.trim()) errors.Issuer = ["Issuer is required."];
    if (!date.trim()) errors.Date = ["Date is required."];

    if (Object.keys(errors).length > 0) {
      setValidationErrors(errors);
      return;
    }

    if (certToEdit) {
      setNewCertifications((prev) =>
        prev.map((c) => (c.id === certToEdit.id ? { ...certToEdit, certification, issuer, date } : c))
      );
    } else {
      const tempId = Math.floor(Math.random() * -1000); 

      const newCert: Certification = {
        id: tempId,
        certification,
        issuer,
        date,
      };
      setNewCertifications((prev) => [...prev, newCert]);
    }

    clearForm();
  };

  const handleEdit = (cert: Certification) => {
    setCertToEdit(cert);
    setCertification(cert.certification);
    setIssuer(cert.issuer);
    setDate(cert.date);
  };


  const handleRemove = (cert: Certification) => {
    setNewCertifications((prev) => prev.filter((c) => c.id !== cert.id));
    if (certToEdit?.id === cert.id) clearForm();
  };

  const handleSave = async () => {
    try {
      await axios.put("https://localhost:7267/seller/profile/certifications", {
  Certifications: newCertifications.map((c) => ({
    Id: c.id,
    Certification: c.certification,
    Issuer: c.issuer,
Date: c.date  })),
});

      onEditSuccess(newCertifications);
      clearForm();
    
    } catch (error) {
      console.error("Error saving certifications:", error);
    }
  };

  return (
    <div className="add-form-wrapper">
      <AddDetailsModal onSave={onAddOrUpdate}>
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
          tooltipDescription="Enter the issuing organization."
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
          tooltipDescription="Enter the certification date."
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
          placeholder="Enter Date"
          ariaDescribedBy="date-help"
          onShowTooltip={handleShowDateTooltip}
          showTooltip={showDateTooltip}
          error={validationErrors.Date || []}
        />
      </AddDetailsModal>

      {newCertifications.map((cert) => (
        <NewAddedCertification
          key={cert.id}
          certification={cert}
          onRemove={() => handleRemove(cert)}
          onEdit={() => handleEdit(cert)}
        />
      ))}

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleSave} onClear={clearForm} />
      </div>
    </div>
  );
}
