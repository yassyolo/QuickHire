import './BrowsingHistoryRowItem.css';
import { useNavigate } from 'react-router-dom';
interface BrowsingHistoryRowItemProps {
    id: number;
    title: string;
    imageUrl: string; 
    liked: boolean;
    gigId: number; 
    setLiked: (liked: boolean, id: number) => void;
}

export function BrowsingHistoryRowItem({ id, title, imageUrl }: BrowsingHistoryRowItemProps) {
    const navigate = useNavigate();

    const handleBrowsingHistoryRowClick = () => {
        navigate(`/buyer/browsing-history`);
    };
    return (
        <div className="browsing-history-row-item" key={id} >
            <div className="browsing-history-image-wrapper">
                <img className="browsing-history-image"  src={imageUrl}></img>  
            </div>
            <div className="browsing-history-title" onClick={handleBrowsingHistoryRowClick}>{title}</div>
        </div>
    );
}