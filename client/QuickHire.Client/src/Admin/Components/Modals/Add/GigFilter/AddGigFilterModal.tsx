import { useRef, useState } from "react";
import { AddModal } from "../Common/AddModal";
import axios from "../../../../../axiosInstance";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import "../SubSubCategory/AddSubSubCategoryModal.css";
import { FormLabel } from "../../../../../Shared/Forms/FormLabel/FormLabel";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";
import { GigFilter } from "../../../../Pages/Details/Categories/SubSubCategory/SubSubCategoryDetails";
import { FilterItem } from "../../Deactivate/SubSubCategory/DeactivateSubSubCategoryModal";

export interface AddSubCategoryModalProps {
  title: string;
  showModal: boolean;
  onClose: () => void;
  onAddGigFilterSuccess: (filter : GigFilter) => void;
  subSubCategoryId: number;
}

export function AddGigFilterModal({
    subSubCategoryId,
  title,
  showModal,
  onClose,
  onAddGigFilterSuccess,
}: AddSubCategoryModalProps) {
  
  const [filters, setFilters] = useState<GigFilter[]>([]); 
  const [showFilterTooltip, handleShowFilterTooltip] = useTooltip();

  const counterRef = useRef(0);

  const resetForm = () => {
    setFilters([]);
    counterRef.current = 0;
  };

  const handleAddFilter = () => {
    const newFilter: GigFilter = {
      id: counterRef.current++,
      title: "",
      items: [],
    };
    setFilters((prev) => [...prev, newFilter]);
  };

  const handleRemoveFilter = (id: number) => {
    setFilters((prev) => prev.filter((f) => f.id !== id));
  };

  const handleFilterNameChange = (id: number, value: string) => {
  setFilters((prev) =>
    prev.map((f) => (f.id === id ? { ...f, title: value } : f))
  );
};

  const handleAddFilterOption = (filterId: number) => {
    const newOption: FilterItem = { id: counterRef.current++, value: "" };
    setFilters((prev) =>
      prev.map((f) =>
        f.id === filterId ? { ...f, items: [...f.items, newOption] } : f
      )
    );
  };

  const handleRemoveFilterOption = (filterId: number, optionId: number) => {
    setFilters((prev) =>
      prev.map((f) =>
        f.id === filterId
          ? { ...f, options: f.items.filter((o) => o.id !== optionId) }
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
              options: f.items.map((o) =>
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
        SubSubCategoryId : subSubCategoryId,
        filters: filters.map((f) => ({
          title: f.title,
          options: f.items.map((o) => o.value),
        })),
      };

      const response = await axios.post("https://localhost:7267/admin/sub-sub-categories/add", payload);
      onAddGigFilterSuccess(response.data);
      onClose();
      resetForm();
    } catch (error: unknown) {    
        console.error("Error submitting gig filter:", error);
    }
  };

  if (!showModal) return null;

  return (
    <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>
     

      <div className="form-group">
        <div className="d-flex justify-content-between">
          <FormLabel
            id="sub-sub-category-filters"
            label="Filters"
            tooltipDescription="Filters let users refine gigs based on specific criteria."
            onShowTooltip={handleShowFilterTooltip}
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
                  value={filter.title}
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

            {filter.items.map((option) => (
              <div key={option.id} className="d-flex mb-2">
                <input
                  type="text"                
                  placeholder="Filter Option"
                  value={option.value}
                  onChange={(e) =>
                    handleOptionValueChange(filter.id, option.id, e.target.value)
                  }
                />             
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
