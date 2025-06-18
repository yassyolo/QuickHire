import { useState } from "react";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";

export interface Requirement {
  id: number;
  question: string;
}

interface RequirementsProps {
  requirements: Requirement[];
  onRequirementsChange: (updated: Requirement[]) => void;
  validationErrors?: { Question?: string[] };
  setValidationErrors?: (errors: { Question?: string[] }) => void;
}

export function Requirements({
  requirements,
  onRequirementsChange,
  validationErrors = {},
  setValidationErrors = () => {},
}: RequirementsProps) {
  const [newRequirement, setNewRequirement] = useState("");

  const handleAddRequirement = () => {
    const trimmed = newRequirement.trim();
    if (!trimmed) {
      setValidationErrors({ Question: ["Requirement question cannot be empty."] });
      return;
    }
    const newReq: Requirement = {
      id: Date.now(),
      question: trimmed,
    };
    onRequirementsChange([...requirements, newReq]);
    setNewRequirement("");
    setValidationErrors({});
  };

  const handleUpdateRequirement = (id: number, updatedQuestion: string) => {
    const updated = requirements.map((req) =>
      req.id === id ? { ...req, question: updatedQuestion } : req
    );
    onRequirementsChange(updated);
  };

  const handleDeleteRequirement = (id: number) => {
    onRequirementsChange(requirements.filter((req) => req.id !== id));
  };

  return (
    <div className="flex flex-col gap-4">

      <div className="flex gap-2">
        <div className="d-flex flex-column" style={{gap: '10px'}}>
           <FormGroup
          type="text"
          value={newRequirement}
          onChange={(e) => setNewRequirement(e.target.value)}
          placeholder="Enter requirement question" id={"requirement-id"} label={"Requirement"}    
          error={validationErrors.Question ? [validationErrors.Question[0]] : []}    />
          <ActionButton
                                  text={"Add"}
                                  onClick={handleAddRequirement}
                                  className="add-faq-button"
                                  ariaLabel={`FAQ Button`}

                              />
        </div>
       
      </div>

      <div className="flex flex-col gap-3">
        {requirements.map((req) => (
          <div key={req.id} className="flex items-center gap-2">
                        <div className="x-wrapper" style={{marginTop: '15px'}}>

            <input
              type="text"
              value={req.question}
              onChange={(e) => handleUpdateRequirement(req.id, e.target.value)}
              className="border rounded px-3 py-2 w-full"
            />
               <IconButton
                                        icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }}></i>}
                                        onClick={() => handleDeleteRequirement(req.id)}
                                        className="faq-delete-button"
                                        ariaLabel="Delete FAQ"
                                    />
            </div>
           
          </div>
        ))}
      </div>
    </div>
  );
}
