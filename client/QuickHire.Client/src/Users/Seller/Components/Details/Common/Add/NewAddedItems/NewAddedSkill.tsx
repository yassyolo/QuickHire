export interface NewAddedSkillProps {
    skill: string;
    proLevel: string;
    onRemove: () => void;
    onEdit: () => void;
}

export function NewAddedSkill({ skill, proLevel, onRemove, onEdit }: NewAddedSkillProps) {
    return (
        <div className="skills-tag">
            <span className="skills-tag-text">{skill} {proLevel}</span>
            <button className="skills-tag-edit-button" onClick={onEdit}>✏️</button>
            <button className="skills-tag-remove-button" onClick={onRemove}>❌</button>
        </div>
    );
}
