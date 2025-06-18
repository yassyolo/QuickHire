import { useEffect, useState } from "react";
import { Gig } from "../BuyerBrowsingHistory/BrowsingHistoryPage/BrowsingHistory";
import { useNavigate, useParams } from "react-router-dom";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { EditFavouriteListModal } from "../../../../Shared/Modals/Edit/FavouriteList/EditFavouriteListModal";
import { DeactivateFavouriteList } from "../../../../Shared/Modals/Delete/FavouriteList/DeactivateFavouriteList";
import { GigCard } from "../../../../Gigs/Common/GigCard/GigCard";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import './FavouriteListPage.css';
import axios from "../../../../axiosInstance";

 interface FavouriteList{
    name: string;
    description: string;
    id: number;
    gigs: Gig[];
 }

 export function FavouriteListPage() {
        const { id } = useParams<{id: string}>();
        const favouriteListId = id ? parseInt(id) : null;
        const [favouriteList, setFavouriteList] = useState<FavouriteList | null>(null);
        const [showActions, setShowActions] = useState(false);
        const [showEditModal, setShowEditModal] = useState(false);
        const [showDeleteModal, setShowDeleteModal] = useState(false);
        const [name, setName] = useState("");
        const [description, setDescription] = useState("");
        const navigate = useNavigate();
        const handleShowDeleteModal = () => {
            setShowDeleteModal(!showDeleteModal);
        }
        const handleShowEditModal = () => {
            setShowEditModal(!showEditModal);
            if (favouriteList) {
                setName(favouriteList.name);
                setDescription(favouriteList.description);
            }
        }
        const handleActionsClick = () => {
            setShowActions(!showActions);
        }
        const onEditSuccess = (id: number, name: string, description: string) => {
            setShowEditModal(false);
            setShowActions(false);
            if (favouriteList) {
                setFavouriteList({
                    ...favouriteList,
                    name: name,
                    description: description
                });
            }

            setName("");
            setDescription("");
        }
        const onDeactivateFavouriteList = () => {
            navigate("/buyer/favourite-gigs");
            setShowDeleteModal(false);
            setShowActions(false);
            if (favouriteList) {
                setFavouriteList(null);
            }
            setName("");
            setDescription("");

        }
        
    
         useEffect(() => {
            if (favouriteListId) {
             const fetchFavouriteList = async () => {
                     try {
                        const url = `https://localhost:7267/buyers/favourite-gigs/lists/${favouriteListId}`;
                         const response = await axios.get<FavouriteList>(url);
                         setFavouriteList(response.data);
                     } catch (error) {
                         console.error("Error fetching favourite list:", error);
                     }
                 };
                 fetchFavouriteList();
             }
         }, [favouriteListId]);

        const onSetLiked = (liked: boolean, gigId: number) => {
            setFavouriteList(list => {
                if (!list) return list;
                return {
                    ...list,
                    gigs: list.gigs.filter((gig: Gig) => gig.id !== gigId)
                };
            });
        }
    
        return (
            <SellerPage>
                <div className="favourite-list-page d-flex flex-column">
                <div className="favourites-page-title-button d-flex flex-row">
                          <PageTitle title={favouriteList ? favouriteList.name : "Favourite List"}
                            description="View and manage your favourite gigs in this list."
                            breadcrumbs={[
                              { label: <i className="bi bi-house-door"></i>, to: "/buyer" },
                              { label: "My lists", to: "/buyer/favourite-gigs" },
                              { label: favouriteList ? favouriteList.name : "Favourite List" }
                            ]}
                          />
                          <div className="favourite-page-list-item-actions-container">
                                              <div className="dots" onClick={handleActionsClick}>...</div>
                                              {showActions && (
                                                  <div className="favourite-page-list-item-actions">
                                                      <div className="favourite-page-list-item-action-item" onClick={handleShowEditModal}>Edit</div>
                                                      <div className="favourite-page-list-item-action-item" onClick={handleShowDeleteModal}>Delete</div>
                                                  </div>
                                              )}
                                          </div>
                                          {showEditModal && favouriteListId !== null && (
                                              <EditFavouriteListModal
                                                  id={favouriteListId}
                                                  showModal={true}
                                                  onClose={handleShowEditModal}
                                                  onEditSuccess={onEditSuccess}
                                                  initialName={name}
                                                  initialDescription={description}
                                              />
                                          )}
                                          {showDeleteModal && favouriteListId !== null && (
                                              <DeactivateFavouriteList
                                                  favouriteListId={favouriteListId}
                                                  deactivateFavouriteList={onDeactivateFavouriteList}
                                                  onClose={handleShowDeleteModal}
                                                  favouriteListTitle={name}
                                              />
                                          )}
                          
                        </div>
                        <div className="favourite-list-ietms" style={{marginBottom: '30px'}}>
                            {favouriteList?.gigs && favouriteList.gigs.length > 0 ? 
                                favouriteList.gigs.map((gig) => (
                                    <GigCard gig={gig} showSeller={true} setLiked={onSetLiked}/>
                                )) : (  
                                    <div className="no-gigs-message">
                                        No gigs found in this favourite list.
                                    </div>
                                )
                            }
                        </div>
            </div>
            </SellerPage>
        );
    }