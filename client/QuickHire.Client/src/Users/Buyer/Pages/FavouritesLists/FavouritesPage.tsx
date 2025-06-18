import { useEffect, useState } from "react";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import './FavouritesPage.css';
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { AddFavouriteListFillModal } from "../../../../Shared/Modals/Add/FavouriteList/AddFavouriteListFull";
import { FavouritePageListItem } from "./FavouritesPageItem/FavouritePageListItem";
import axios from "../../../../axiosInstance";

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
    console.log("Favourite lists fetched:", favouriteLists);
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
  {favouriteLists.length > 0 ? (
            <div className="favourite-lists" style={{marginBottom: '30px'}}>

    {favouriteLists.map((list) => (
      <FavouritePageListItem
        key={list.id}
        id={list.id}
        name={list.name}
        imageUrls={list.imageUrls}
        description={list.description}
        gigCount={list.gigCount}
        onEditSuccess={handleOnEditList}
        onDeactivateFavouriteList={handleDeactivateFavListSuccess}
      />
    ))}
    </div>
  ) : (
    <div className="no-history" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '100%' }}>
      <svg width="300" height="163" viewBox="0 0 300 163" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M0 162.11H300" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M158.208 17.1036C162.931 17.1036 166.76 13.2748 166.76 8.5518C166.76 3.82877 162.931 0 158.208 0C153.485 0 149.656 3.82877 149.656 8.5518C149.656 13.2748 153.485 17.1036 158.208 17.1036Z" fill="#B8B6F6"></path><path d="M69.5051 75.9201C74.0383 75.9201 77.7133 72.2451 77.7133 67.7119C77.7133 63.1786 74.0383 59.5037 69.5051 59.5037C64.9718 59.5037 61.2969 63.1786 61.2969 67.7119C61.2969 72.2451 64.9718 75.9201 69.5051 75.9201Z" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M178.87 102.27L168.539 141.043H252.133" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M273.581 95.3525L279.338 73.7363H186.467L186.062 75.2787" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M171.005 130.552H252.217H258.417L261.196 135.034L252.66 134.599L252.217 141.043H168.539L171.005 130.552Z" fill="#232426"></path><path d="M217.239 99.5901C220.621 87.0755 228.142 78.287 234.296 79.8294C240.596 81.4023 243.077 93.2297 239.825 106.248C236.572 119.267 228.829 128.544 222.523 126.971C219.606 126.246 217.338 122.878 216.246 118.664" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M192.805 4.26807C192.805 4.26807 174.892 18.8519 168.555 42.8275L191.652 52.1887C197.188 39.2306 205.651 27.7313 216.376 18.5923L192.805 4.26807Z" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M192.453 10.5522L207.755 20.0814" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M189.125 14.6677L204.159 24.2885" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M185.656 19.4783L200.889 28.2439" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M182.551 24.2886L198.005 32.283" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M179.375 29.7556L194.684 36.4062" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M180.528 36.4061C178.436 35.7494 174.244 38.9335 177.351 40.8042C180.459 42.6749 189.988 38.9335 188.614 37.8492C187.239 36.765 182.406 40.2315 183.781 42.6138C185.155 44.9961 190.569 43.3773 190.278 41.8884C189.988 40.3995 186.949 41.9648 189.125 47.0195" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M102.484 45.5765C102.484 45.5765 112.502 54.0443 118.084 62.8786L140.708 41.6366C138.42 34.7399 134.565 28.4673 129.445 23.3113L113.747 39.5062L111.288 36.8109L102.484 45.5765Z" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M131.645 25.7165L139.746 20.1196C139.746 20.1196 143.121 34.2377 140.708 41.7053" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M252.219 135.034H280.142V148.145C280.146 149.981 279.787 151.8 279.087 153.498C278.387 155.196 277.359 156.739 276.062 158.039C274.765 159.339 273.224 160.371 271.528 161.075C269.831 161.778 268.013 162.141 266.176 162.141V162.141C262.472 162.141 258.92 160.669 256.301 158.05C253.682 155.431 252.211 151.879 252.211 148.175V135.034H252.219Z" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M238.655 161.942L225.018 155.757L229.454 139.585H208.701L204.356 155.757L190.727 161.942H238.655Z" fill="#232426"></path><path d="M62.5664 72.0946C62.5664 72.0946 72.8286 67.6279 71.729 56.2585" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M71.3631 59.7176C71.3631 59.7176 64.1781 58.9006 62.1012 64.1767C60.0244 69.4529 62.1012 72.5147 63.6284 71.5145C65.1555 70.5142 72.3023 64.9021 71.3631 59.7176Z" fill="#232426"></path><path d="M26.8242 118.084L46.3864 123.123L43.7293 143.647C43.7293 143.647 61.207 145.671 75.2182 147.114C87.0686 148.328 92.4058 151.031 92.4058 151.031V122.146L98.331 125.963L114.114 107.356C114.114 107.356 97.43 89.5648 90.9092 86.7015C78.3946 81.2116 52.6858 83.5175 52.6858 83.5175C42.4923 86.5717 37.7354 90.0917 33.2457 100.43C30.7522 106.18 28.6079 112.075 26.8242 118.084Z" fill="#232426"></path><path d="M266.175 135.034C265.518 125.635 257.371 108.027 253.767 104.973C250.163 101.919 250.575 103.164 250.781 105.561C250.988 107.959 251.27 119.404 255.92 126.269C260.57 133.133 261.532 134.927 261.532 134.927" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M263.861 124.894C263.059 120.718 260.7 104.141 262.089 97.4064C263.479 90.6718 262.784 90.061 263.861 87.7016C264.938 85.3422 267.358 98.2386 268.122 103.858C268.885 109.478 269.374 129.3 268.122 135.027" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M268.738 128.979C269.922 120.718 272.839 119.267 274.984 114.273C277.13 109.28 277.626 106.225 278.313 105.874C279 105.523 282.62 115.854 280.535 120.435C278.451 125.016 271.8 128.888 271.869 135.027" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M268.234 104.813C268.952 99.3612 271.174 94.4363 271.938 92.2831C272.701 90.1299 274.641 98.1777 274.083 102.69C273.526 107.203 275.404 114.029 273.686 116.824" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M240.932 90.42C236.778 93.2986 223.462 101.346 208.572 99.5827C193.683 97.8188 188.407 82.6699 188.407 82.6699C188.407 82.6699 185.391 72.8277 184.215 69.2849C183.78 67.9639 182.299 66.2306 181.161 68.4297C179.901 70.9418 183.52 84.6628 183.52 84.6628" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M237.769 112.601C237.769 112.601 219.444 120.443 205.288 119.107C191.132 117.77 180.335 105.271 176.273 99.2543C175.425 98.0021 169.118 91.6188 168.072 88.4348C167.026 85.2508 168.355 83.9222 170.157 85.2431C171.959 86.5641 176.395 91.2065 176.395 91.2065L176.639 91.0003C176.639 91.0003 172.822 78.0199 172.18 74.8054C171.485 71.2243 171.539 67.3455 173.417 67.1164C174.883 66.9408 175.784 69.8728 176.533 73.4539C176.815 74.7672 179.655 86.2816 179.655 86.2816" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M177.122 76.0118C176.434 71.2472 174.724 63.94 176.244 62.9092C177.465 62.0693 178.794 62.5809 179.58 65.8795C180.298 68.8955 181.237 73.7364 181.237 73.7364" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M44.5445 162.141C38.8637 162.141 31.5031 162.904 25.7688 155.635C17.6827 145.327 29.9607 101.24 38.2605 91.7944C46.5603 82.3493 56.456 82.7311 67.1992 83.1281C81.7678 83.7161 90.6402 84.8614 97.1839 90.924C103.728 96.9866 121.618 114.808 121.618 114.808C121.618 114.808 140.264 88.4195 141.6 85.2432" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M240.931 90.42C240.931 90.42 236.35 93.4742 232.7 95.3525C229.05 97.2308 217.238 99.5902 217.238 99.5902C217.238 99.5902 223.202 79.6768 233.051 79.6768C238.427 79.6768 240.931 90.42 240.931 90.42Z" fill="black"></path><path d="M223.729 127.147C233.915 125.024 237.771 112.639 237.771 112.639L223.729 117.221L216.414 119.153C216.414 119.153 217.819 128.376 223.729 127.147Z" fill="black"></path><path d="M82.119 150.817C86.7004 152.535 95.9852 156.597 97.9551 157.956C99.2684 158.857 98.6271 160.583 95.466 159.598C92.3048 158.613 81.0195 154.428 81.0195 154.428" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M80.4766 147.037C84.0882 147.61 92.5407 150.977 95.4193 152.779C98.5575 154.749 97.6641 157.048 93.8922 155.833" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M92.1992 158.491C93.6805 159.01 97.315 160.926 95.4672 162.11" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M80.4766 157.872L92.785 162.11" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M153.282 100.018L135.88 129.392C134.688 131.331 133.117 133.011 131.261 134.329C129.405 135.648 127.302 136.578 125.078 137.065C122.854 137.552 120.554 137.585 118.317 137.163C116.08 136.74 113.951 135.871 112.058 134.607L97.6875 125.017" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M141.387 85.5104C142.784 82.1889 144.953 75.1642 146.052 71.881C146.457 70.6593 147.839 69.0253 148.862 71.1174C150.023 73.4615 146.64 86.1899 146.64 86.1899" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M153.076 92.0235C153.076 92.0235 156.672 79.9517 157.252 77.0044C157.902 73.683 157.856 70.079 156.115 69.8652C154.756 69.6896 153.916 72.4155 153.206 75.7369C152.946 76.9586 150.281 87.6407 150.281 87.6407" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M153.078 100.293C153.925 99.0482 160.248 92.6573 161.294 89.4962C162.34 86.3351 161.026 84.9836 159.224 86.2969C157.422 87.6102 152.971 92.245 152.971 92.245L152.727 92.0388" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path><path d="M152.635 78.1495C153.276 73.7286 154.88 66.9482 153.475 65.9861C152.337 65.2226 151.108 65.6731 150.367 68.7349C149.695 71.5295 148.84 76.0268 148.84 76.0268" stroke="#232426" strokeWidth="1.5" strokeMiterlimit="10"></path></svg>
        <div className="no-history-title">No favourite lists yet.</div>
        <div className="no-history-desc">Start exploring gigs to build your history.</div>
    </div>
  )}
       

        
      </div>
    </SellerPage>
  );
}
