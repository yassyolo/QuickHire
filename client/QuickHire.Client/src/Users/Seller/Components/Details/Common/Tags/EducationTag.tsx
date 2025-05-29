import { SellerTag } from "./SellerTag";

export interface EducationTagProps {
    degree: string;
    institution: string;
    major: string;
    endYear: string;
    onDeleteRowClick: () => void;
}

export function EducationTag({ degree, institution, major, endYear, onDeleteRowClick }: EducationTagProps) {
    return (
        //ToDo
        <SellerTag title={`${degree} - ${institution}`} icon="/images/education-icon.svg" id={0} showActionsButtons={true} onDeleteRowClick={onDeleteRowClick}>
            <span className="education-tag-date">{major} - {endYear}</span>
        </SellerTag>
    
    );
}