import React from "react";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { Th } from "../../../../../Shared/Tables/Common/Th/Th";
import './SubCategoriesTableSection.css';


interface CategoryItem {
  id: number;
  name: string;
  imageUrl?: string;
}

interface SubCategoryTableSectionProps {
  title: string;
  addButtonLabel: string;
  items: CategoryItem[];
  onAddClick: () => void;
  renderActions: (item: CategoryItem) => React.ReactNode;
  renderModals?: (item: CategoryItem) => React.ReactNode;
  addModal: React.ReactNode;
  className?: string;
}

export function SubCategoriesTableSection ({ title, addButtonLabel, items, onAddClick, renderActions, renderModals, addModal,} : SubCategoryTableSectionProps)  {
  return (
    <div className="sub-categories-section">
      <div className="sub-categories-header-button d-flex flex-row justify-content-between">
        <div className="sub-categories-header">{title}</div>
        <ActionButton onClick={onAddClick} text={addButtonLabel} className="add-category-button" ariaLabel={`Add ${title} Button`}  />{addModal}
      </div>

      <div className="sub-categories-list">
        <table>
          <thead>
            <tr>
              <Th title="Name" />
              <Th title="Actions" />
            </tr>
          </thead>
          <tbody>
            {items.map((item) => (
              <React.Fragment key={item.id}>
                <tr>
                  <td>{item.name}</td>
                  <td>{renderActions(item)}</td>
                </tr>
                {renderModals && renderModals(item)}
              </React.Fragment>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

