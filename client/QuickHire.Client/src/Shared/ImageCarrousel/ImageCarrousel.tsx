import { useState } from "react";
import './ImageCarrousel.css';

interface ImageCarrouselProps {
    images: string[];
}

export function ImageCarrousel({ images }: ImageCarrouselProps) {
    const [currentImageIndex, setCurrentImageIndex] = useState(0);
    const handleShowPreviousImage = (e: React.MouseEvent) => {
        e.stopPropagation();
        setCurrentImageIndex((prev) => (prev === 0 ? images.length - 1 : prev - 1));
    };
    const handleShowNewxtImage = (e: React.MouseEvent) => {
        e.stopPropagation();
        setCurrentImageIndex((prev) => (prev === images.length - 1 ? 0 : prev + 1));
    };
    return(
        <div className="image-carousel-container">
                <img src={images[currentImageIndex]} alt={`${currentImageIndex}-image-preview`} className="gig-image" />
                {images.length > 1 && 
                        <>
                        <button className="carousel-button left" onClick={handleShowPreviousImage}><i className="fa-solid fa-less-than"></i></button>
                        <button className="carousel-button right" onClick={handleShowNewxtImage}><i className="fa-solid fa-greater-than"></i></button></>
                }
         </div>
    )
}