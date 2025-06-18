import { useState, useEffect } from "react";
import axios from "../../axiosInstance";
import { FormGroup } from "../../Shared/Forms/FormGroup/FormGroup";
import { SelectDropdown } from "../../Shared/Dropdowns/Select/SelectDropdown";
import "./SendWorkModal.css";
import { IconButton } from "../../Shared/Buttons/IconButton/IconButton";

interface Item {
  id: number;
  name: string;
}

export interface SendWorkModalProps {
  onClose: () => void;
}

export function SendWorkModal({ onClose }: SendWorkModalProps) {
  const [populatedData, setPopulatedData] = useState<Item[]>([]);
  const [selectedType, setSelectedType] = useState<number | undefined>();
  const [description, setDescription] = useState<string>("");
  const [attachment, setAttachment] = useState<File | null>(null);
  const [images, setImages] = useState<File[]>([]);
  const [validationErrors, setValidationErrors] = useState<Record<string, string[]>>({});
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    setPopulatedData([
      { id: 1, name: "Revision" },
      { id: 2, name: "Delivery" }
    ]);
  }, []);

  const handleImagesChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    if (e.target instanceof HTMLInputElement && e.target.files) {
      setImages(Array.from(e.target.files));
    }
  };

  const handleSubmit = async () => {
    const errors: Record<string, string[]> = {};

    if (!selectedType) errors.Type = ["Please select a type."];
    if (!description.trim()) errors.Description = ["Description cannot be empty."];
    if (!attachment) errors.Attachment = ["Please attach a file."];
    if (images.length === 0) errors.Images = ["Please upload at least one image."];

    setValidationErrors(errors);

    if (Object.keys(errors).length > 0) return;

    try {
      setLoading(true);
      const url = "https://localhost:7267/orders/send-work";
      const formData = new FormData();

      formData.append("Type", selectedType!.toString());
      formData.append("Description", description);
      images.forEach((image) => formData.append("Images", image));

      const response = await axios.post(url, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      console.log("Work submitted successfully:", response.data);

      setSelectedType(undefined);
      setDescription("");
      setAttachment(null);
      setImages([]);
      setValidationErrors({});
    } catch (error) {
      console.error("Error submitting work:", error);
      alert("Error submitting work. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="work-overlay">
      <div className="work-content d-flex flex-row">
        <div className="d-flex flex-column" style={{gap: "1rem", width: "100%"}}>
            <div className="work-content-title" >Send Work</div>

        <SelectDropdown
                  id="type"
                  label="Type"
                  options={populatedData}
                  value={selectedType}
                  onChange={setSelectedType}
                  getOptionLabel={(opt) => opt.name}
                  getOptionValue={(opt) => opt.id}
                  tooltipDescription="Is this a delivery or revision?" showTooltip={false} ariaDescribedBy={""} onShowTooltip={function (): void {
                      throw new Error("Function not implemented.");
                  } }        />

        <FormGroup
          id="description"
          label="Description"
          tooltipDescription="Provide details for this delivery or revision."
          type="textarea"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Describe the work you're submitting"
          error={validationErrors.Description || []}
        />

        <FormGroup
          id="images"
          label="Images"
          tooltipDescription="Upload multiple images if needed."
          type="file"
          onChange={handleImagesChange}
          error={validationErrors.Images || []}
        />

        <div className="d-flex flex-row justify-content-end">
          <button className="add-faq-button" onClick={handleSubmit} disabled={loading}>
            {loading ? "Submitting..." : "Submit"}
          </button>
        </div>
        </div>
                                    <IconButton icon={<i className="bi bi-x"></i>} onClick={onClose} className={"close-button"} ariaLabel={"Close customOffer preview"} />
        
      </div>
    </div>
  );
}
