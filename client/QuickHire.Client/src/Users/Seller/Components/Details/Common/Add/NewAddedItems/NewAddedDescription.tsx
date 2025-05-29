export interface NewAddedDescriptionProps {
    description: string;
    onRemove: () => void;
    onEdit: () => void;
}

export function NewAddedDescription({ description, onRemove, onEdit }: NewAddedDescriptionProps) {
    return (
        <div className="description-tag">
            <span className="skills-tag-text">{description}</span>
            <button className="skills-tag-edit-button" onClick={onEdit}>✏️</button>
            <button className="skills-tag-remove-button" onClick={onRemove}>❌</button>
        </div>
    );
}
