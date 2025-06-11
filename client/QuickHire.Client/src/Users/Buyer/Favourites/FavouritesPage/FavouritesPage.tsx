import { useEffect, useState } from "react";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import './FavouritesPage.css';
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { AddFavouriteListFillModal } from "../../../../Admin/Components/Modals/Add/FavouriteList/AddFavouriteListFull";
import axios from "axios";
import { FavouritePageListItem } from "./FavouritesPageItem/FavouritePageListItem";

export interface FavouritePageListItem {
  id: number;
  name: string;
  imageUrls: string[];
  description: string;
  gigCount: number;
}



export function FavouritesPage() {
  const [showAddListModal, setShowAddListModal] = useState(false);
  const [favouriteLists, setFavouriteLists] = useState<FavouritePageListItem[]>([]);

  const fetchFavouriteLists = async () => {
{
      try {
        const response = await axios.get<FavouritePageListItem[]>(
          'https://localhost:7267/buyers/favourite-gigs/lists'
        );
        setFavouriteLists(response.data);
      } catch (error) {
        console.error("Error fetching favourite lists:", error);
      }
    }
  };

  useEffect(() => {
    fetchFavouriteLists();
  }, []);

  const handleCreateAListModalVisibility = () => {
    setShowAddListModal(!showAddListModal);
  };

  const handleOnEditList = (id: number, name: string, description: string) => {
    setFavouriteLists(prevLists =>
      prevLists.map(list =>
        list.id === id ? { ...list, name, description } : list
      )
    );
    }

    const handleOnAddNewList = (name: string, description: string) => {
    const newList: FavouritePageListItem = {
      id: favouriteLists.length + 1, 
      name,
      imageUrls: [], 
      description,
      gigCount: 0,
    };
    setFavouriteLists([...favouriteLists, newList]);
    setShowAddListModal(false);
  };

  const handleDeactivateFavListSuccess = (id: number) => {
    setFavouriteLists(prevLists => prevLists.filter(list => list.id !== id));
  };

  return (
    <SellerPage>
      <div className="favourites-page-container d-flex flex-column">
        <div className="favourites-page-title-button d-flex flex-row">
          <PageTitle title="My lists"
            description="Organize your go-to services into custom lists you can easily access."
            breadcrumbs={[
              { label: <i className="bi bi-house-door"></i>, to: "/buyer" },
              { label: "My lists" },
            ]}
          />
          <ActionButton
            text={"+ Create a list"}
            onClick={handleCreateAListModalVisibility}
            className={"create-a-list-button"}
            ariaLabel={"Create a list button"}
          />
          {showAddListModal && (
            <AddFavouriteListFillModal title={"new list"}
                          showModal={true}
                          onClose={handleCreateAListModalVisibility} handleOnAddSuccess={handleOnAddNewList}            />
          )}
        </div>
        <div className="favourite-lists">
            {favouriteLists.map((list) => (
          <FavouritePageListItem onEditSuccess={handleOnEditList}
              key={list.id} id={list.id}
              name={list.name}
              imageUrls={list.imageUrls}
              description={list.description} gigCount={list.gigCount} onDeactivateFavouriteList={handleDeactivateFavListSuccess}          />
        ))}
        </div>

        
      </div>
    </SellerPage>
  );
}
