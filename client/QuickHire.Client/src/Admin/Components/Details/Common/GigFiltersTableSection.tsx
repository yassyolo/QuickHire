import { useState } from "react";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { Th } from "../../Tables/Common/Th";
import './SubCategoriesTableSection.css';
import { GigFilter } from "../SubSubCategoryDetails";
import { AddGigFilterModal } from "../../Modals/Add/AddGigFilterModal";
import { IconButton } from "../../../../Shared/Buttons/IconButton/IconButton";
import { EditFiterModal } from "../../Modals/Edit/EditGigFilterModal";
import { DeactivateGigFilterModal } from "../../Modals/Deactivate/DeactivateGigFilter";
import { EditFiterOptionModal } from "../../Modals/Edit/EditFilterOption";
import { DeactivateFilterOptionsModal } from "../../Modals/Deactivate/DeactivateFilterOption";

interface SubCategoryTableSectionProps {
  items: GigFilter[];
  onAddGigFilterSuccess: (newFilter: GigFilter) => void;
  onEditGigFilterSuccess: (id: number, newTitle: string) => void;
  onDeactivateGigFilterSuccess: (id: number) => void;
  onEditFilterOptionSuccess: (id: number, newValue: string) => void;
  onDeactivateFilterOptionSuccess: (id: number) => void;
}

export function SubSubCategoriesTableSection({
  items,
  onAddGigFilterSuccess,
  onEditGigFilterSuccess,
  onDeactivateGigFilterSuccess,
  onEditFilterOptionSuccess,
  onDeactivateFilterOptionSuccess
}: SubCategoryTableSectionProps) {
  const [showAddGigFilterModal, setShowAddGigFilterModal] = useState(false);
  const [editingGigFilterId, setEditingGigFilterId] = useState<number | null>(null);
  const [deactivatingGigFilterId, setDeactivatingGigFilterId] = useState<number | null>(null);
  const [editingOptionId, setEditingOptionId] = useState<number | null>(null);
  const [deactivatingOptionId, setDeactivatingOptionId] = useState<number | null>(null);
  const [showEditFilterModal, setShowEditFilterModal] = useState(false);

  const handleOnEditSuccessAndClose = () => {
    setShowEditFilterModal(!showEditFilterModal);
  }

  return (
    <div className="sub-categories-section">
      <div className="sub-categories-header-button d-flex flex-row justify-content-between">
        <div className="sub-categories-header">Filters</div>
        <ActionButton
          onClick={() => setShowAddGigFilterModal(true)}
          text="Add gig filter"
          className="add-category-button"
          ariaLabel="Add gig filter Button"
        />
        {showAddGigFilterModal && (
          <AddGigFilterModal
            onClose={() => setShowAddGigFilterModal(false)}
            title="gig filter"
            showModal={true}
            onAddGigFilterSuccess={onAddGigFilterSuccess}
            subSubCategoryId={0}
          />
        )}
      </div>

      <div className="sub-categories-list">
        <table>
          <thead>
            <tr>
              <Th title="Filter" />
              <Th title="Options" />
            </tr>
          </thead>
          <tbody>
            {items.map((item) => (
              <tr key={item.id}>
                <td>
                  {item.title}
                  <IconButton
                    icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }} />}
                    onClick={() => setDeactivatingGigFilterId(item.id)}
                    className="faq-delete-button"
                    ariaLabel="Deactivate Gig Filter Button"
                  />
                  <IconButton
                    icon={<i className="bi bi-pencil" style={{ fontSize: "18px" }} />}
                    onClick={() => setEditingGigFilterId(item.id)}
                    className="faq-edit-button"
                    ariaLabel="Edit Gig Filter Button"
                  />

                  {editingGigFilterId === item.id && (
                    <EditFiterModal
                      id={item.id}
                      showModal={editingGigFilterId === item.id}
                      onEditSuccessAndClose={handleOnEditSuccessAndClose}
                      onClose={() => setEditingGigFilterId(null)}
                      onEditSuccess={onEditGigFilterSuccess}
                      name={item.title}
                    />
                  )}

                  {deactivatingGigFilterId === item.id && (
                    <DeactivateGigFilterModal
                      onClose={() => setDeactivatingGigFilterId(null)}
                      onDeactivateSuccess={onDeactivateGigFilterSuccess}
                      showModal={deactivatingGigFilterId === item.id}
                      id={item.id}
                    />
                  )}
                </td>

                <td>
                  {item.items.map((option) => (
                    <div key={option.id} className="option-row d-flex align-items-center mb-1">
                      <span className="option-value flex-grow-1">{option.value}</span>

                      <IconButton
                        icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }} />}
                        onClick={() => setDeactivatingOptionId(option.id)}
                        className="faq-delete-button"
                        ariaLabel={`Delete option ${option.value}`}
                      />

                      <IconButton
                        icon={<i className="bi bi-pencil" style={{ fontSize: "18px" }} />}
                        onClick={() => setEditingOptionId(option.id)}
                        className="faq-edit-button"
                        ariaLabel={`Edit option ${option.value}`}
                      />

                      {editingOptionId === option.id && showEditFilterModal && (
                        <EditFiterOptionModal
                          id={option.id}
                          showModal={true}
                          onClose={() => setEditingOptionId(null)}
                          onEditSuccess={onEditFilterOptionSuccess}
                          name={option.value}
                        />
                      )}

                      {deactivatingOptionId === option.id && (
                        <DeactivateFilterOptionsModal
                          onClose={() => setDeactivatingOptionId(null)}
                          onDeactivateSuccess={onDeactivateFilterOptionSuccess}
                          showModal={true}
                          id={option.id}
                        />
                      )}
                    </div>
                  ))}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
