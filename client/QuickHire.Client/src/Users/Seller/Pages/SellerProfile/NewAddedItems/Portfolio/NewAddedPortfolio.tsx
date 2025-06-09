import { NewAddedItemsActions } from "../Buttons/NewAddedItemsActions";
import "./NewAddedPortfolio.css";

interface NewAddedPortfolioProps {
    title: string;
    description: string;
    imageUrl: string;
    mainCategoryName: string;
    onRemove: () => void;
    onEdit: () => void;
}

export function NewAddedPortfolio({ title, description, imageUrl, onRemove, onEdit, mainCategoryName }: NewAddedPortfolioProps) {
    return (
        <div className="new-portfolio-item d-flex flex-row">
            <div className="new-portfolio-image-wrapper d-flex flex-row align-items-center justify-content-center">
                            {imageUrl && <img src={imageUrl} alt={title} className="new-portfolio-image" />}

            </div>

            <div className="new-portfolio-details d-flex flex-column">
                <div className="new-portfolio-title">{title}</div>               
                <div className="new-portfolio-description">{description}</div>
                <div className="new-portfolio-main-category">{mainCategoryName}</div>
            </div>

             <NewAddedItemsActions onEdit={onEdit} onRemove={onRemove} />             
        </div>
    );
}
