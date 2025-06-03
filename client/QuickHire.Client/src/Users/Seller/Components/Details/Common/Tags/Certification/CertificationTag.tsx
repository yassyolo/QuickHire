import './CertificationTag.css';
export interface CertificationTagProps {
    id: number;
    certification: string;
    issuer: string;
    date: string;
}

export function CertificationTag({ certification, issuer, date, id }: CertificationTagProps) {
    return (
        <div className="certification-tag" key={id}>
            <div className="certification-issuer d-flex flex-row">            
                <div className="certification-name">{certification} - </div>
                <div className="certification-issuer-text"> {issuer}</div>
                </div>
            <div className="certification-date">{date}</div>
        </div>
    );
}
