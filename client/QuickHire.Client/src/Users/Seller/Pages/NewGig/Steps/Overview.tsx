import { useEffect, useState } from "react";
import axios from "../../../../../axiosInstance";
import { SelectDropdown } from "../../../../../Shared/Dropdowns/Select/SelectDropdown";
import { FormGroup } from "../../../../../Shared/Forms/FormGroup/FormGroup";
import { useTooltip } from "../../../../../Shared/Forms/Common/Tooltips/Tooltip";

interface SubSubCategory {
  id: number;
  name: string;
}

interface Props {
  selectedSubSubCategoryId: number | null;
  onChangeSubSubCategoryId: (value: number | null) => void;
  title: string;
  tags: string;
  onChangeTitle: (value: string) => void;
  onChangeTags: (value: string) => void;
}

export function Overview({
  selectedSubSubCategoryId,
  onChangeSubSubCategoryId,
  title,
  tags,
  onChangeTitle,
  onChangeTags}: Props) {
  const [subSubCategories, setSubSubCategories] = useState<SubSubCategory[]>([]);
  const [showDescriptionTooltip, handleShowDescriptionTooltip] = useTooltip();
  const [showTagsTooltip, handleShowTagsTooltip] = useTooltip();
  const [showDropdownTooltip, handleShowDropdownTooltip] = useTooltip();

  useEffect(() => {
    const fetchSubSubCategories = async () => {
      try {
        const response = await axios.get<SubSubCategory[]>("https://localhost:7267/sub-sub-categories/populate");
        setSubSubCategories(response.data);
      } catch (error) {
        console.error("Error fetching sub-sub-categories:", error);
      }
    };
    fetchSubSubCategories();
  }, []);

  return (
    <div className="wizard-form">
      <div className="brief-description-title">Overview</div>
      <FormGroup
        id="title"
        label="Title"
        type="textarea"
        value={title}
        onChange={(e) => onChangeTitle(e.target.value)}
        placeholder="Write catchy title..."
        tooltipDescription="Provide a clear and detailed title for your gig."
        showTooltip={showDescriptionTooltip}
        onShowTooltip={handleShowDescriptionTooltip}
        ariaDescribedBy="title-help"
      />
      <SelectDropdown
        id="sub-sub-category"
        label="Select a relevant sub-sub-category"
        options={subSubCategories}
        value={selectedSubSubCategoryId === null ? undefined : selectedSubSubCategoryId}
        onChange={(value) => onChangeSubSubCategoryId(value === undefined ? null : value)}
        getOptionLabel={(opt) => opt.name}
        getOptionValue={(opt) => opt.id}
        tooltipDescription="Choose the specific area that your gig fits into. This improves discoverability."
        showTooltip={showDropdownTooltip}
        ariaDescribedBy="sub-sub-category-help"
        onShowTooltip={handleShowDropdownTooltip}
      />

      <FormGroup
        id="tags"
        label="Tags"
        type="text"
        value={tags}
        onChange={(e) => onChangeTags(e.target.value)}
        placeholder="Add tags (comma separated)"
        tooltipDescription="Add relevant keywords separated by commas to help users find your gig."
        showTooltip={showTagsTooltip}
        onShowTooltip={handleShowTagsTooltip}
        ariaDescribedBy="tags-help"
      />

      
    </div>
  );
}
