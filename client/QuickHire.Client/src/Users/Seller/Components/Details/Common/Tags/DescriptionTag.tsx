
export interface DescriptionTagProps {
    description: string;
}

export function DescriptionTag({ description }: DescriptionTagProps) {

    return (
        <div className="description-tag">
            <span className="description-tag-text">{description}</span>
        </div>
    );
}