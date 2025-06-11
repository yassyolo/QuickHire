import { useEffect, useState } from "react";
import { IconButton } from "../../../Shared/Buttons/IconButton/IconButton";
import axios from "axios";
import { FavouriteListItem } from "./FavouriteListItem";
import { AddFavouriteListModal } from "../../../Admin/Components/Modals/Add/FavouriteList/AddFavouriteList";
import './FavouriteButtonDropdown.css';

interface FavouriteButtonDropdownProps {
    gigId: number;
    liked: boolean;
    setLiked: (liked: boolean, id: number) => void;
}

interface FavouriteListItem {
    id: number;
    name: string;
}

export function FavouriteButtonDropdown({ liked , gigId, setLiked}: FavouriteButtonDropdownProps) {
    const [showLikeDropdown, setShowLikeDropdown] = useState(false);
    const [favouriteList, setFavouriteList] = useState<FavouriteListItem[]>([]);
    const [selectedFavouriteListId, setSelectedFavouriteListId] = useState<number | null>(null);
    const [showAddNewListModal, setShowAddNewListModal] = useState(false);

    const handleAddNewList = () => {
        setShowAddNewListModal(true);
    };

    const fetchFavouriteList = async () => {    
        
        try {
            const response = await axios.get<FavouriteListItem[]>('https://localhost:7267/buyers/favourite-gigs/lists/populate');
            setFavouriteList(response.data);
        } catch (error) {
            console.error("Error fetching favourite list:", error);
        }
    }

    useEffect(() => {
        fetchFavouriteList();
    }, []);


    const handleAddToOldList = async (id: number) => {
        if (selectedFavouriteListId !== null) {
            try {
                const params = new URLSearchParams();
                params.append('gigId', id.toString());
                params.append('favouriteListId', selectedFavouriteListId.toString());
                 await axios.post('https://localhost:7267/buyers/favourite-gigs/lists/add-to-old-list', params);
                setShowLikeDropdown(false);
            } catch (error) {
                console.error("Error adding to favourite list:", error);
            }
        } else {
            console.warn("No favourite list selected");
        }
    }

    const handleSelectedFavouriteListId = (id: number) => {
        setSelectedFavouriteListId(id);
        handleAddToOldList(gigId);
        setShowLikeDropdown(false);
    };

    const handleLikeDropdownVisibility = () => {
        setShowLikeDropdown(!showLikeDropdown);
    };

    const handleRemoveLikedGig = async () => {
        try {
            await axios.put(`https://localhost:7267/buyers/favourite-gigs/unfavourite/${gigId}`);
            setShowLikeDropdown(false);
            setLiked(false, gigId);
        } catch (error) {
            console.error("Error removing liked gig:", error);
        }
    };


    return (
        <div className="favourite-button-dropdown-container">
            {liked ? (<IconButton icon={<i className="fa-solid fa-heart"></i>} onClick={handleRemoveLikedGig} className={'unlike-button'} ariaLabel={'Unlike'} buttonInfo={"Unlike"}></IconButton>) :
                (<IconButton icon={<i className="fa-regular fa-heart"></i>} onClick={handleLikeDropdownVisibility} className={'like-button'} ariaLabel={'Like'} buttonInfo={"Like"} ></IconButton>)}
             {showLikeDropdown &&
             <div className="favourite-dropdown">
                <div className="favourite-dropdown-title">
                    <div className="favourite-dropdown-title-text">Your Lists</div>
                    <IconButton icon={<i className="fa-solid fa-plus"></i>} onClick={handleAddNewList} className={'add-new-list'} ariaLabel={'Add new list'}></IconButton>
                </div>
                    <div className="favourite-dropdown-list">
                        {favouriteList.map((item) => (
                            <FavouriteListItem id={item.id} title={item.name} setFavouriteListId={handleSelectedFavouriteListId}/>
                        ))}
                        {showAddNewListModal && <AddFavouriteListModal gigId={gigId} onClose={() => setShowAddNewListModal(false)} title={"Create new list"} showModal={showAddNewListModal} />}
                    </div>
             </div>
       }                        
</div>
    );
}