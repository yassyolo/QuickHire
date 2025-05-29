import './ImageSubTextItem.css';
interface ImageSubTextItemProps {
    text: string;
}

export function ImageSubTextItem({ text }: ImageSubTextItemProps) {
    return (
            <div className="d-flex flex-row image-subtext-item">
                <i className="bi bi-check"></i>
                <div className="image-subtext">{text}</div>
            </div>
    );
}