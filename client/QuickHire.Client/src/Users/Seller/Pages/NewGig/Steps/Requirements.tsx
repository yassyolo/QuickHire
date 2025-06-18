import { useState } from "react";

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
      <h2 className="text-xl font-semibold">Requirements</h2>

      <div className="flex gap-2">
        <input
          type="text"
          value={newRequirement}
          onChange={(e) => setNewRequirement(e.target.value)}
          placeholder="Enter requirement question"
          className="border rounded px-3 py-2 w-full"
        />
        <button
          onClick={handleAddRequirement}
          className="bg-primary text-white px-4 py-2 rounded"
        >
          Add
        </button>
      </div>

      {validationErrors.Question && (
        <div className="text-red-500 text-sm">{validationErrors.Question[0]}</div>
      )}

      <div className="flex flex-col gap-3">
        {requirements.map((req) => (
          <div key={req.id} className="flex items-center gap-2">
            <input
              type="text"
              value={req.question}
              onChange={(e) => handleUpdateRequirement(req.id, e.target.value)}
              className="border rounded px-3 py-2 w-full"
            />
            <button
              onClick={() => handleDeleteRequirement(req.id)}
              className="text-red-500 hover:text-red-700"
            >
              Delete
            </button>
          </div>
        ))}
      </div>
    </div>
  );
}
