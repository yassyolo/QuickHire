import { ImagePreview } from '../../../../../Shared/Images/ImagePreview/ImagePreview';
import './DetailsRow.css'; 

export interface DetailsRowProps {
    label: string;
    value: React.ReactNode | null;
    icon: React.ReactNode;
}

export function DetailsRow({ label, value, icon } : DetailsRowProps) {
    const normalizedLabel = label.trim().toLowerCase();

return (
    <div className="detail-row">
        <div className="detail-row-icon">{icon}</div>
        <div className="detail-label-value d-flex flex-column">
        <div aria-label={label} className="detail-label">{label}:</div>
        {normalizedLabel === "image url" && value ? (
            <ImagePreview src={String(value)} alt={'value'} />
        ) : (<div className="detail-value">{value}</div>)}
        </div>
    </div>
);

};