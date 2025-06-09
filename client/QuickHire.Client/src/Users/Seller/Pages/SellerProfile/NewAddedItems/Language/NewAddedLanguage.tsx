import { NewAddedItemsActions } from "../Buttons/NewAddedItemsActions";
import './NewAddedLanguage.css';

interface NewAddedLanguageProps {
    languageName: string;
    onDelete: () => void;
    onEdit: () => void;
}


export function NewAddedLanguage({ languageName, onDelete, onEdit }: NewAddedLanguageProps) {
    return (
        <div className="new-language-tag d-flex flex-row align-items-center justify-content-between">
            <span className="language-name">{languageName}</span>
            <NewAddedItemsActions onEdit={onEdit} onRemove={onDelete} />   
        </div>
    );
}