import { useEffect, useState } from "react";
import "./NewSellerForm.css";

import { WizardList } from "../../../../../Shared/Wizard/WizardList";
import { Skill, UserLanguage } from "../../SellerProfile/SellerProfile";
import axios from "../../../../../axiosInstance";

import { Language } from "../../SellerProfile/Forms/EditLanguageModalForm";
import { PersonalInfoStep } from "../Steps/NewSellerStep1";
import { ProfessionalInfoStep } from "../Steps/NewSellerStep2";
import { Certification } from "../../SellerProfile/Forms/EditCErtificationModalForm";
import { useNavigate } from "react-router-dom";

interface Education {
  id: number;
  institution: string;
  degree: string;
  endYear: string;
  major: string;
}

export function NewSellerForm() {
  const [activeStep, setActiveStep] = useState(1);

  const [fullName, setFullName] = useState("");
  const [username, setUsername] = useState("");
  const [description, setDescription] = useState("");
  const [profilePicture, setProfilePicture] = useState<File | null>(null);
  const [profilePictureUrl, setProfilePictureUrl] = useState<string | null>(null);
  const [newLanguages, setNewLanguages] = useState<UserLanguage[]>([]);
  const [availableLanguages, setAvailableLanguages] = useState<Language[]>([]);
  const [selectedLanguageId, setSelectedLanguageId] = useState<number | null>(null);
  const navigate = useNavigate();

  const [certifications, setCertifications] = useState<Certification[]>([]);
  const [certificationInput, setCertificationInput] = useState("");
  const [issuerInput, setIssuerInput] = useState("");
  const [dateInput, setDateInput] = useState("");

  const [skills, setSkills] = useState<Skill[]>([]);
  const [skillInput, setSkillInput] = useState("");

  const [educations, setEducations] = useState<Education[]>([]);
  const [institutionInput, setInstitutionInput] = useState("");
  const [degreeInput, setDegreeInput] = useState("");
  const [endYearInput, setEndYearInput] = useState("");
  const [majorInput, setMajorInput] = useState("");
  const [selectedMainCategoryId, setSelectedMainCategoryId] = useState<number | null>(null);

  const [validationErrors, setValidationErrors] = useState<{
    FullName?: string[];
    Username?: string[];
    Description?: string[];
    ProfilePicture?: string[];
    Certification?: string[];
    Issuer?: string[];
    Date?: string[];
    Skill?: string[];
    Institution?: string[];
    Degree?: string[];
    EndYear?: string[];
    Major?: string[];
  }>({});

  useEffect(() => {
    fetchUserDetails();
    fetchAvailableLanguages();
  }, []);

  const fetchUserDetails = async () => {
    try {
      const res = await axios.get("https://localhost:7267/seller/new");
      setFullName(res.data.fullName || "");
      setUsername(res.data.username || "");
      setDescription(res.data.description || "");
      setProfilePictureUrl(res.data.profilePictureUrl || null);
      setNewLanguages(res.data.languages || []);
    } catch (err) {
      console.error("Error fetching user details:", err);
    }
  };

  const fetchAvailableLanguages = async () => {
    try {
      const res = await axios.get<Language[]>("https://localhost:7267/languages/populate");
      setAvailableLanguages(res.data);
    } catch (err) {
      console.error("Error fetching languages:", err);
    }
  };

  const handleAddLanguage = () => {
    if (selectedLanguageId === null) return;
    const selected = availableLanguages.find(l => l.id === selectedLanguageId);
    if (!selected) return;

    const newLang: UserLanguage = {
      languageId: selected.id,
      languageName: selected.name,
    };
    setNewLanguages(prev => [...prev, newLang]);
    setSelectedLanguageId(null);
  };

  const handleRemoveLanguage = (id: number) => {
    setNewLanguages(prev => prev.filter(l => l.languageId !== id));
  };

  const handleAddCertification = () => {
    const errors: typeof validationErrors = {};
    if (!certificationInput) errors.Certification = ["Name is required"];
    if (!issuerInput) errors.Issuer = ["Issuer is required"];
    if (!dateInput) errors.Date = ["Date is required"];
    setValidationErrors(errors);
    if (Object.keys(errors).length > 0) return;

    const newCert: Certification = {
      id: Date.now(),
      certification: certificationInput,
      issuer: issuerInput,
      date: dateInput,
    };
    setCertifications(prev => [...prev, newCert]);
    setCertificationInput("");
    setIssuerInput("");
    setDateInput("");
  };

  const handleRemoveCertification = (id: number) => {
    setCertifications(prev => prev.filter(c => c.id !== id));
  };

  const handleEditCertification = (cert: Certification) => {
    setCertificationInput(cert.certification);
    setIssuerInput(cert.issuer);
    setDateInput(cert.date);
    handleRemoveCertification(cert.id);
  };

  const handleAddSkill = () => {
    if (!skillInput) {
      setValidationErrors(prev => ({ ...prev, Skill: ["Skill name required"] }));
      return;
    }

    const newSkill: Skill = {
      id: Date.now(),
      name: skillInput,
    };
    setSkills(prev => [...prev, newSkill]);
    setSkillInput("");
  };

  const handleRemoveSkill = (id: number) => {
    setSkills(prev => prev.filter(s => s.id !== id));
  };

  const handleEditSkill = (skill: Skill) => {
    setSkillInput(skill.name);
    handleRemoveSkill(skill.id);
  };

  const handleAddEducation = () => {
    const errors: typeof validationErrors = {};
    if (!institutionInput) errors.Institution = ["Institution is required"];
    if (!degreeInput) errors.Degree = ["Degree is required"];
    if (!endYearInput) errors.EndYear = ["End year is required"];
    if (!majorInput) errors.Major = ["Major is required"];
    setValidationErrors(errors);
    if (Object.keys(errors).length > 0) return;

    const newEdu: Education = {
      id: Date.now(),
      institution: institutionInput,
      degree: degreeInput,
      endYear: endYearInput,
      major: majorInput,
    };
    setEducations(prev => [...prev, newEdu]);
    setInstitutionInput("");
    setDegreeInput("");
    setEndYearInput("");
    setMajorInput("");
  };

  const handleRemoveEducation = (id: number) => {
    setEducations(prev => prev.filter(e => e.id !== id));
  };

  const handleEditEducation = (edu: Education) => {
    setInstitutionInput(edu.institution);
    setDegreeInput(edu.degree);
    setEndYearInput(edu.endYear);
    setMajorInput(edu.major);
    handleRemoveEducation(edu.id);
  };

  const handleNextStep = () => {
    const errors: typeof validationErrors = {};
    if (!fullName) errors.FullName = ["Full name is required"];
    if (!username) errors.Username = ["Username is required"];
    if (!description) errors.Description = ["Description is required"];
    if (!profilePicture && !profilePictureUrl) errors.ProfilePicture = ["Profile picture is required"];

    setValidationErrors(errors);
    if (Object.keys(errors).length === 0 && activeStep < steps.length) {
      setActiveStep(activeStep + 1);
    }
  };

const handleSaveNewUser = async () => {
  const formData = new FormData();
  console.log("industryId", selectedMainCategoryId);
  formData.append("IndustryId", selectedMainCategoryId?.toString() || "0");
  formData.append("FullName", fullName);
  formData.append("Username", username);
  formData.append("Description", description);

  if (profilePicture) {
    formData.append("ProfilePicture", profilePicture);
  } else {
    formData.append("ProfilePicture", ""); 
  }

  newLanguages.forEach((l) => {
    formData.append("Languages", l.languageId.toString());
  });

  certifications.forEach((c, index) => {
    formData.append(`Certifications[${index}].Certification`, c.certification);
    formData.append(`Certifications[${index}].Issuer`, c.issuer);
    formData.append(`Certifications[${index}].Date`, c.date); 
  });

  skills.forEach((skill, index) => {
    formData.append(`Skills[${index}].Name`, skill.name.toString());
  });

  educations.forEach((education, index) => {
    formData.append(`Educations[${index}].Institution`, education.institution);
    formData.append(`Educations[${index}].Degree`, education.degree);
    formData.append(`Educations[${index}].Major`, education.major);
    formData.append(`Educations[${index}].EndYear`, education.endYear.toString());
  });

  try {
    const res = await axios.post("https://localhost:7267/seller/new", formData);
    if (res.status === 200) {
      console.log("User created successfully");
      navigate("/seller/profile");
    }
  } catch (err) {
    console.error("Error saving user:", err);
  }
};



  const steps = [
    {
      title: "Personal info",
      isValid: !!fullName && !!username && !!description && (!!profilePicture || !!profilePictureUrl),
      content: (
        <PersonalInfoStep
          fullName={fullName}
          username={username}
          description={description}
          profilePicture={profilePicture}
          profilePictureUrl={profilePictureUrl}
          selectedLanguageId={selectedLanguageId}
          availableLanguages={availableLanguages}
          newLanguages={newLanguages}
          validationErrors={validationErrors}
          onChange={{
            fullName: setFullName,
            username: setUsername,
            description: setDescription,
            profilePicture: (file) => {
              setProfilePicture(file);
              setValidationErrors(prev => ({ ...prev, ProfilePicture: [] }));
            },
            selectedLanguageId: setSelectedLanguageId,
          }}
          onAddLanguage={handleAddLanguage}
          onRemoveLanguage={handleRemoveLanguage}
          onNextStep={handleNextStep}
        />
      ),
    },
    {
      title: "Professional info",
      isValid: certifications.length > 0 || skills.length > 0 || educations.length > 0,
      content: (
        <ProfessionalInfoStep
          certifications={certifications}
          skills={skills}
          educations={educations}
          validationErrors={validationErrors}
          certificationInput={certificationInput}
          issuerInput={issuerInput}
          dateInput={dateInput}
          skillInput={skillInput}
          institutionInput={institutionInput}
          degreeInput={degreeInput}
          endYearInput={endYearInput}
          majorInput={majorInput}
          onChange={{
            certificationInput: setCertificationInput,
            issuerInput: setIssuerInput,
            dateInput: setDateInput,
            skillInput: setSkillInput,
            institutionInput: setInstitutionInput,
            degreeInput: setDegreeInput,
            endYearInput: setEndYearInput,
            majorInput: setMajorInput,
          }}
          onAddCertification={handleAddCertification}
          onRemoveCertification={handleRemoveCertification}
          onEditCertification={handleEditCertification}
          onAddSkill={handleAddSkill}
          onRemoveSkill={handleRemoveSkill}
          onEditSkill={handleEditSkill}
          onAddEducation={handleAddEducation}
          onRemoveEducation={handleRemoveEducation}
          onEditEducation={handleEditEducation}
          onNextStep={handleSaveNewUser} 
           categoryId={selectedMainCategoryId}  
  onChangeCategoryId={setSelectedMainCategoryId}        />
      ),
    },
  ];

  return (
    <div className="new-seller-wizard">
      <WizardList steps={steps} activeStep={activeStep} onStepChange={setActiveStep} />
    </div>
  );
}
