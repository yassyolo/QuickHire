import { useMemo } from "react";
import { ProfileChecklistItem } from "../ProfileChecklistItem";
import "./ProfileChecklist.css";

interface ProfileChecklistProps {
  buyerDetails: {
    profilePictureUrl?: string;
    languages: { language: string }[];
  } | null;
    handleEditLanguagesModalShow: () => void;
    handleAddBuyerDetailsModalVisbility: () => void;
    handleEditBuyerDetailsModalVisbility: () => void;
}

export function ProfileChecklist({ buyerDetails, handleEditLanguagesModalShow, handleAddBuyerDetailsModalVisbility, handleEditBuyerDetailsModalVisbility } : ProfileChecklistProps) {
  const totalSteps = 2;

  const completedSteps = useMemo(() => {
    let count = 0;
    if (buyerDetails?.languages && buyerDetails.languages.length > 0) count++;
    if (buyerDetails?.profilePictureUrl) count++;
    return count;
  }, [buyerDetails]);

  const completionPercentage = Math.round((completedSteps / totalSteps) * 100);

  return (
    <div className="profile-checklist">
      <div className="profile-checklist-header d-flex align-items-center justify-content-between">
        <span>Profile checklist</span>
        <span className="complete-profile">{completionPercentage}% complete</span>
      </div>

      {buyerDetails?.languages.length === 0 ? (
        <ProfileChecklistItem
          title="Set communication preferences"
          description="Let people know languages you speak"
          buttonName="Add"
          onButtonClick={handleEditLanguagesModalShow}
        />
      ) : (
        <ProfileChecklistItem
          title="Set communication preferences"
          description="Let people know languages you speak"
          buttonName="Edit"
          onButtonClick={handleEditLanguagesModalShow}
        />
      )}

      {!buyerDetails?.profilePictureUrl ? (
        <ProfileChecklistItem
          title="Add details for your profile"
          description="Add photo and details for better personalization"
          buttonName="Add"
          onButtonClick={handleAddBuyerDetailsModalVisbility}
        />
      ) : (
        <ProfileChecklistItem
          title="Edit details for your profile"
          description="Add photo and details for better personalization"
          buttonName="Edit"
          onButtonClick={handleEditBuyerDetailsModalVisbility}
        />
      )}
    </div>
  );
}
