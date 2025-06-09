import { NewAddedItemsActions } from "../Buttons/NewAddedItemsActions";
import "./NewAddedDescription.css";

export interface NewAddedDescriptionProps {
    description: string;
    onRemove: () => void;
    onEdit: () => void;
}

export function NewAddedDescription({ description, onRemove, onEdit }: NewAddedDescriptionProps) {
    return (
        <div className="new-description-tag d-flex flex-row align-items-center justify-content-between">
            <span className="new-description-tag-text">{description}</span>
            <NewAddedItemsActions onEdit={onEdit} onRemove={onRemove} />
        </div>
    );
}
