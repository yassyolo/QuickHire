import './FavouriteListItem.css';
interface FavouriteListItemProps {
    id: number;
    title: string;
    setFavouriteListId: (id: number) => void;
}

export function FavouriteListItem({ id, title , setFavouriteListId}: FavouriteListItemProps) {
    const handleFavouriteClick = () => {
        setFavouriteListId(id);
    }
    return (
        <div className="favourite-list-item d-flex flex-row" key={id} onClick={handleFavouriteClick}>
            <i className="fa-solid fa-heart"></i>
            <div className="favourite-title">{title}</div>
        </div>
    );
}