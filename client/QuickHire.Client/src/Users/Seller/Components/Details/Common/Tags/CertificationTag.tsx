import { SellerTag } from "./SellerTag";

export interface CertificationTagProps {
    id: number;
    certification: string;
    issuer: string;
    date: string;
    showActionButtons: boolean;
    onDeleteButtonClick: () => void;
}

export function CertificationTag({ certification, issuer, date, id, showActionButtons, onDeleteButtonClick }: CertificationTagProps) {
    return (
        <SellerTag title={certification} icon="/images/certification-icon.svg" id={id} showActionsButtons={showActionButtons} onDeleteRowClick={onDeleteButtonClick}>
             <div className="issuer-date">
               <span className="certification-tag-issuer">{issuer}</span>
               <span className="certification-tag-date">{date}</span>
             </div>
        </SellerTag>
    );
}
