import { useState, useEffect } from "react";
import axios from "../../axiosInstance";
import { FormGroup } from "../../Shared/Forms/FormGroup/FormGroup";
import { SelectDropdown } from "../../Shared/Dropdowns/Select/SelectDropdown";
import "./SendWorkModal.css";
import { IconButton } from "../../Shared/Buttons/IconButton/IconButton";
import { ImagePreview } from "../../Shared/Images/ImagePreview/ImagePreview";
import { DeliveryPayload, RevisionPayload } from "../Pages/OrderDetails/OrderChat/OrderChat";

interface Item {
  id: number;
  name: string;
}

export interface SendWorkResponse {
  delivery?: DeliveryPayload;
  revision?: RevisionPayload;
}

export interface SendWorkModalProps {
  onClose: () => void;
  orderId: number;
  onWorkSubmitted: (response: SendWorkResponse, type: "delivery" | "revision") => void; 
}
export function SendWorkModal({ onClose, orderId, onWorkSubmitted }: SendWorkModalProps) {
  const [populatedData, setPopulatedData] = useState<Item[]>([]);
  const [selectedType, setSelectedType] = useState<number | undefined>();
  const [description, setDescription] = useState<string>("");
  const [image, setImage] = useState<File | null>(null);
  const [imagePreview, setImagePreview] = useState<string | null>(null);
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
      const files = e.target.files;
      if (files.length > 0) {
        const file = files[0];
        setImage(file);
        setImagePreview(URL.createObjectURL(file));
      }
    }
  };

  const handleSubmit = async () => {
    const errors: Record<string, string[]> = {};

    if (!selectedType) errors.Type = ["Please select a type."];
    if (!description.trim()) errors.Description = ["Description cannot be empty."];
    if (!image) errors.Images = ["Please upload at least one image."];

    setValidationErrors(errors);

    if (Object.keys(errors).length > 0) return;

    try {
      setLoading(true);
      const url = "https://localhost:7267/orders/send-work";
      const formData = new FormData();

      formData.append("Id", orderId.toString());
      formData.append("Type", selectedType!.toString());
      formData.append("Description", description);
      formData.append("Image", image!);

      const response = await axios.post(url, formData, {
        headers: {
          "Content-Type": "multipart/form-data"
        }
      });

      const type = selectedType === 1 ? "revision" : "delivery";

      onWorkSubmitted(response.data, type); 

      console.log("Work submitted successfully:", response.data);

      setSelectedType(undefined);
      setDescription("");
      setImage(null);
      setImagePreview(null);
      setValidationErrors({});
        onClose();
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
        <div className="d-flex flex-column" style={{ gap: "1rem", width: "100%" }}>
          <div className="work-content-title">Send your work</div>

          <SelectDropdown
            id="type"
            label="Type"
            options={populatedData}
            value={selectedType}
            onChange={setSelectedType}
            getOptionLabel={(opt) => opt.name}
            getOptionValue={(opt) => opt.id}
            tooltipDescription="Is this a delivery or revision?"
            showTooltip={false}
            ariaDescribedBy={""}
            onShowTooltip={() => {}}
          />

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
            tooltipDescription="Upload an image."
            type="file"
            onChange={handleImagesChange}
            error={validationErrors.Images || []}
          />

          {imagePreview && (
            <ImagePreview src={imagePreview} alt={"Preview of uploaded image"} />
          )}

          <div className="d-flex flex-row justify-content-end">
            <button className="add-faq-button" onClick={handleSubmit} disabled={loading}>
              {loading ? "Submitting..." : "Submit"}
            </button>
          </div>
        </div>

        <IconButton
          icon={<i className="bi bi-x"></i>}
          onClick={onClose}
          className={"close-button"}
          ariaLabel={"Close customOffer preview"}
        />
      </div>
    </div>
  );
}
