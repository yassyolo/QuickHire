import { NewAddedItemsActions } from "../Buttons/NewAddedItemsActions";
import "./NewAddedEducation.css";

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
    <div className="new-added-education d-flex flex-row justify-content-between">
      <div className="d-flex flex-column justify-content-center" style={{gap: "0.5rem"}}>
        <div className="d-flex flex-row">
          <div className="new-institution">{institution} -</div>
      <div className="new-degree">{degree}</div>
        </div>
        
      <div className="d-flex flex-row new-major-end-year">
        <div className="end-year">{endYear}</div>
      <div className="major">{major}</div>
      </div>
      </div>
      
      
       <NewAddedItemsActions onEdit={onEdit} onRemove={onRemove} />   
    </div>
  );
}