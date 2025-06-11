import { useEffect, useState } from "react";
import axios from "../../../../../axiosInstance";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { DetailsModalButtons } from "../Modals/ModalButtons/DetailsModalButtons";
import { AddDetailsModal } from "./AddDetailsForm";
import { NewAddedSkill } from "../NewAddedItems/Skill/NewAddedSkill";

export interface Skill {
  id: number;
  name: string;
}

interface EditSkillsModalFormProps {
  initialSkills: Skill[];
  onSuccess: (updatedSkills: Skill[]) => void;
}

export function EditSkillsModalForm({ initialSkills, onSuccess }: EditSkillsModalFormProps) {
  const [name, setName] = useState("");
  const [validationErrors, setValidationErrors] = useState<{ Skill?: string[] }>({});
  const [skills, setSkills] = useState<Skill[]>([]);
  const [skillToEdit, setSkillToEdit] = useState<Skill | null>(null);
  const [showSkillTooltip, handleShowSkillTooltip] = useTooltip();

  useEffect(() => {
    setSkills(initialSkills);
  }, [initialSkills]);

  const clearForm = () => {
    setName("");
    setSkillToEdit(null);
    setValidationErrors({});
  };

  const onAddOrUpdate = () => {
    const errors: typeof validationErrors = {};
    if (!name.trim()) errors.Skill = ["Skill is required."];

    if (Object.keys(errors).length > 0) {
      setValidationErrors(errors);
      return;
    }

    if (skillToEdit) {
      setSkills((prev) =>
        prev.map((s) =>
          s.id === skillToEdit.id ? { ...skillToEdit, name } : s
        )
      );
    } else {
      const tempId = Math.floor(Math.random() * -1000);
      const newSkill: Skill = { id: tempId, name };
      setSkills((prev) => [...prev, newSkill]);
    }

    clearForm();
  };

  const handleEdit = (skill: Skill) => {
    setSkillToEdit(skill);
    setName(skill.name);
  };

  const handleRemove = (skill: Skill) => {
    setSkills((prev) => prev.filter((s) => s.id !== skill.id));
    if (skillToEdit?.id === skill.id) clearForm();
  };

  const handleSave = async () => {
    if (skills.length === 0) {
      setValidationErrors({ Skill: ["At least one skill must be added."] });
      return;
    }

    try {
      await axios.put("https://localhost:7267/seller/profile/skills", {
        Skills: skills.map((s) => ({
          Id: s.id,
          Name: s.name,
        })),
      });

      onSuccess(skills);
      clearForm();
    } catch (error) {
      console.error("Error saving skills:", error);
    }
  };

  return (
    <div className="add-form-wrapper">
      <AddDetailsModal onSave={onAddOrUpdate}>
        <FormGroup
          id="skill"
          label="Skill"
          tooltipDescription="Enter a specific skill you possess, such as 'Graphic Design', 'JavaScript', or 'Project Management'."
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Enter Skill"
          ariaDescribedBy="skill-help"
          onShowTooltip={handleShowSkillTooltip}
          showTooltip={showSkillTooltip}
          error={validationErrors.Skill || []}
        />
      </AddDetailsModal>

      {skills.map((skill) => (
        <NewAddedSkill
          key={skill.id}
          skill={skill.name}
          onRemove={() => handleRemove(skill)}
          onEdit={() => handleEdit(skill)}
        />
      ))}

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleSave} onClear={clearForm} />
      </div>
    </div>
  );
}
