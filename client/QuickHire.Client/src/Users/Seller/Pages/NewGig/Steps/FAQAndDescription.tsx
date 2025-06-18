import { ChangeEvent, useCallback } from "react";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { FAQFormGroup } from "../../../../../Shared/Forms/FAQ/FAQFormGroup";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { FAQ } from "../../../../../Gigs/Common/FAQ/FAQList/FAQItem/FAQ";

export interface Faq {
  question: string;
  answer: string;
}

export interface FAQAndDescriptionProps {
  description: string;
  onDescriptionChange: (description: string) => void;
  descriptionError?: string[];

  faqs: Faq[];
  onFaqsChange: (faqs: Faq[]) => void;

  faqValidationErrors?: { Question?: string[]; Answer?: string[] };
  setFaqValidationErrors?: (errors: { Question?: string[]; Answer?: string[] }) => void;
}

export function FAQAndDescription({
  description,
  onDescriptionChange,
  descriptionError = [],
  faqs,
  onFaqsChange,
  faqValidationErrors = {},
  setFaqValidationErrors = () => {},
}: FAQAndDescriptionProps) {
  const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
  const [showFAQTooltip, handleShowFAQTooltip] = useTooltip();

  const handleDescriptionInputChange = useCallback(
    (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
      onDescriptionChange(event.target.value);
    },
    [onDescriptionChange]
  );

  const handleAddFAQ = useCallback(
    (question: string, answer: string) => {
      const errors: { Question?: string[]; Answer?: string[] } = {};

      if (!question || question.trim().length < 2 || question.trim().length > 100) {
        errors.Question = ["Question must be between 2 and 100 characters."];
      }

      if (!answer || answer.trim().length < 2 || answer.trim().length > 100) {
        errors.Answer = ["Answer must be between 2 and 100 characters."];
      }

      if (errors.Question || errors.Answer) {
        setFaqValidationErrors(errors);
        return;
      }

      setFaqValidationErrors({});
      const newFAQ: Faq = { question: question.trim(), answer: answer.trim() };
      onFaqsChange([...faqs, newFAQ]);
    },
    [faqs, onFaqsChange, setFaqValidationErrors]
  );

  const handleRemoveFAQ = useCallback(
    (index: number) => {
      onFaqsChange(faqs.filter((_, i) => i !== index));
    },
    [faqs, onFaqsChange]
  );

  return (
    <div style={{display: "flex", flexDirection: "column", gap: "30px"}}>
      <FormGroup
        id={"description"}
        label={"Description"}
        tooltipDescription={"Write a short, catchy description that grabs attention."}
        type={"text"}
        value={description}
        onChange={handleDescriptionInputChange}
        placeholder={"Enter Description"}
        ariaDescribedBy={"description-help"}
        onShowTooltip={handleShowDescriptionTooltip}
        showTooltip={showDescriptionTooltip}
        error={descriptionError}
      />

      <FAQFormGroup
        showAddButton={true}
        id={"faq-input"}
        label={"Add FAQ"}
        tooltipDescription={"Add a common question and its answer for this category."}
        onAdd={handleAddFAQ}
        questionPlaceholder={"Enter question"}
        answerPlaceholder={"Enter answer"}
        ariaDescribedBy={"faq-help"}
        onShowTooltip={handleShowFAQTooltip}
        showTooltip={showFAQTooltip}
        questionError={faqValidationErrors.Question || []}
        answerError={faqValidationErrors.Answer || []}
      />

      {faqs.length > 0 && (
        <div className="faq-list" aria-label={"faq-list"}>
          <div className="faq-list-title">FAQ's</div>
          {faqs.map((faq, index) => (
            <div key={index} className="faq-row" style={{ display: "flex", alignItems: "center", justifyContent: 'center', gap: "80%" }}>
              <FAQ question={faq.question} answer={faq.answer} id={0} showActions={false} />
              <ActionButton
                text={<i className="bi bi-x" style={{ fontSize: "25px", color: "red" }} />}
                onClick={() => handleRemoveFAQ(index)}
                className={"remove-row-button"}
                ariaLabel={"Remove FAQ Button"}
              />
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
