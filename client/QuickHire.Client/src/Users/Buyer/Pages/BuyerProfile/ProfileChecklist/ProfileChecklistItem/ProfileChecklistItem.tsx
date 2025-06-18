import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton/ActionButton";
import "./ProfileChecklistItem.css";

interface ProfileChecklistItemProps {
    title: string;
    description: string;
    buttonName: string;
    onButtonClick: () => void;
}

export function ProfileChecklistItem({
    title, description, buttonName,
    onButtonClick
}: ProfileChecklistItemProps) {
    return (
        <div className="profile-checklist-item d-flex flex-row justify-content-between">
            <div className="d-flex flex-column title-description-chechlist">
                <div className="profile-checklist-item-title">{title}</div>
            <div className="profile-checklist-item-description">{description}</div>
            </div>
            <ActionButton text={buttonName} onClick={onButtonClick} className={"checklist-button"} ariaLabel={""}/>
        </div>
    );
}