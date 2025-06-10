import { IconButton } from "../../../../../../Shared/Buttons/IconButton/IconButton";

interface LanguageTagProps {
    languageName: string;
    onButtonClick?: () => void;
    showActions: boolean;
}

export function LanguageTag({ languageName, onButtonClick, showActions }: LanguageTagProps) {
    return (
        <div className="language-tag">
            <span className="language-name"> <i className="bi bi-chat"></i> {languageName}</span>
{showActions && (
    <IconButton
        icon={<i className="bi bi-pencil"></i>}
        onClick={onButtonClick ?? (() => {})}
        className={"edit-language-button"}
        ariaLabel={"EditLanguageButton"}
    />
)}
        </div>
    );
}