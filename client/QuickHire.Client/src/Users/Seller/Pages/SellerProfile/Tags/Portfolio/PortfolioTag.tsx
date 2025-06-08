import './PortfolioTag.css'; 
interface PortolioTagProps {
    title: string;
    description: string;
    imageUrl: string;
    mainCategoryName: string;
}

export function PortfolioTag({ title, description, imageUrl, mainCategoryName }: PortolioTagProps) {
    return (
        <div className="portfolio-tag">
            <div className="portfolio-image-wrapper">
                <img src={imageUrl} alt={title} className="portfolio-image" />
            </div>
            <div className="portfolio-details">
                <div className="portfolio-title">{title}</div>
                <div className="portfolio-description">{description}</div>
                <div className="portfolio-category">{mainCategoryName}</div>
            </div>
        </div>
    );
}