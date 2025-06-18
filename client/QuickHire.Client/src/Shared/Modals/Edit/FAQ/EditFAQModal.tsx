import { useEffect, useState } from "react";
import { EditModal } from "../Common/EditModal";
import { useTooltip } from "../../../Forms/Common/Tooltips/Tooltip";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import axios from "../../../../axiosInstance";
import { isAxiosError } from "axios";

export interface EditFAQModalModalProps {
  id: number;
  showModal: boolean;
  onClose: () => void;
  onEditSuccess(id: number, newName: string, newDescription: string): void;
  initialQuestion: string;
  initialAnswer: string;
}

export function EditFAQModal({onEditSuccess,
  showModal, id,
  onClose, initialQuestion,
  initialAnswer,
}: EditFAQModalModalProps) {
  const [question, setNewQuestion] = useState<string>(initialQuestion);
  const [answer, setNewAnswer] = useState<string>(initialAnswer);
  const handleQuestionChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setNewQuestion(event.target.value);
    setValidationErrors({ Question: [] });
  };
  const handleAnswerChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setNewAnswer(event.target.value);
    setValidationErrors({ Answer: [] });
  };
  const [showQuestionTooltip, handleShowQuestionTooltip] = useTooltip();
  const [showAnswerTooltip, handleShowAnswerTooltip] = useTooltip();

  const [validationErrors, setValidationErrors] = useState<{Question?: string[]; Answer?: string[];}>({});

  useEffect(() => {
    if (showModal) {
      setNewQuestion(initialQuestion);
      setNewAnswer(initialAnswer);      
      setValidationErrors({});
    }
  }, [showModal, initialQuestion, initialAnswer]);

  const onEditSuccessInternal = async () => {
    try {
      const url = `https://localhost:7267/faqs`;
      const formData = new FormData();
      formData.append("id", id.toString());
      formData.append("question", question);
      formData.append("answer", answer);

      const response = await axios.put(url, formData);

      console.log("FAQ edited successfully:", response.data);
      setValidationErrors({});
      onEditSuccess(id, question, answer);
      onClose();
    } catch (error: unknown) {
      if (isAxiosError(error) && error.response?.status === 400) {
        setValidationErrors({
          Question: error.response.data.errors?.Question || [],
          Answer: error.response.data.errors?.Answer || [],
        });
      } else {
        console.error("Error editing FAQ:", error);
      }
    }
  };

  if (!showModal) return null;

  return (
    <EditModal onClose={onClose} id={id} onContinue={onEditSuccessInternal}>
       <FormGroup id={"question"} label={"Question"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={question} onChange={handleQuestionChange} placeholder={"Enter Question"} ariaDescribedBy={"question-help"} onShowTooltip={handleShowQuestionTooltip} showTooltip={showQuestionTooltip} error={validationErrors.Question || []} />
        <FormGroup id={"answer"} label={"Answer"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={answer} onChange={handleAnswerChange} placeholder={"Enter Answer"} ariaDescribedBy={"answer-help"} onShowTooltip={handleShowAnswerTooltip} showTooltip={showAnswerTooltip} error={validationErrors.Answer || []} />               
    </EditModal>
  );
}
