import './Tag.css';
export interface TagProps{
    label: string;
}

export function Tag({ label }: TagProps) {
    return (
        <div className="tag" aria-label="tag">
            <div id={label} className="tag-label">{label}</div>
        </div>
    );
}