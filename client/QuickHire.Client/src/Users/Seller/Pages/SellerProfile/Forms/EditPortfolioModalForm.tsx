import { useEffect, useState, ChangeEvent } from "react";
import axios from "../../../../../axiosInstance";
import { useTooltip } from "../../../../../Shared/Tooltip/Tooltip";
import { AddDetailsModal } from "./AddDetailsForm";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup";
import { SelectDropdown } from "../../../../../Shared/Select/SelectDropdown";
import { DetailsModalButtons } from "../Modals/ModalButtons/DetailsModalButtons";
import { Item } from "../../../../../Admin/Components/Dropdowns/Common/PopulateDropdown";
import { NewAddedPortfolio } from "../NewAddedItems/Portfolio/NewAddedPortfolio";
import { Portfolio } from "../SellerProfile";

interface PortfolioWithFile extends Portfolio {
  imageFile?: File | null;
}

interface EditProjectPortfolioModalFormProps {
  initialData: Portfolio[];
  onEditSuccess: (updated: Portfolio[]) => void;
}

export function EditProjectPortfolioModalForm({
  initialData,
  onEditSuccess,
}: EditProjectPortfolioModalFormProps) {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [mainCategoryId, setMainCategoryId] = useState<number | null>(null);
  const [existingImageUrl, setExistingImageUrl] = useState("");
  const [newImage, setNewImage] = useState<File | null>(null);

  const [categories, setCategories] = useState<Item[]>([]);
  const [validationErrors, setValidationErrors] = useState<Record<string, string[]>>({});
  const [portfolios, setPortfolios] = useState<PortfolioWithFile[]>([]);
  const [editingPortfolio, setEditingPortfolio] = useState<PortfolioWithFile | null>(null);

  const [showTitleTooltip, handleShowTitleTooltip] = useTooltip();
  const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
  const [showMainCategoryTooltip, handleShowMainCategoryTooltip] = useTooltip();
  const [showImageTooltip, handleShowImageTooltip] = useTooltip();

  useEffect(() => {
    setPortfolios(initialData.map((p) => ({ ...p, imageFile: null })));
  }, [initialData]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await axios.get<Item[]>(
          "https://localhost:7267/admin/main-categories/populate"
        );
        setCategories(response.data);
      } catch (error) {
        console.error("Error fetching categories:", error);
      }
    };

    fetchCategories();
  }, []);

  const validate = () => {
    const errors: Record<string, string[]> = {};
    if (!title.trim()) errors.title = ["Title is required."];
    if (!description.trim()) errors.description = ["Description is required."];
    if (!mainCategoryId) errors.mainCategoryId = ["Main category is required."];
    setValidationErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const clearForm = () => {
    setTitle("");
    setDescription("");
    setMainCategoryId(null);
    setExistingImageUrl("");
    setNewImage(null);
    setEditingPortfolio(null);
    setValidationErrors({});
  };

  const handleFileChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const target = e.target as HTMLInputElement;
    if (target.files?.length) {
      setNewImage(target.files[0]);
    }
  };

  const handleAddOrUpdate = () => {
    if (!validate()) return;

    const updatedPortfolio: PortfolioWithFile = {
      id: editingPortfolio ? editingPortfolio.id : Math.floor(Math.random() * -1000),
      title,
      description,
      mainCategoryId: mainCategoryId!,
      mainCategoryName: categories.find((c) => c.id === mainCategoryId)?.name || "",
      imageUrl: newImage ? URL.createObjectURL(newImage) : existingImageUrl,
      imageFile: newImage ?? editingPortfolio?.imageFile ?? null,
    };

    setPortfolios((prev) =>
      editingPortfolio
        ? prev.map((p) => (p.id === editingPortfolio.id ? updatedPortfolio : p))
        : [...prev, updatedPortfolio]
    );

    clearForm();
  };

  const handleEdit = (portfolio: PortfolioWithFile) => {
    setEditingPortfolio(portfolio);
    setTitle(portfolio.title);
    setDescription(portfolio.description);
    setMainCategoryId(portfolio.mainCategoryId);
    setExistingImageUrl(portfolio.imageUrl);
    setNewImage(null);
  };

  const handleRemove = (portfolio: PortfolioWithFile) => {
    setPortfolios((prev) => prev.filter((p) => p.id !== portfolio.id));
    if (editingPortfolio?.id === portfolio.id) clearForm();
  };

  const handleSave = async () => {
    try {
    const formData = new FormData();

    portfolios.forEach((portfolio, index) => {
      formData.append(`Portfolios[${index}].Id`, portfolio.id.toString());
      formData.append(`Portfolios[${index}].Title`, portfolio.title);
      formData.append(`Portfolios[${index}].Description`, portfolio.description);
      formData.append(`Portfolios[${index}].MainCategoryId`, portfolio.mainCategoryId.toString());

      // Append image only if changed (if imageFile exists)
      if (portfolio.imageFile) {
        formData.append(`Portfolios[${index}].Image`, portfolio.imageFile);
      }
    });
await axios.put("https://localhost:7267/seller/profile/portfolio", formData);

      onEditSuccess(portfolios);
      clearForm();
    } catch (err) {
      console.error("Failed to save portfolios:", err);
    }
  };

  return (
    <div className="edit-form-wrapper space-y-4">
      <AddDetailsModal onSave={handleAddOrUpdate}>
        <FormGroup
          id="title"
          label="Title"
          tooltipDescription="Enter the title of your project."
          type="text"
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Project Title"
          onShowTooltip={handleShowTitleTooltip}
          showTooltip={showTitleTooltip}
          error={validationErrors.title}
        />

        <FormGroup
          id="description"
          label="Description"
          tooltipDescription="Briefly describe your project."
          type="text"
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          placeholder="Project Description"
          onShowTooltip={handleShowDescriptionTooltip}
          showTooltip={showDescriptionTooltip}
          error={validationErrors.description}
        />

        <SelectDropdown
          id="main-category"
          label="Main category"
          options={categories}
          value={mainCategoryId ?? undefined}
          onChange={(val) => setMainCategoryId(val ?? null)}
          getOptionLabel={(opt) => opt.name}
          getOptionValue={(opt) => Number(opt.id)}
          tooltipDescription="Choose the main category this project belongs to."
          showTooltip={showMainCategoryTooltip}
          onShowTooltip={handleShowMainCategoryTooltip}
          ariaDescribedBy=""
        />
        {validationErrors.mainCategoryId && (
          <div className="validation-error">{validationErrors.mainCategoryId[0]}</div>
        )}

        <FormGroup
          id="image"
          label="Image"
          tooltipDescription="Upload a project image."
          type="file"
          onChange={handleFileChange}
          showTooltip={showImageTooltip}
          onShowTooltip={handleShowImageTooltip}
        />

        {(existingImageUrl || newImage) && (
          <div className="preview-section mt-3">
            <NewAddedPortfolio
              title={title}
              description={description}
              imageUrl={newImage ? URL.createObjectURL(newImage) : existingImageUrl}
              mainCategoryName={
                categories.find((c) => c.id === mainCategoryId)?.name || ""
              }
              onEdit={() => {}}
              onRemove={() => {
                setNewImage(null);
                setExistingImageUrl("");
              }}
            />
          </div>
        )}
      </AddDetailsModal>

      {portfolios.map((port) => (
        <NewAddedPortfolio
          key={port.id}
          title={port.title}
          description={port.description}
          imageUrl={port.imageUrl}
          mainCategoryName={port.mainCategoryName}
          onEdit={() => handleEdit(port)}
          onRemove={() => handleRemove(port)}
        />
      ))}

      <div className="description-buttons">
        <DetailsModalButtons onSave={handleSave} onClear={clearForm} />
      </div>
    </div>
  );
}
