import { Certification } from "../../AddCertificationModalForm";
import { NewAddedItemsActions } from "../Actions/NewAddedItemsActions";
import "./NewAddedCertification.css";

interface NewAddedCertificationProps {
    certification: Certification;
    onRemove: () => void;
    onEdit: () => void;
}
export function NewAddedCertification({ certification, onRemove, onEdit }: NewAddedCertificationProps) 
{
    return (
        <div className="new-certification-tag d-flex flex-row justify-content-between ">
            <div className="d-flex flex-column">
                <span className="new-certification-tag-text">
                {certification.certification} - {certification.issuer}
            </span>
            <span className="new-certification-tag-date">{certification.date}</span>
            </div>
            
            <NewAddedItemsActions onEdit={onEdit} onRemove={onRemove} />           
        </div>
    );
}
