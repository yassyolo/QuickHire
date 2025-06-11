import { SyntheticEvent } from 'react';
import './ImagePreview.css';

export interface ImagePreviewProps {
    src: string;
    alt: string;
}

export function ImagePreview({ src, alt }: ImagePreviewProps) {

    const handleError = (e: SyntheticEvent<HTMLImageElement, Event>) => {
        const target = e.target as HTMLImageElement;
        target.onerror = null; 
        target.src = 'https://png.pngtree.com/png-vector/20221125/ourmid/pngtree-no-image-available-icon-flatvector-illustration-pic-design-profile-vector-png-image_40966566.jpg'; 
    };
    
    return (
        <div className="image-preview-container">
            <img src={src} alt={alt ?? "Preview"} className="image-preview"  onError={handleError}/>
        </div>
    );
}