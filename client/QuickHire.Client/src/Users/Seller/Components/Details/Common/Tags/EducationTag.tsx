import './EducationTag.css';
export interface EducationTagProps {
    degree: string;
    institution: string;
    major: string;
    endYear: string;
}

export function EducationTag({ degree, institution, major, endYear }: EducationTagProps) {
    return (
        <div className="education-tag">
            <div className="degree-institution d-flex flex-row">
                            <div className="education-degree">{degree} -</div>
            <div className="education-institution">{institution}</div>
            </div>
            <div className="major-end-year d-flex flex-row">
                <div className="education-major">{major}</div>
                <div className="education-end-year">{endYear}</div>
            </div>
        </div>
    );
}