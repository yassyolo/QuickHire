import { useState } from "react";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import { FavouriteListItem } from "./FavouriteListItem";
import { AddFavouriteListModal } from "../../../../../Shared/Modals/Add/FavouriteList/AddFavouriteList";
import './FavouriteButtonDropdown.css';
import axios from "../../../../../axiosInstance";

interface FavouriteButtonDropdownProps {
    gigId: number;
    liked: boolean;
    setLiked: (liked: boolean, id: number) => void;
}

interface FavouriteListItem {
    id: number;
    name: string;
}

export function FavouriteButtonDropdown({ liked, gigId, setLiked }: FavouriteButtonDropdownProps) {
    const [showLikeDropdown, setShowLikeDropdown] = useState(false);
    const [favouriteList, setFavouriteList] = useState<FavouriteListItem[]>([]);
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
    };

    const handleAddToFavouriteList = async (favouriteListId: number) => {
        try {
            await axios.post(`https://localhost:7267/buyers/favourite-gigs/add`, {
                gigId,
                favouriteListId,
            });
            setLiked(true, gigId);
            setShowLikeDropdown(false);          
            console.log(`Gig ${gigId} added to list ${favouriteListId}`);
        } catch (error) {
            console.error("Error adding to favourite list:", error);
        }
    };

    const handleListItemClick = (id: number) => {
        handleAddToFavouriteList(id);  
    };

    const handleLikeDropdownVisibility = () => {
        if (!showLikeDropdown) {
            fetchFavouriteList();
        }
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
            {liked ? (
                <IconButton
                    icon={<i className="fa-solid fa-heart"></i>}
                    onClick={handleRemoveLikedGig}
                    className={'unlike-button'}
                    ariaLabel={'Unlike'}
                    buttonInfo={"Unlike"}
                />
            ) : (
                <IconButton
                    icon={<i className="fa-regular fa-heart"></i>}
                    onClick={handleLikeDropdownVisibility}
                    className={'like-button'}
                    ariaLabel={'Like'}
                    buttonInfo={"Like"}
                />
            )}

            {showLikeDropdown && (
                <div className="favourite-dropdown">
                    <div className="favourite-dropdown-title">
                        <div className="favourite-dropdown-title-text">Your Lists</div>
                        <IconButton
                            icon={<i className="fa-solid fa-plus"></i>}
                            onClick={handleAddNewList}
                            className={'add-new-list'}
                            ariaLabel={'Add new list'}
                        />
                    </div>

                    <div className="favourite-dropdown-list">
                        {favouriteList.map((item) => (
                            <FavouriteListItem
                                key={item.id}
                                id={item.id}
                                title={item.name}
                                setFavouriteListId={handleListItemClick}
                            />
                        ))}
                        {showAddNewListModal && (
                            <AddFavouriteListModal
                                gigId={gigId}
                                onClose={() => setShowAddNewListModal(false)}
                                title={"new list"}
                                showModal={showAddNewListModal}
                                onMakeGigFavourite={() => {
                                    setLiked(true, gigId);
                                    setShowLikeDropdown(false);
                                }}
                            />
                        )}
                    </div>
                </div>
            )}
        </div>
    );
}
