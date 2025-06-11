import { Link, useNavigate } from "react-router-dom";
import "./BuyerProfilePage.css";
import axios from "../../../axiosInstance";
import { UserLanguage } from "../../Seller/Pages/SellerProfile/SellerProfile";
import { useEffect, useState } from "react";
import { LanguageTag } from "../../Seller/Pages/SellerProfile/Tags/Language/LanguageTag";
import { EditLanguageModalForm } from "../../Seller/Pages/SellerProfile/Forms/EditLanguageModalForm";
import { SellerPage } from "../../Seller/Pages/Common/SellerPage";
import { AddOrEditDetailsModal } from "../../Seller/Pages/SellerProfile/Modals/EditOrDeleteModal/AddOrEditDetailsModal";
import { AddBuyerDetailsModal } from "../../../Admin/Components/Modals/Add/BuyerDetails/AddBuyerDetails";
import { useAuth } from "../../../AuthContext";
import { ProfileChecklist } from "./ProfileChecklist/ProfileChecklist";
import { EditBuyerDetailsModal } from "../../../Admin/Components/Modals/Add/BuyerDetails/EditBuyerDetails";
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
    const [showEditBuyerDetailsModal, setShowEditBuyerDetailsModal] = useState(false);

    const handleEditBuyerDetailsModalVisbility = () => {
        setShowEditBuyerDetailsModal(!showEditBuyerDetailsModal);
    }

      const { switchMode } = useAuth();  
    const navigate = useNavigate();
  const handleSwitchToSelling = () => {
    switchMode("seller");
    navigate("/seller/dashboard");
  };
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

    const handleEditBuyerDetailsSuccess = (image: string, description: string) => {
        setBuyerDetails(prev => {
            if (!prev) return prev;
            return {
                ...prev,
                profilePictureUrl: image,
                description: description
            };
        });
        setShowEditBuyerDetailsModal(false);
    }

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
                    <div className="d-flex flex-row">
                                            <div className="currently-in-buyer-mode-description">Go to your seller profile by clicking </div> <div className="link-to-seller" onClick={handleSwitchToSelling}>here</div>

                    </div>
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
                <ProfileChecklist
                    buyerDetails={
                        buyerDetails
                            ? {
                                ...buyerDetails,
                                languages: buyerDetails.languages
                                    ? buyerDetails.languages.map(lang => ({ language: lang.languageName }))
                                    : []
                            }
                            : null
                    }
                    handleEditLanguagesModalShow={handleEditLanguagesModalShow}
                    handleAddBuyerDetailsModalVisbility={handleAddBuyerDetailsModalVisbility}
                    handleEditBuyerDetailsModalVisbility={handleEditBuyerDetailsModalVisbility}
                />        </div>

        </div>
       </SellerPage>
               {showEditLanguagesModal && (
                <AddOrEditDetailsModal title={"Languages"} onClose={handleEditLanguagesModalShow} show={true}><EditLanguageModalForm
                initialLanguages={buyerDetails?.languages || []}
                onEditSuccess={handleOnEditLanguagesSuccess}
            /></AddOrEditDetailsModal>            
        )}
                    {showAddBuyerDetailsModal && <AddBuyerDetailsModal showModal={true} onClose={handleAddBuyerDetailsModalVisbility} onAddBuyerDetailsSuccess={onAddBuyerDetailsSuccess}/>}
                    {showEditBuyerDetailsModal && (
                        <EditBuyerDetailsModal
                            showModal={true}
                            onClose={handleEditBuyerDetailsModalVisbility}
                            onEditBuyerDetailsSuccess={handleEditBuyerDetailsSuccess}
                            initialDescription={buyerDetails?.description ?? ""}
                            initialImageUrl={buyerDetails?.profilePictureUrl ?? ""}
                        />
                    )}

        </>
    );
}