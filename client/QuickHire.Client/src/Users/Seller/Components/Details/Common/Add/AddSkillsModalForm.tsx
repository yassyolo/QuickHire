import { useState } from "react";
import axios from "axios";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { AddDetailsModal } from "./AddDetailsForm";
import { NewAddedSkill } from "./NewAddedItems/Skill/NewAddedSkill";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";
import { Skill } from "../../../../Pages/SellerProfile/SellerProfile";

interface TempSkill {
    name: string;
}

interface AddSkillsModalFormProps {
    onSuccess: (newSkills: Skill[]) => void;
}

export function AddSkillsModalForm({ onSuccess }: AddSkillsModalFormProps) {
    const [skillName, setSkillName] = useState<string>("");
    const [skills, setSkills] = useState<TempSkill[]>([]);
    const [editingIndex, setEditingIndex] = useState<number | null>(null);
    const [validationErrors, setValidationErrors] = useState<{ Skill?: string[]; Level?: string[] }>({});

    const [showSkillTooltip, handleShowSkillTooltip] = useTooltip();

    const handleAddSkill = () => {
        if (!skillName) {
            setValidationErrors({
                Skill: skillName ? [] : ["Skill is required."],
            });
            return;
        }

        const newSkill: TempSkill = { name: skillName };

        if (editingIndex !== null) {
            setSkills(skills.map((s, idx) => (idx === editingIndex ? newSkill : s)));
            setEditingIndex(null);
        } else {
            setSkills([...skills, newSkill]);
        }

        setSkillName("");
        setValidationErrors({});
    };

    const handleSaveToBackend = async () => {
        if (skills.length === 0) {
            setValidationErrors({ Skill: ["At least one skill must be added."] });
            return;
        }

        const payload = {
            skills: skills.map(s => ({ name: s.name }))
        };

        try {
            const res = await axios.post<Skill[]>("https://localhost:7267/users/skills/add", payload);
            if (res.status === 200) {
                onSuccess(res.data); 
                setSkills([]);
                setSkillName("");
                setValidationErrors({});
                setEditingIndex(null);
            }
        } catch (error: unknown) {
            console.error("Failed to save skills", error);
            if (axios.isAxiosError(error) && error.response?.status === 400) {
                const err = error.response.data.errors || {};
                setValidationErrors({
                    Skill: err.Skill || []                });
            }
        }
    };

    const handleClear = () => {
        setSkillName("");
        setSkills([]);
        setEditingIndex(null);
        setValidationErrors({});
    };

    return (
        <>
            <AddDetailsModal onSave={handleAddSkill}>
                <div className="d-flex flex-column gap-3">
                    {editingIndex !== null && (
                        <div className="alert alert-info">Editing skill #{editingIndex + 1}</div>
                    )}

                    <FormGroup
                        id="skill"
                        label="Skill"
                        tooltipDescription="Enter a specific skill like 'JavaScript', 'UI Design', etc."
                        type="text"
                        value={skillName}
                        onChange={(e) => setSkillName(e.target.value)}
                        placeholder="Enter Skill"
                        ariaDescribedBy="skill-help"
                        onShowTooltip={handleShowSkillTooltip}
                        showTooltip={showSkillTooltip}
                        error={validationErrors.Skill || []}
                    />

                   

                    <div className="d-flex flex-wrap gap-2 mt-3">
                        {skills.map((s, index) => (
                            <NewAddedSkill
                                key={index}
                                skill={s.name}
                                onRemove={() => setSkills(skills.filter((_, i) => i !== index))}
                                onEdit={() => {
                                    setSkillName(s.name);
                                    setEditingIndex(index);
                                }}
                            />
                        ))}
                    </div>
                </div>
            </AddDetailsModal>

            <div className="description-buttons">
                <DetailsModalButtons onSave={handleSaveToBackend} onClear={handleClear} />
            </div>
        </>
    );
}
