import { Link } from "react-router-dom";
import axios from "axios";
import "./BuyerProfilePage.css";
import { UserLanguage } from "../../Seller/Pages/SellerProfile/SellerProfile";
import { useEffect, useState } from "react";
import { LanguageTag } from "../../Seller/Pages/SellerProfile/Tags/Language/LanguageTag";
import { ProfileChecklistItem } from "./ProfileChecklistItem";
import { EditLanguageModalForm } from "../../Seller/Pages/SellerProfile/Forms/EditLanguageModalForm";
import { SellerPage } from "../../Seller/Pages/Common/SellerPage";
import { AddOrEditDetailsModal } from "../../Seller/Pages/SellerProfile/Modals/EditOrDeleteModal/AddOrEditDetailsModal";
import { AddBuyerDetailsModal } from "../../../Admin/Components/Modals/Add/AddBuyerDetails";
interface BuyerDetails{
    profilePictureUrl: string;
    fullName: string;
    username: string;
    description: string;
    memberSince: string;
    location: string;
    languages: UserLanguage[];
}

export function BuyerProfilePage() {
    const [buyerDetails, setBuyerDetails] = useState<BuyerDetails | null>(null);
    const [showEditLanguagesModal, setShowEditLanguagesModal] = useState(false);
    const [showAddBuyerDetailsModal, setShowAddBuyerDetailsModal] = useState(false);
    const handleAddBuyerDetailsModalVisbility = () => {
        setShowAddBuyerDetailsModal(!showAddBuyerDetailsModal);
    }
    const handleEditLanguagesModalShow = () => {
        setShowEditLanguagesModal(!showEditLanguagesModal);
    }

   const fetchBuyerDetails = async () => {
        try {
            const response = await axios.get<BuyerDetails>("https://localhost:7267/buyers/profile");
            setBuyerDetails(response.data);
        } catch (error) {
            console.error("Error fetching buyer details:", error);
        }
    }

    const handleOnEditLanguagesSuccess = (updatedLanguages: UserLanguage[]) => {
        setBuyerDetails(prev => {
            if (!prev) return prev;
            return {
                ...prev,
                languages: updatedLanguages
            };
        });
        setShowEditLanguagesModal(false);
    };

    const onAddBuyerDetailsSuccess = (image: string, description: string) => {
        setBuyerDetails(prev => {
            if (!prev) return prev;
            return {
                ...prev,
                profilePictureUrl: image,
                description: description
            };
        });
        setShowAddBuyerDetailsModal(false);
    }

    useEffect(() => {
        fetchBuyerDetails();
    }, []);

    return (
        <>
       <SellerPage>
         <div className="buyer-profile-page d-flex flex-row">
            <div className="buyer-profile-page-left">
                <div className="buyer-details-card">
                    <div className="buyer-details-card-header">
                        <div className="buyer-card-image-wrapper">
                            <img className="buyer-profile-picture" src={buyerDetails?.profilePictureUrl} alt="Profile" />
                        </div>
                            <div className="buyer-full-name">{buyerDetails?.fullName}</div>
                            <div className="buyer-username">@{buyerDetails?.username}</div>
                    </div>
                    <div className="divider"></div>
                    {buyerDetails?.description && <><div className="buyer-description">{buyerDetails?.description}</div><div className="divider"></div></>}
                    <div className="buyer-card-item">
                        <div className="buyer-card-label">Member since:</div>
                        <div className="buyer-card-value">{buyerDetails?.memberSince} </div>
                    </div>
                    {buyerDetails?.location &&
                    <div className="buyer-card-item">
                        <div className="buyer-card-label">Location:</div>
                        <div className="buyer-card-value">{buyerDetails?.location}</div>
                    </div>}
                    {buyerDetails?.languages && buyerDetails?.languages.length > 0 &&
                     <div className="buyer-card-item">
                            <div className="buyer-card-label">Languages:</div>
                            <div className="buyer-languages">{buyerDetails?.languages.map((lang, index) => (
                            <LanguageTag key={index} languageName={lang.languageName} onButtonClick={handleEditLanguagesModalShow} showActions={true}/>                                
                        ))}</div>

                     </div>}
                   
                </div>
            </div>
            <div className="buyer-profile-page-right">
                <div className="currently-in-buyer-mode">
                    <div className="currently-in-buyer-mode-header">You are currently in client profile</div>
                    <div className="currently-in-buyer-mode-description">Go to your seller profile by clicking <Link className="link-to-seller" to={"/seller/dashboard"}>here</Link></div>
                </div>
                <div className="get-started-with-our-platform">
                    <div className="get-started-with-our-platform-header">Get started with our platform</div>
                    <div className="get-started-with-our-platform-item item1">
                        <Link className="link-to-create" to={"/seller/create-gig"}>Create new gig <i className="bi bi-arrow-right"></i> </Link>
                                            </div>
                    <div className="get-started-with-our-platform-item item2">
                        <Link className="link-to-create" to={"/buyer/project-briefs"}> Post service requirements <i className="bi bi-arrow-right"></i> </Link>
                    </div>
                    <div className="get-started-with-our-platform-item item3">
                        <Link className="link-to-create" to={"/buyer/dashboard"}> Start exploring <i className="bi bi-arrow-right"></i> </Link>
                    </div>
                </div>
                <div className="profile-checklist">
                    <div className="profile-checklist-header">Profile checklist</div>
                    {buyerDetails?.languages.length === 0 ?
                    (<ProfileChecklistItem title={"Set communication preferences"} description={"Let people know languages do you speak"} buttonName={"Add"} onButtonClick={handleEditLanguagesModalShow}></ProfileChecklistItem>) : 
                    (<ProfileChecklistItem title={"Set communication preferences"} description={"Let people know languages do you speak"} buttonName={"Edit"} onButtonClick={handleEditLanguagesModalShow}></ProfileChecklistItem>)}

                     {!buyerDetails?.profilePictureUrl ?
                    (<ProfileChecklistItem title={"Add details for your profile"} description={"Add photo and details for better personalization"} buttonName={"Add"} onButtonClick={handleAddBuyerDetailsModalVisbility}></ProfileChecklistItem>) :
                    (<ProfileChecklistItem title={"Edit details for your profile"} description={"Add photo and details for better personalization"} buttonName={"Edit"} onButtonClick={() => console.log("Add profile picture")}></ProfileChecklistItem>)}
                    
                </div>
            </div>

        </div>
       </SellerPage>
               {showEditLanguagesModal && (
                <AddOrEditDetailsModal title={"Languages"} onClose={handleEditLanguagesModalShow} show={true}><EditLanguageModalForm
                initialLanguages={buyerDetails?.languages || []}
                onEditSuccess={handleOnEditLanguagesSuccess}
            /></AddOrEditDetailsModal>            
        )}
                    {showAddBuyerDetailsModal && <AddBuyerDetailsModal showModal={true} onClose={handleAddBuyerDetailsModalVisbility} onAddBuyerDetailsSuccess={onAddBuyerDetailsSuccess}/>}

        </>
    );
}