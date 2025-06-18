import { useEffect, useState } from "react";
import { WizardList } from "../../../../../../Shared/Wizard/WizardList";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { SelectDropdown } from "../../../../../../Shared/Dropdowns/Select/SelectDropdown";
import axios from "../../../../../../axiosInstance"; 
import "./PostProjectBriefForm.css";
import { ActionButton } from "../../../../../../Shared/Buttons/ActionButton/ActionButton";
import { isAxiosError } from "axios";
import { useNavigate } from "react-router-dom";

interface CategoryItem{
    id: number;
    name: string;
}

interface PostProjectBriefFormProps {
    onBack: () => void;
}

export function PostProjectBriefForm({ onBack }: PostProjectBriefFormProps) {
    const [activeStep, setActiveStep] = useState<number>(1);
    const [aboutBuyer, setAboutBuyer] = useState<string>("");
    const [showAboutBuyerTooltip, handleShowAboutBuyerTooltip] = useTooltip();
    const [description, setDescription] = useState<string>("");
    const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
    const [subSubCategories , setSubSubCategories] = useState<CategoryItem[]>([]);
    const [subSubCategoryId, setSubSubCategoryId] = useState<number>(0);
    const [showSubSubCategoryTooltip, handleShowSubSubCategoryTooltip] = useTooltip();
    const [deliveryDays, setDeliveryDays] = useState<string>("");
    const [showDeliveryDaysTooltip, handleShowDeliveryDaysTooltip] = useTooltip();
    const [budget, setBudget] = useState<string>("");
    const [showBudgetTooltip, handleShowBudgetTooltip] = useTooltip();
    const navigate = useNavigate();
    const [validationErrors, setValidationErrors] = useState<{ AboutBuyer?: string[]; Description?: string[]; Budget?: string[];DeliveryDays?: string[]; }>({});

    const fetchSubSubCategories = async() => {
        try {
            const response = await axios.get<CategoryItem[]>("https://localhost:7267/sub-sub-categories/populate");
            setSubSubCategories(response.data);
        } catch (error) {
            console.error("Error fetching sub-subcategories:", error);
        }
    }

    useEffect(() => {
        setActiveStep(1);
        setValidationErrors({});
        fetchSubSubCategories();
    }, []);

    const handleSelectSubSubCategoryId = (value: number | undefined) => {
        if (value !== undefined) {
            setSubSubCategoryId(value);
        }
    }

    const handleShowNextStep = () => {
  const errors: typeof validationErrors = {};

  if (activeStep === 1) {
    if (!aboutBuyer) errors.AboutBuyer = ["This field is required"];
    if (!description) errors.Description = ["This field is required"];
    if (subSubCategoryId <= 0) errors.Description = ["Please select a sub-subcategory"];
  }

  if (activeStep === 2) {
    if (!deliveryDays) errors.DeliveryDays = ["Delivery time is required"];
    if (!budget) errors.Budget = ["Budget is required"];
  }

  setValidationErrors(errors);

  if (Object.keys(errors).length === 0 && activeStep < steps.length) {
    setActiveStep(activeStep + 1);
  }
};

const handleAddProjectBrief = async () => {
    setValidationErrors({});

    const errors: typeof validationErrors = {};
    if (!deliveryDays) errors.DeliveryDays = ["Delivery time is required"];
    if (!budget) errors.Budget = ["Budget is required"];
    setValidationErrors(errors);
    if (Object.keys(errors).length > 0) {
        return;
    }
    else {
        try {
            await axios.post("https://localhost:7267/buyers/project-briefs", {
                aboutBuyer,
                description,
                subSubCategoryId,
                deliveryDays,
                budget
            });
            setActiveStep(1);
            setAboutBuyer("");
            setDescription("");
            setSubSubCategoryId(0);
            setDeliveryDays("");
            setBudget("");
            navigate("/buyer/my-project-briefs");
        }
        catch (error: unknown) {
            if (isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors({
                    AboutBuyer: error.response.data.errors?.AboutBuyer || [],
                    Description: error.response.data.errors?.Description || [],
                    Budget: error.response.data.errors?.Budget || [],
                    DeliveryDays: error.response.data.errors?.DeliveryDays || []
                });
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    }
};


   const steps = [
    {
      title: "Share brief description",
    isValid: !!aboutBuyer && !!description && subSubCategoryId > 0,

      content: (
        <div className="wizard-form">
          <FormGroup
            id={"aboutBuyer"}
            label={"Tell us about yourself?"}
            tooltipDescription={"Briefly introduce yourself to help the seller understand your background."}
            type={"textarea"}
            value={aboutBuyer}
            onChange={(e) => setAboutBuyer(e.target.value)}
            placeholder={"e.g., I’m a startup founder looking to build a prototype."}
            ariaDescribedBy={"name-help"}
            onShowTooltip={handleShowAboutBuyerTooltip}
            showTooltip={showAboutBuyerTooltip}
            error={validationErrors.AboutBuyer || []}
          />
          <FormGroup
            id={"description"}
            label={"What are you looking for to get done?"}
            tooltipDescription={"Explain what you want done in clear and concise terms."}
            type={"textarea"}
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder={"e.g., I need a mobile app for food delivery."}
            ariaDescribedBy={"description-help"}
            onShowTooltip={handleShowDescriptionTooltip}
            showTooltip={showDescriptionTooltip}
            error={validationErrors.Description || []}
          />
          <div className="subcategories-choices">
            <SelectDropdown
              id="sub-sub-category"
              label="Category"
              options={subSubCategories}
              value={subSubCategoryId}
              onChange={handleSelectSubSubCategoryId}
              getOptionLabel={(opt) => opt.name}
              getOptionValue={(opt) => opt.id}
              tooltipDescription={"Choose a more specific category to help find the right seller."}
              showTooltip={showSubSubCategoryTooltip}
              ariaDescribedBy={"dropdown-help"}
              onShowTooltip={handleShowSubSubCategoryTooltip}
            />
      
          </div>
          <div className="d-flex flex-row justify-content-between align-content-center" style={{marginTop: '20px'}}>
             <ActionButton
            text="Back"
            onClick={onBack}
            className="back-button"
            ariaLabel="On Back button"
          />
                      <ActionButton text={"Save and continue"} onClick={handleShowNextStep} className={"save-and-continue-button"} ariaLabel={"Show next step button"}               />
          </div>
        </div>
      ),
    },
    {
      title: "Add timeline and budget",
          isValid: !!deliveryDays && !!budget,

      content: (
        <div className="wizard-form">
            <FormGroup
              id={"deliveryDays"}
label={"In how many days should the project be completed?"}
              tooltipDescription={"Specify how long the project should take to complete. Use numbers only."}
              type={"text"}
              value={deliveryDays}
              onChange={(e) => setDeliveryDays(e.target.value)}
              placeholder={"e.g., 7"}
              ariaDescribedBy={"delivery-days-help"}
              onShowTooltip={handleShowDeliveryDaysTooltip}
              showTooltip={showDeliveryDaysTooltip}
              error={validationErrors.DeliveryDays || []}
            />
            <FormGroup
              id={"budget"}
              label={"What is your budget?"}
              tooltipDescription={"Provide an estimated total budget for the project. Use numbers only."}
              type={"text"}
              value={budget}
              onChange={(e) => setBudget(e.target.value)}
              placeholder={"e.g., 1500"}
              ariaDescribedBy={"budget-help"}
              onShowTooltip={handleShowBudgetTooltip}
              showTooltip={showBudgetTooltip}
              error={validationErrors.Budget || []}
            />

            <ActionButton text={"Save and continue"} onClick={handleAddProjectBrief} className={"save-and-continue-button"} ariaLabel={"Add project brief button"} />             

        </div>
      ),
    },
  ];

  return (
    <div className="project-brief-wizard">
        <WizardList steps={steps} activeStep={activeStep} onStepChange={setActiveStep} />
    </div>
  );
}
