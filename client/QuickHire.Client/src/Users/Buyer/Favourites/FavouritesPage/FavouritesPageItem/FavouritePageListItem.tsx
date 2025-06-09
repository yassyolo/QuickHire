import { useState } from 'react';
import './FavouritePageListItem.css';
import { EditFavouriteListModal } from '../../../../../Admin/Components/Modals/Edit/EditFavouriteListModal';
import { DeactivateFavouriteList } from '../../../../../Admin/Components/Modals/Deactivate/DeactivateFavouriteList';
import { useNavigate } from 'react-router-dom';
interface FavouritePageListItemProps {
    id: number;
    name: string;
    imageUrls: string[];
    description: string;
    gigCount: number;
    onEditSuccess: (id: number, name: string, description: string) => void;
    onDeactivateFavouriteList: (id: number) => void;
}
export function FavouritePageListItem({ id, name, imageUrls, description, gigCount, onEditSuccess, onDeactivateFavouriteList }: FavouritePageListItemProps) {
    const [showActions, setShowActions] = useState(false);
    const [showEditModal, setShowEditModal] = useState(false);
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const navigate = useNavigate();
    const handleShowDeleteModal = () => {
        setShowDeleteModal(!showDeleteModal);
    }
    const handleShowEditModal = () => {
        setShowEditModal(!showEditModal);
    }
    const handleActionsClick = () => {
        setShowActions(!showActions);
    }
    const handleShowFavouriteList = () => {
        navigate(`/buyer/favourite-gigs/${id}`);
    }



    return (
        <div className="favourite-page-list-item d-flex flex-column" key={id} onClick={handleShowFavouriteList}>
            <div className="favourite-page-list-item-images">
                <div className="left-image">
  {imageUrls[0] ? (
    <img src={imageUrls[0]} alt="Main" />
  ) : (
    <div className="image-placeholder" />
  )}
</div>
<div className="right-images">
  {imageUrls[1] ? (
    <img src={imageUrls[1]} alt="Secondary 1" />
  ) : (
    <div className="image-placeholder" />
  )}
  {imageUrls[2] ? (
    <img src={imageUrls[2]} alt="Secondary 2" />
  ) : (
    <div className="image-placeholder" />
  )}
</div>
    
  </div>
            <div className="favourite-page-list-info">
                <div className="favourite-page-list-item-header d-flex flex-row">
                   <div className="favourite-page-list-item-title">{name}</div>
                   <div className="favourite-page-list-item-gig-count">({gigCount} gigs)</div>
                </div>
                
                <div className="favourite-page-list-item-description">{description}</div>
                <div className="favourite-page-list-item-actions-container">
                    <div className="dots" onClick={handleActionsClick}>...</div>
                    {showActions && (
                        <div className="favourite-page-list-item-actions">
                            <div className="favourite-page-list-item-action-item" onClick={handleShowEditModal}>Edit</div>
                            <div className="favourite-page-list-item-action-item" onClick={handleShowDeleteModal}>Delete</div>
                        </div>
                    )}
                </div>
                {showEditModal && <EditFavouriteListModal id={id} showModal={true} onClose={handleShowEditModal} onEditSuccess={onEditSuccess} initialName={name} initialDescription={description}/>}
                {showDeleteModal && <DeactivateFavouriteList favouriteListId={id} deactivateFavouriteList={onDeactivateFavouriteList} onClose={handleShowDeleteModal} favouriteListTitle={name}/>}
            
            </div>
        </div>
    );
}