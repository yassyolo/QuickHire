import './SkillsTag.css';
export interface SkillsTagProps {
    skill: string;
}

export function SkillsTag({ skill }: SkillsTagProps) {
    return (
        <div className="skills-tag">
            <span className="skills-tag-text">{skill}</span>
        </div>
    );
}