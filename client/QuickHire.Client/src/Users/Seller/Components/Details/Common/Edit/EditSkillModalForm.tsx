import { useCallback, useEffect, useState } from "react";
import axios from "axios";
import { SelectDropdown } from "../../../../../../Shared/Select/SelectDropdown";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { DetailsModalButtons } from "../EditOrDelete/DetailsModalButtons";
import { AddDetailsModal } from "../Add/AddDetailsForm";
import { NewAddedSkill } from "../Add/NewAddedItems/NewAddedSkill";

export interface SkillLevels {
    id: number;
    name: string;
}

interface AddedSkills {
    id?: number; // optional for new entries
    skill: string;
    levelId: number;
    levelName: string;
}

interface EditSkillsModalFormProps {
    onSuccess: (updatedSkills: string[]) => void;
}

export function EditSkillsModalForm({ onSuccess }: EditSkillsModalFormProps) {
    const [skill, setSkill] = useState<string>("");
    const [levelId, setLevelId] = useState<number | undefined>(undefined);
    const [levels, setLevels] = useState<SkillLevels[]>([]);
    const [skills, setSkills] = useState<AddedSkills[]>([]);
    const [deletedSkillIds, setDeletedSkillIds] = useState<number[]>([]);
    const [editingIndex, setEditingIndex] = useState<number | null>(null);
    const [validationErrors, setValidationErrors] = useState<{ Skill?: string[]; Level?: string[] }>({});
    const [showSkillTooltip, handleShowSkillTooltip] = useTooltip();
    const [showLevelsTooltip, handleShowLevelsTooltip] = useTooltip();

    useEffect(() => {
        async function fetchData() {
            try {
                const [levelsRes, skillsRes] = await Promise.all([
                    axios.get<SkillLevels[]>("https://localhost:7267/users/skills/levels"),
                    axios.get<AddedSkills[]>("https://localhost:7267/users/skills")
                ]);
                setLevels(levelsRes.data);
                setSkills(skillsRes.data);
            } catch (error) {
                console.error("Error loading skill data:", error);
            }
        }
        fetchData();
    }, []);

    const handleSelectLevelId = useCallback((value: number | undefined) => setLevelId(value), []);
    const handleSkillChange = useCallback(
        (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => setSkill(event.target.value),
        []
    );

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

        const newSkill: AddedSkills = {
            skill,
            levelId,
            levelName: selectedLevel.name
        };

        if (editingIndex !== null) {
            // retain ID on update
            setSkills((prev) =>
                prev.map((s, i) => (i === editingIndex ? { ...s, ...newSkill } : s))
            );
            setEditingIndex(null);
        } else {
            setSkills((prev) => [...prev, newSkill]);
        }

        setSkill("");
        setLevelId(undefined);
        setValidationErrors({});
    };

    const onRemoveSkill = (index: number) => {
        const removed = skills[index];
        if (removed.id) {
            setDeletedSkillIds((prev) => [...prev, removed.id!]);
        }
        setSkills((prev) => prev.filter((_, i) => i !== index));
    };

    const handleOnSave = async () => {
        if (skills.length === 0) {
            setValidationErrors({ Skill: ["At least one skill must be added."] });
            return;
        }

        try {
            const url = "https://localhost:7267/users/skills/update";
            const data = {
                skills: skills.map((s) => ({
                    id: s.id ?? null,
                    skill: s.skill,
                    levelId: s.levelId
                })),
                deletedSkillIds
            };

            const response = await axios.put(url, data);
            if (response.status === 200) {
                setSkill("");
                setLevelId(undefined);
                setDeletedSkillIds([]);
                setEditingIndex(null);
                setValidationErrors({});
                onSuccess(skills.map((s) => s.skill));
            }
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response?.status === 400) {
                setValidationErrors({
                    Skill: error.response.data.errors?.Skill || [],
                    Level: error.response.data.errors?.Level || []
                });
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    };

    const handleClear = () => {
        setSkill("");
        setLevelId(undefined);
        setSkills([]);
        setDeletedSkillIds([]);
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
                        id="skill"
                        label="Skill"
                        tooltipDescription="Enter a specific skill you possess, such as 'Graphic Design', 'JavaScript', or 'Project Management'."
                        type="text"
                        value={skill}
                        onChange={handleSkillChange}
                        placeholder="Enter Skill"
                        ariaDescribedBy="skill-help"
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
                        tooltipDescription="Select your proficiency level in the skill, such as Beginner, Intermediate, or Expert."
                        showTooltip={showLevelsTooltip}
                        ariaDescribedBy="skill-level-help"
                        onShowTooltip={handleShowLevelsTooltip}
                    />
                    <div className="d-flex flex-wrap gap-2 mt-3">
                        {skills.map((s, index) => (
                            <NewAddedSkill
                                key={s.id ?? index}
                                skill={s.skill}
                                proLevel={s.levelName}
                                onRemove={() => onRemoveSkill(index)}
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
