// components/forms/PersonalInfoStep.tsx
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { SelectDropdown } from "../../../../../Shared/Dropdowns/Select/SelectDropdown";
import { NewAddedLanguage } from "../../SellerProfile/NewAddedItems/Language/NewAddedLanguage";
import { UserLanguage } from "../../SellerProfile/SellerProfile";
import { Language } from "../../SellerProfile/Forms/EditLanguageModalForm";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { useEffect } from "react";

interface Props {
  fullName: string;
  username: string;
  description: string;
  profilePicture: File | null;
  selectedLanguageId: number | null;
  availableLanguages: Language[];
  newLanguages: UserLanguage[];
  profilePictureUrl: string | null;


  validationErrors: {
    FullName?: string[];
    Username?: string[];
    Description?: string[];
    ProfilePicture?: string[];
  };

  onChange: {
    fullName: (value: string) => void;
    username: (value: string) => void;
    description: (value: string) => void;
    profilePicture: (file: File | null) => void;
    selectedLanguageId: (id: number | null) => void;
  };

  onAddLanguage: () => void;
  onRemoveLanguage: (id: number) => void;
  onNextStep: () => void;
}

export function PersonalInfoStep({
  fullName,
  username,
  description,
  selectedLanguageId,
  availableLanguages,
  newLanguages,
  validationErrors,
  onChange,
  onAddLanguage,
  onRemoveLanguage,
  profilePictureUrl,
  onNextStep,
}: Props) {
  useEffect(() => {
    console.log(profilePictureUrl);
  }, [profilePictureUrl]);
    const [showLanguageTooltip, handleShowLanguageTooltip] = useTooltip();
          const [showFullNameTooltip, handleShowFullNameTooltip] = useTooltip();
      const [showUsernameTooltip, handleShowUsernameTooltip] = useTooltip();
      const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
      const [showProfilePictureTooltip, handleShowProfilePictureTooltip] = useTooltip();
  return (

    <div className="wizard-form">
      <div className="brief-description-title">Let's get to know you better.</div>
<div style={{display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gap: '20px', border: '1px solid #ccc', padding: '20px', borderRadius: '8px'}}>

      <FormGroup
  id="fullName"
  label="Full Name"
  tooltipDescription="Enter your full legal name."
  type="text"
  value={fullName}
  onChange={(e) => onChange.fullName(e.target.value)}
  placeholder="e.g., John Doe"
  ariaDescribedBy="full-name-help"
  onShowTooltip={handleShowFullNameTooltip}
  showTooltip={showFullNameTooltip}
  error={validationErrors.FullName || []}
/>

<FormGroup
  id="username"
  label="Username"
  tooltipDescription="Choose a unique username for your profile."
  type="text"
  value={username}
  onChange={(e) => onChange.username(e.target.value)}
  placeholder="e.g., johndesigns"
  ariaDescribedBy="username-help"
  onShowTooltip={handleShowUsernameTooltip}
  showTooltip={showUsernameTooltip}
  error={validationErrors.Username || []}
/>
</div>

<FormGroup
  id="description"
  label="Description"
  tooltipDescription="Briefly describe yourself and your skills."
  type="textarea"
  value={description}
  onChange={(e) => onChange.description(e.target.value)}
  placeholder="e.g., I'm a web designer with 5 years of experience in UI/UX."
  ariaDescribedBy="description-help"
  onShowTooltip={handleShowDescriptionTooltip}
  showTooltip={showDescriptionTooltip}
  error={validationErrors.Description || []}
/>

<FormGroup
  id="profilePicture"
  label="Profile Picture"
  tooltipDescription="Upload a clear profile picture."
  type="file"
  onChange={(e) => {
    const file = (e.target as HTMLInputElement).files?.[0] ?? null;
    onChange.profilePicture(file);
  }}
  ariaDescribedBy="profile-picture-help"
  onShowTooltip={handleShowProfilePictureTooltip}
  showTooltip={showProfilePictureTooltip}
  error={validationErrors.ProfilePicture || []}
/>

{profilePictureUrl && (
  <div className="profile-picture-preview">
    <img src={profilePictureUrl} alt="Current profile" style={{ width: 100, height: 100, borderRadius: "50%" }} />
  </div>
)}
<div className="new-seller-title" style={{fontSize: '18px', fontWeight: '600'}}>Languages</div>

<div style={{display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gap: '20px', border: '1px solid #ccc', padding: '20px', borderRadius: '8px'}}>

<SelectDropdown
  id="language"
  label="Language"
  options={availableLanguages}
  value={selectedLanguageId ?? undefined}
  onChange={(value) => onChange.selectedLanguageId(value ?? null)}
  getOptionLabel={(opt) => opt.name}
  getOptionValue={(opt) => opt.id}
  tooltipDescription="Choose a language you speak."
  showTooltip={showLanguageTooltip}
  onShowTooltip={handleShowLanguageTooltip}
  ariaDescribedBy="language-help"
/>

      <ActionButton
        text="Add +"
        onClick={onAddLanguage}
        className="add-new-button"
        ariaLabel="Add new language"
      />
<div style={{display: 'flex', flexDirection: 'row', gap: '20px'}}>
  {newLanguages.map((lang) => (
        <NewAddedLanguage
          key={lang.languageId}
          languageName={lang.languageName}
          onDelete={() => onRemoveLanguage(lang.languageId)}
          onEdit={() => onRemoveLanguage(lang.languageId)} 
        />
      ))}
</div>
      
      </div>

      <ActionButton
        text="Save and continue"
        onClick={onNextStep}
        className="save-and-continue-button"
        ariaLabel="Save personal info and go to next step"
      />
    </div>
  );
}
