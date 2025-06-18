import { useState } from "react";
import { WizardList } from "../../../../../Shared/Wizard/WizardList";
import axios from "../../../../../axiosInstance";
import { Overview } from "../Steps/Overview";
import { PaymentPlan } from "../../../../../Gigs/Pages/GigPreview/GigPreview";
import { PricingStep } from "../Steps/Pricing";
import { FAQAndDescription } from "../Steps/FAQAndDescription";
import { Faq } from "../Steps/FAQAndDescription";
import { Requirement, Requirements } from "../Steps/Requirements";
import { GalleryUpload } from "../Steps/GalleryUpload";
import { GigMetadata, SelectedOption } from "../Steps/GigMetadata";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";

export function NewGigForm() {
  const [activeStep, setActiveStep] = useState(1);
const [title, setTitle] = useState("");
  const [selectedSubSubCategoryId, setSelectedSubSubCategoryId] = useState<number | null>(null);
  const [description, setDescription] = useState("");
  const [tags, setTags] = useState("");
  const [requirements, setRequirements] = useState<Requirement[]>([]);
  const [requirementsValidationErrors, setRequirementsValidationErrors] = useState<{ Question?: string[] }>({});
  const [plans, setPlans] = useState<PaymentPlan[]>([]);
  const [galleryImages, setGalleryImages] = useState<File[]>([]);
  const [galleryValidationErrors] = useState<string[]>([]);
  const [selectedOptions, setSelectedOptions] = useState<SelectedOption[]>([]);
  const [faqs, setFaqs] = useState<Faq[]>([]);
  const [faqValidationErrors, setFaqValidationErrors] = useState<{ Question?: string[]; Answer?: string[] }>({});

  const handlePublish = async () => {
    try {
      const formData = new FormData();

      formData.append("SubSubCategoryId", String(selectedSubSubCategoryId));
      formData.append("Description", description);
      formData.append("Tags", tags);
      formData.append("Plans", JSON.stringify(plans));
      formData.append("Metadata", JSON.stringify(selectedOptions));
      formData.append("Faqs", JSON.stringify(faqs));
      formData.append("Requirements", JSON.stringify(requirements));
      galleryImages.forEach((file) => formData.append(`GalleryImages`, file));

      await axios.post("/api/gigs", formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });

      alert("Gig published successfully!");
      setActiveStep(1);
    } catch (error: unknown) {
      console.error("Error publishing gig:", error);
    }
  };

    const handleNextStep = () => {
        if (steps[activeStep - 1].isValid) {
        setActiveStep((prev) => prev + 1);
        } else {
        alert("Please fill in all required fields before proceeding.");
        }
    };

  const steps = [
    {
      title: "Overview",
      isValid: title.trim() !== "" && tags.trim() !== "" && selectedSubSubCategoryId !== null,
      content: (
        <><Overview
              selectedSubSubCategoryId={selectedSubSubCategoryId}
              onChangeSubSubCategoryId={setSelectedSubSubCategoryId}
              title={title}
              tags={tags}
              onChangeTitle={setTitle}
              onChangeTags={setTags}/>
              <ActionButton
                  text="Save and continue"
                  onClick={handleNextStep}
                  className="save-and-continue-button"
                  ariaLabel="Save personal info and go to next step" /></>
      ),
    },
    {
      title: "Pricing",
      isValid: plans.length > 0 && plans.every((plan) => plan.price > 0),
      content: (
        <><PricingStep
              onNextStep={() => {
                  setActiveStep((prev) => prev + 1);
              } }
              onPlansChange={setPlans} /></>
      ),
    },
    {
      title: "Metadata",
      isValid: selectedOptions.length > 0,
      content: (
        <><GigMetadata
              selectedOptions={selectedOptions}
              onApply={(options) => {
                  setSelectedOptions(options);
                  setActiveStep((prev) => prev + 1);
              } } /><ActionButton
                  text="Save and continue"
                  onClick={handleNextStep}
                  className="save-and-continue-button"
                  ariaLabel="Save personal info and go to next step" /></>
      ),
    },
    {
      title: "FAQ and Description",
      isValid: faqs.length > 0 && description.trim() !== "",
      content: (
        <><FAQAndDescription
              description={description}
              onDescriptionChange={setDescription}
              faqs={faqs}
              onFaqsChange={setFaqs}
              faqValidationErrors={faqValidationErrors}
              setFaqValidationErrors={setFaqValidationErrors} /><ActionButton
                  text="Save and continue"
                  onClick={handleNextStep}
                  className="save-and-continue-button"
                  ariaLabel="Save personal info and go to next step" /></>
      ),
    },
    {
      title: "Requirements",
      isValid: requirements.every((req) => req.question.trim() !== "") && requirements.length > 0,
      content: (
        <><Requirements
              requirements={requirements}
              onRequirementsChange={setRequirements}
              validationErrors={requirementsValidationErrors}
              setValidationErrors={setRequirementsValidationErrors} /><ActionButton
                  text="Save and continue"
                  onClick={handleNextStep}
                  className="save-and-continue-button"
                  ariaLabel="Save personal info and go to next step" /></>
      ),
    },
    {
      title: "Gallery",
      isValid: galleryImages.length > 0,
      content: (
        <><GalleryUpload
              images={galleryImages}
              onImagesChange={setGalleryImages}
              validationErrors={galleryValidationErrors}
              tooltipDescription="Upload up to 5 images of your work." /><ActionButton
                  text="Save and continue"
                  onClick={handleNextStep}
                  className="save-and-continue-button"
                  ariaLabel="Save personal info and go to next step" /></>
      ),
    },
    {
      title: "Publish",
      isValid: galleryImages.length > 0,      
      content: (
        <button className="primary-button" onClick={handlePublish}>
          Publish Gig
        </button>
      ),
    },
  ];

  return (
    <div className="new-seller-wizard">
      <WizardList steps={steps} activeStep={activeStep} onStepChange={setActiveStep} />
    </div>
  );
}
