import { ChangeEvent, useEffect, useRef, useState } from "react";
import { AddModal } from "../Common/AddModal";
import axios from "../../../../../axiosInstance";
import { isAxiosError } from "axios";import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import "./AddSubSubCategoryModal.css";
import { FormLabel } from "../../../../../Shared/Forms/FormLabel/FormLabel";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import { Item } from "../../../../../Shared/Dropdowns/Common/PopulateDropdown";
import { SelectDropdown } from "../../../../../Shared/Dropdowns/Select/SelectDropdown";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";

export interface AddSubCategoryModalProps {
  title: string;
  showModal: boolean;
  onClose: () => void;
  onAddSubSubCategorySuccess: (id: number, name: string) => void;
  showCategoriesPopulate: boolean;
    submitedSubCategoryId: number;
}

interface FilterOption {
  id: number;
  value: string;
}

interface Filter {
  id: number;
  name: string;
  options: FilterOption[];
}

export function AddSubSubCategoryModal({
  title,
  showModal,
  onClose,
  onAddSubSubCategorySuccess,
    showCategoriesPopulate,
    submitedSubCategoryId
}: AddSubCategoryModalProps) {
  const [name, setName] = useState("");
  const [filters, setFilters] = useState<Filter[]>([]);
  const [categories, setCategories] = useState<Item[]>([]);
  const [selectedSubCategoryId, setSelectedSubCategoryId] = useState<number>();
  const [validationErrors, setValidationErrors] = useState<{ Name?: string[]; Options?: string[] }>({});
  const [showNameTooltip, handleShowNameTooltip] = useTooltip();
  const [showSubCategoryTooltip, handleShowSubCategoryTooltip] = useTooltip();
  const [showFilterTooltip, setShowFilterTooltip] = useState(false);

  const counterRef = useRef(0);

  useEffect(() => {
    if (!showModal) {
      resetForm();
    } else {
      fetchCategories();
    }
  }, [showModal]);

  const resetForm = () => {
    setName("");
    setFilters([]);
    setSelectedSubCategoryId(undefined);
    setValidationErrors({});
    counterRef.current = 0;
  };

  const fetchCategories = async () => {
    try {
      const response = await axios.get<Item[]>("https://localhost:7267/admin/sub-categories/populate");
      setCategories(response.data);
    } catch (error) {
      console.error("Error fetching sub-categories:", error);
    }
  };

  const handleNameChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setName(e.target.value);
  };

  const handleAddFilter = () => {
    const newFilter: Filter = {
      id: counterRef.current++,
      name: "",
      options: [],
    };
    setFilters((prev) => [...prev, newFilter]);
  };

  const handleRemoveFilter = (id: number) => {
    setFilters((prev) => prev.filter((f) => f.id !== id));
  };

  const handleFilterNameChange = (id: number, value: string) => {
    setFilters((prev) =>
      prev.map((f) => (f.id === id ? { ...f, name: value } : f))
    );
  };

  const handleAddFilterOption = (filterId: number) => {
    const newOption: FilterOption = { id: counterRef.current++, value: "" };
    setFilters((prev) =>
      prev.map((f) =>
        f.id === filterId ? { ...f, options: [...f.options, newOption] } : f
      )
    );
  };

  const handleRemoveFilterOption = (filterId: number, optionId: number) => {
    setFilters((prev) =>
      prev.map((f) =>
        f.id === filterId
          ? { ...f, options: f.options.filter((o) => o.id !== optionId) }
          : f
      )
    );
  };

  const handleOptionValueChange = (filterId: number, optionId: number, value: string) => {
    setFilters((prev) =>
      prev.map((f) =>
        f.id === filterId
          ? {
              ...f,
              options: f.options.map((o) =>
                o.id === optionId ? { ...o, value } : o
              ),
            }
          : f
      )
    );
  };

  const handleSubmit = async () => {
    try {
      const payload = {
        SubcategoryId: showCategoriesPopulate ? selectedSubCategoryId : submitedSubCategoryId,
        name,
        filters: filters.map((f) => ({
          name: f.name,
          options: f.options.map((o) => o.value),
        })),
      };

      const result = await axios.post("https://localhost:7267/admin/sub-sub-categories/add", payload);
      onAddSubSubCategorySuccess(result.data.id, name);
      onClose();
      resetForm();
    } catch (error: unknown) {
      if (isAxiosError(error) && error.response?.status === 400) {
        const err = error.response.data.errors;
        setValidationErrors({
          Name: err?.Name || [],
          Options: err?.Options || [],
        });
      } else {
        console.error("Error submitting sub-sub-category:", error);
      }
    }
  };

  if (!showModal) return null;

  return (
    <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
      <FormGroup
        id="sub-sub-category-name"
        label="Name"
        type="text"
        value={name}
        onChange={handleNameChange}
        placeholder="Enter Name"
        error={validationErrors.Name}
        tooltipDescription="Use a clear, descriptive name."
        onShowTooltip={handleShowNameTooltip}
        showTooltip={showNameTooltip}
      />

     { showCategoriesPopulate &&  <SelectDropdown
              id="sub-category"
              label="Sub category"
              options={categories}
              value={selectedSubCategoryId}
              onChange={setSelectedSubCategoryId}
              getOptionLabel={(opt) => opt.name}
              getOptionValue={(opt) => Number(opt.id)}
              tooltipDescription="Choose the sub category that this sub-sub-category belongs to."
              onShowTooltip={handleShowSubCategoryTooltip}
              showTooltip={showSubCategoryTooltip} ariaDescribedBy={""}      />}

      <div className="form-group">
        <div className="d-flex justify-content-between">
          <FormLabel
            id="sub-sub-category-filters"
            label="Filters"
            tooltipDescription="Filters let users refine gigs based on specific criteria."
            onShowTooltip={() => setShowFilterTooltip(true)}
            showTooltip={showFilterTooltip}
          />
          <ActionButton
                      text="Add filter +"
                      onClick={handleAddFilter}
                      className="add-filter-button" ariaLabel={""}          />
        </div>

        {filters.map((filter) => (
          <div key={filter.id} className="new-filter-section">
            <div className="title-section d-flex justify-content-between">
              <div className="d-flex align-items-center">
                <input
                  type="text"
                  className="form-control me-2"
                  placeholder="Enter Title"
                  value={filter.name}
                  onChange={(e) =>
                    handleFilterNameChange(filter.id, e.target.value)
                  }
                />
                <IconButton
                            icon={<i className="bi bi-x text-danger fs-5" />}
                            onClick={() => handleRemoveFilter(filter.id)}
                            className="faq-delete-button" ariaLabel={""}                />
              </div>
              <ActionButton
                        text="Add option +"
                        onClick={() => handleAddFilterOption(filter.id)}
                        className="add-filter-button" ariaLabel={""}              />
            </div>

            {filter.options.map((option) => (
              <div key={option.id} className="d-flex mb-2">
                <input
                  type="text"
                  className={`form-control me-2 ${
                    validationErrors.Options?.length ? "error" : ""
                  }`}
                  placeholder="Filter Option"
                  value={option.value}
                  onChange={(e) =>
                    handleOptionValueChange(filter.id, option.id, e.target.value)
                  }
                />
                {(validationErrors.Options?.length ?? 0) > 0 && (
                  <div className="validation-error">
                    {validationErrors.Options![0]}
                  </div>
                )}
                <IconButton
                        icon={<i className="bi bi-x text-danger fs-5" />}
                        onClick={() => handleRemoveFilterOption(filter.id, option.id)}
                        className="faq-delete-button" ariaLabel={""}                />
              </div>
            ))}
          </div>
        ))}
      </div>
    </AddModal>
  );
}
