import { NewAddedItemsActions } from "../Actions/NewAddedItemsActions";
import "./NewAddedSkill.css";

export interface NewAddedSkillProps {
    skill: string;
    onRemove: () => void;
    onEdit: () => void;
}

export function NewAddedSkill({ skill, onRemove, onEdit }: NewAddedSkillProps) {
    return (
        <div className="new-skills-tag d-flex flex-row align-items-center justify-content-between">
            <span className="new-skills-tag-text">{skill}</span>
            <NewAddedItemsActions onEdit={onEdit} onRemove={onRemove} />           
        </div>
    );
}
