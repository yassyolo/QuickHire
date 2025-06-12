import React from "react";
import { DetailsRow } from "../DetailsRow/DetailsRow";
import './CategoryDetails.css';
import { FAQList } from "../../../../../Shared/FAQ/FAQList/FAQList";

interface MainCategoryDetailsProps {
  details: {
    id: number;
    name: string;
    description?: string;
    clicks: number;
    createdOn: string;
    imageUrl?: string;
  };
  showFAQ?: boolean;
  faqMainCategoryId?: number;
  children?: React.ReactNode;
}

export function CategoryDetails ({ details, showFAQ = false, faqMainCategoryId, children} : MainCategoryDetailsProps) {
  return (
    <div className="d-flex flex-column category-details-faq">
      <div className="category-details-section">
        <div className="category-details-top d-flex flex-row justify-content-between">
          <div className="category-details-name">{details.name}</div>
          <div className="category-details-id">ID: {details.id}</div>
        </div>

        {details.description && (<div className="category-details-description">{details.description} </div>)}

        <div className="category-details-row-list">
          <DetailsRow label="Clicks" value={details.clicks} icon={<i className="bi bi-activity"></i>}/>
          <DetailsRow label="Created On" value={details.createdOn} icon={<i className="bi bi-calendar"></i>}/>
          {details.imageUrl && (<DetailsRow label="Image URL" value={details.imageUrl} icon={<i className="bi bi-image"></i>} />)}
        </div>
      </div>

      {showFAQ && faqMainCategoryId !== undefined && ( <div className="category-details-faq-list"> <FAQList mainCategoryId={faqMainCategoryId} showActions={true} title={details.name} /></div>)}
      {children}
    </div>
  );
};

