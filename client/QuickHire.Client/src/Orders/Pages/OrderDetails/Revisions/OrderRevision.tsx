interface OrderRevisionProps {
    revision: Revision;
}

export interface Revision {
    id: number;
    revisionNumber: number;
    description: string;
    attachments: string[];
    dateCreated: string;
}

export function OrderRevision({ revision }: OrderRevisionProps) {
    return (
        <div className="order-revision">
            <h3>Revision #{revision.revisionNumber}</h3>
            <p>{revision.description}</p>
            {revision.attachments.length > 0 && (
                <div className="attachments">
                    {revision.attachments.map((attachment, index) => (
                        <a key={index} href={attachment} target="_blank" rel="noopener noreferrer">
                            Attachment {index + 1}
                        </a>
                    ))}
                </div>
            )}
            <span className="date-created">{new Date(revision.dateCreated).toLocaleDateString()}</span>
        </div>
    );
}