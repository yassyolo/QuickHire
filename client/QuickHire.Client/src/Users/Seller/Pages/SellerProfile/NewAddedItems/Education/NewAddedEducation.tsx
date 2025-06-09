import { NewAddedItemsActions } from "../Buttons/NewAddedItemsActions";

interface NewAddedEducationProps {
      institution: string;
  degree: string;
  endYear: string;
  major: string;
    onRemove: () => void;
    onEdit: () => void;
}

export function NewAddedEducation({ institution, degree, onRemove, onEdit, endYear, major,}: NewAddedEducationProps) {
  return (
    <div className="new-added-education">
      <div className="institution">{institution}</div>
      <div className="degree">{degree}</div>
      <div className="d-flex flex-row new-major-end-year">
        <div className="end-year">{endYear}</div>
      <div className="major">{major}</div>
      </div>
      
       <NewAddedItemsActions onEdit={onEdit} onRemove={onRemove} />   
    </div>
  );
}