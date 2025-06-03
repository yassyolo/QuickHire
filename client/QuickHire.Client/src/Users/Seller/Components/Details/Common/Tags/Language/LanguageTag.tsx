import { IconButton } from "../../../../../../../Shared/Buttons/IconButton";

interface LanguageTagProps {
    languageName: string;
    onButtonClick: () => void;
}

export function LanguageTag({ languageName, onButtonClick }: LanguageTagProps) {
    return (
        <div className="language-tag">
            <span className="language-name">{languageName}</span>
            <IconButton icon={<i className="bi bi-pencil"></i>} onClick={onButtonClick} className={"edit-language-button"} ariaLabel={"EditLanguageButton"}    />  
        </div>
    );
}