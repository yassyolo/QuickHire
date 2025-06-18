import { useEffect, useState } from "react";
import axios from "../../../../../axiosInstance";
import { Dropdown } from "../../../../../Shared/Dropdowns/Common/Dropdown/Dropdown";
import { Checkbox } from "../../../../../Shared/Dropdowns/Common/Checkbox/Checkbox";

interface GigFilter {
  id: number;
  name: string;
  options: FilterOption[];
}

interface FilterOption {
  id: number;
  value: string;
}

export interface SelectedOption {
  gigFilterId: number;
  optionId: number;
}

interface ServiceIncludesDropdownProps {
  selectedOptions: SelectedOption[];
  onApply: (selected: SelectedOption[]) => void;
  subSubCategoryId: number;
}

export function GigMetadata({
  subSubCategoryId,
  selectedOptions,
  onApply,
}: ServiceIncludesDropdownProps) {
  const [filters, setFilters] = useState<GigFilter[]>([]);
  const [internalSelected, setInternalSelected] = useState<SelectedOption[]>(selectedOptions);

  useEffect(() => {
    const fetchFilters = async () => {
      try {
        const params = new URLSearchParams();
    params.append("Id", subSubCategoryId.toString());
    const url = `https://localhost:7267/gig-filters/populate?${params.toString()}`;

    const response = await axios.get<GigFilter[]>(url);
    setFilters(response.data);
      } catch (error) {
        console.error("Error fetching gig filters:", error);
      }
    };

    fetchFilters();
  }, []);

  const isSelected = (gigFilterId: number, optionId: number) => {
    return internalSelected.some(
      (selected) => selected.gigFilterId === gigFilterId && selected.optionId === optionId
    );
  };

  const toggleOption = (gigFilterId: number, optionId: number) => {
    setInternalSelected((prevSelected) => {
      const exists = prevSelected.some(
        (selected) => selected.gigFilterId === gigFilterId && selected.optionId === optionId
      );

      if (exists) {
        return prevSelected.filter(
          (selected) =>
            !(
              selected.gigFilterId === gigFilterId &&
              selected.optionId === optionId
            )
        );
      } else {
        return [...prevSelected, { gigFilterId, optionId }];
      }
    });
  };

  const handleClearAll = () => {
    setInternalSelected([]);
  };

  const handleApply = () => {
    onApply(internalSelected);
  };

  return (

    <Dropdown onClearAll={handleClearAll} onApply={handleApply}>
      {filters.map((filter) => (
        <div key={filter.id} className="filter-item" style={{width: '500px'}}>
          <div className="filter-title">{filter.name}</div>
          <div className="checkbox-list">
            {filter.options.map((option) => (
              <Checkbox
                key={option.id}
                id={option.id}
                label={option.value}
                isSelected={isSelected(filter.id, option.id)}
                onChange={() => toggleOption(filter.id, option.id)}
              />
            ))}
          </div>
          {filter.options.length > 0 && (
  <div className="divider" />
)}

        </div>
      ))}
    </Dropdown>
  );
}
