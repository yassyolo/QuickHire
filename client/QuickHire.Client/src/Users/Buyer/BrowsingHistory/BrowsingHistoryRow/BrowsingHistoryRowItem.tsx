import { FavouriteButtonDropdown } from '../../Favourites/FavouriteButtonDropdown';
import './BrowsingHistoryRowItem.css';
import { useNavigate } from 'react-router-dom';
interface BrowsingHistoryRowItemProps {
    id: number;
    title: string;
    imageUrl: string; 
    liked: boolean;
}

export function BrowsingHistoryRowItem({ id, title, imageUrl, liked }: BrowsingHistoryRowItemProps) {
    const navigate = useNavigate();

    const handleBrowsingHistoryRowClick = () => {
        navigate(`/buyer/gigs/${id}`);
    };
    return (
        <div className="browsing-history-row-item" key={id} >
            <div className="browsing-history-image-wrapper">
                <img className="browsing-history-image"  src={imageUrl}></img>  
                <div className="like-button-wrapper">
                     <FavouriteButtonDropdown liked={liked} gigId={id} />             
                </div>
            </div>
            <div className="browsing-history-title" onClick={handleBrowsingHistoryRowClick}>{title}</div>
        </div>
    );
}