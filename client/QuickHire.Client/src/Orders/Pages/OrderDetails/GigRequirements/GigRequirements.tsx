import './GigRequirements.css';
export interface GigRequirement {
    question: string;
    answer: string;
}

interface GigRequirementsProps {
    requirements: GigRequirement[];
}

export function GigRequirements({ requirements }: GigRequirementsProps) {
    return (
        <div className="gig-requirements-show-container d-flex flex-column">
            <ul className="gig-requirements-list">
                {requirements.map((req, index) => (
                    <li key={index} className="gig-requirement-item d-flex flex-column">
                        <div className="requirement-show-question">{req.question}</div>
                        <div className="requirement-show-answer">- {req.answer}</div>
                    </li>
                ))}
            </ul>
        </div>
    );
}