import { Link } from "react-router-dom";
import { EditLanguageModalForm, UserLanguage } from "../Forms/EditLanguageModalForm";
import { LanguageTag } from "../Tags/Language/LanguageTag";
import "./SellerInfoCard.css";
import { GigCardRating } from "../../../../../Gigs/GigCard/GigCardRating/GigCardRating";
import { AddOrEditDetailsModal } from "../Modals/EditOrDeleteModal/AddOrEditDetailsModal";


interface SellerInfoCardProps {
  sellerDetails: {
    profilePictureUrl: string;
    fullName: string;
    country: string;
    username: string;
    languages: UserLanguage[];
  } | null;
  averageRating?: number;
  totalReviews?: number;
    topRated?: boolean;
    industry?: string;
    memberSince?: string;
  showEditLanguagesModal?: boolean;
  showActions: boolean;
  handleOnEditLanguagesButtonClick?: () => void;
  handleOnEditLanguagesSuccess?: (languages: UserLanguage[]) => void;
  onClose?: () => void;
}

export function SellerInfoCard ({
    averageRating, topRated, industry, memberSince, totalReviews, onClose,
  sellerDetails,
  showEditLanguagesModal,
  handleOnEditLanguagesButtonClick,
  handleOnEditLanguagesSuccess,
    showActions 
} : SellerInfoCardProps) {
  return (
    <div className="seller-info">
      <img
        src={sellerDetails?.profilePictureUrl}
        alt="Profile"
        className="profile-picture"
      />
      <div className="names-country-languages d-flex flex-column">
        <div className="username-full-name d-flex flex-column">
          <Link to="/buyer/settings" className="seller-full-name">
            {sellerDetails?.fullName}
          </Link>
          <div className="seller-username">@{sellerDetails?.username}</div>
        </div>

        {averageRating && totalReviews && topRated &&
        <div className="seller-rating-top-rated-badge d-flex flex-row">
            <GigCardRating reviewsCount={totalReviews} averageRating={averageRating}/>
            <div className="seller-ratin-top-rated-divider"></div>
            <span className="top-rated-seller-badge">Top rated</span>
        </div>      
        }

        {industry && (
          <div className="seller-industry">
            <i className="bi bi-briefcase"></i>
            {industry}
          </div>
        )}

        {memberSince && (
          <div className="seller-member-since d-flex flex-row">
            <div className="seller-member-since-label"> Member since:</div>
            {memberSince}
          </div>
        )}


        <div className="country-languages d-flex flex-row">
                  {sellerDetails?.country && 

          <div className="seller-country">
            <i className="bi bi-geo-alt"></i>
            {sellerDetails?.country}
          </div>
        }

<div className="seller-langiages">
    {sellerDetails?.languages?.map((language, index) => (
            <LanguageTag
                  key={index}
                  languageName={language.languageName}
                  onButtonClick={handleOnEditLanguagesButtonClick ?? (() => {})} showActions={showActions}            />
          ))}

          {showEditLanguagesModal && (
            <AddOrEditDetailsModal
              title={'Language'}
              onClose={onClose ?? (() => {})}
              show={showEditLanguagesModal}
            >
              <EditLanguageModalForm
                initialLanguages={sellerDetails?.languages || []}
                onEditSuccess={handleOnEditLanguagesSuccess ?? (() => {})}
              />
            </AddOrEditDetailsModal>
          )}
</div>
          
        </div>
      </div>
    </div>
  );
};

