import { useNavigate } from "react-router-dom";
import './MostPopularInMainCategoryItem.css'; 

interface MostPopularInMainCategoryProps {
    id: number;
    name: string;
    imageUrl: string;
}

export function MostPopularInMainCategoryItem({ id, name, imageUrl }: MostPopularInMainCategoryProps) {
   const navigate = useNavigate();
    const handleClick = () => {
        navigate(`/buyers/main-categories/${id}`);
    };

    return (
        <div className="most-popular-item" key={id} onClick={handleClick}>
<img
  src={imageUrl}
  alt="Popular Sub-Category"
  className="most-popular-image"
  onError={(e) => {
    e.currentTarget.onerror = null; 
    e.currentTarget.src = "/images/fallback.png";
  }}
/>
            <div className="most-popular-name"> {name}</div>
            <div>'..'</div>
        </div>
    );
}