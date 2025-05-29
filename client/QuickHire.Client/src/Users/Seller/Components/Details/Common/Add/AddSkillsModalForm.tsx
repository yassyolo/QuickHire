import { useCallback, useEffect, useState } from "react";
import axios from "axios";
import { SelectDropdown } from "../../../../../../Shared/Select/SelectDropdown";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { AddDetailsModal } from "./AddDetailsForm";
import { NewAddedSkill } from "./NewAddedItems/NewAddedSkill";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";

export interface SkillLevels {
    id: number;
    name: string;
}

interface AddedSkills {
    skill: string;
    levelId: number;
    levelName: string;
}

interface AddSkillsModalFormProps {
    onSuccess: (newSkills: string[]) => void;
}

export function AddSkillsModalForm({ onSuccess }: AddSkillsModalFormProps) {
    const [skill, setSkill] = useState<string>("");
    const [levelId, setLevelId] = useState<number | undefined>(undefined);
    const [levels, setLevels] = useState<SkillLevels[]>([]);
    const [showSkillTooltip, handleShowSkillTooltip] = useTooltip();
    const [showLevelsTooltip, handleShowLevelsTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ Skill?: string[]; Level?: string[] }>({});
    const [skills, setSkills] = useState<AddedSkills[]>([]);
    const [editingIndex, setEditingIndex] = useState<number | null>(null);

    useEffect(() => {
        async function fetchSkillLevels() {
            try {
                const url = "https://localhost:7267/users/skills/levels";
                const response = await axios.get<SkillLevels[]>(url);
                setLevels(response.data);
            } catch (error) {
                console.error("Error fetching skill levels:", error);
            }
        }
        fetchSkillLevels();
    }, []);

    const handleSelectLevelId = useCallback((value: number | undefined) => setLevelId(value), []);
    const handleSkillChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setSkill(event.target.value), []);

    const onAdd = () => {
        if (!skill || !levelId) {
            setValidationErrors({
                Skill: skill ? [] : ["Skill is required."],
                Level: levelId ? [] : ["Skill level is required."]
            });
            return;
        }

        const selectedLevel = levels.find((lvl) => lvl.id === levelId);
        if (!selectedLevel) return;

        const newSkill: AddedSkills = { skill, levelId, levelName: selectedLevel.name };

        if (editingIndex !== null) {
            setSkills((prev) =>
                prev.map((s, i) => (i === editingIndex ? newSkill : s))
            );
            setEditingIndex(null);
        } else {
            setSkills((prev) => [...prev, newSkill]);
        }

        setSkill("");
        setLevelId(undefined);
        setValidationErrors({});
    };

    const handleOnSave = async () => {
        if (skills.length === 0) {
            setValidationErrors({ Skill: ["At least one skill must be added."] });
            return;
        }
        const newSkills = skills.map((s) => s.skill);
        try {
            const url = "https://localhost:7267/users/skills/add";
            const data = { skills: newSkills };
            const response = await axios.post(url, data);
            if (response.status === 200) {
                setSkills([]);
                setSkill("");
                setLevelId(undefined);
                setValidationErrors({});
                onSuccess(newSkills);
                return response.data;
            }
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors({
                    Skill: error.response.data.errors?.Skill || [],
                    Level: error.response.data.errors?.Level || []
                });
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
        return null;
    };

    const handleClear = () => {
        setSkills([]);
        setSkill("");
        setLevelId(undefined);
        setValidationErrors({});
        setEditingIndex(null);
    };

    return (
        <>
            <AddDetailsModal onSave={onAdd}>
                <div className="d-flex flex-column gap-3">
                    {editingIndex !== null && (
                        <div className="alert alert-info">
                            Editing skill #{editingIndex + 1}
                        </div>
                    )}
                    <FormGroup
                        id={"skill"}
                        label={"Skill"}
                        tooltipDescription={
                            "Enter a specific skill you possess, such as 'Graphic Design', 'JavaScript', or 'Project Management'."
                        }
                        type={"text"}
                        value={skill}
                        onChange={handleSkillChange}
                        placeholder={"Enter Skill"}
                        ariaDescribedBy={"skill-help"}
                        onShowTooltip={handleShowSkillTooltip}
                        showTooltip={showSkillTooltip}
                        error={validationErrors.Skill || []}
                    />
                    <SelectDropdown
                        id="skill-level"
                        label="Skill level"
                        options={levels}
                        value={levelId}
                        onChange={handleSelectLevelId}
                        getOptionLabel={(opt) => opt.name}
                        getOptionValue={(opt) => opt.id}
                        tooltipDescription={
                            "Select your proficiency level in the skill, such as Beginner, Intermediate, or Expert."
                        }
                        showTooltip={showLevelsTooltip}
                        ariaDescribedBy={"skill-level-help"}
                        onShowTooltip={handleShowLevelsTooltip}
                    />
                    <div className="d-flex flex-wrap gap-2 mt-3">
                        {skills.map((s, index) => (
                            <NewAddedSkill
                                key={index}
                                skill={s.skill}
                                proLevel={s.levelName}
                                onRemove={() =>
                                    setSkills(skills.filter((_, i) => i !== index))
                                }
                                onEdit={() => {
                                    setSkill(s.skill);
                                    setLevelId(s.levelId);
                                    setEditingIndex(index);
                                }}
                            />
                        ))}
                    </div>
                </div>
            </AddDetailsModal>
            <DetailsModalButtons onSave={handleOnSave} onClear={handleClear} />
        </>
    );
}
