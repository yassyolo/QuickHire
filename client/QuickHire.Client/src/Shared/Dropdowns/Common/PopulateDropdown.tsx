import { useEffect, useState, useCallback } from "react";
import { Dropdown } from "./Dropdown/Dropdown";
import { Checkbox } from "./Checkbox/Checkbox";

export interface Item<TId = string | number> {
  id?: TId;
  name: string;
  description?: string;
}

interface PopulateDropdownProps<TId> {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: TId | undefined) => void;
  selectedId: TId | undefined;
  data: Item<TId>[];
  label: string;
}

export function PopulateDropdown<TId extends string | number>({ show, setShow, setSelectedId, selectedId, data, label,}: PopulateDropdownProps<TId>) {
  const [localSelectedId, setLocalSelectedId] = useState<TId | undefined>(selectedId);

  useEffect(() => {
    if (show) {
      setLocalSelectedId(selectedId);
    } else {
      setLocalSelectedId(undefined);
    }
  }, [show, selectedId]);

  const handleCheckboxChange = useCallback((id: TId) => {
    setLocalSelectedId((prev) => (prev === id ? undefined : id));
  }, []);

  const handleClearFilter = useCallback(() => {
    setLocalSelectedId(undefined);
    setSelectedId(undefined);
    setShow(false);
  }, [setSelectedId, setShow]);

  const handleOnApply = useCallback(() => {
    setSelectedId(localSelectedId);
    setShow(false);
  }, [localSelectedId, setSelectedId, setShow]);

  return (
    <Dropdown onClearAll={handleClearFilter} onApply={handleOnApply}>
      <div className="populated-dropdown-data-circle-checkbox">
        {data.length > 0 ? (
          data.map((item) =>
            item.id !== undefined ? (
              <Checkbox
                key={item.id.toString()}
                id={item.id}
                label={item.name}
                description={item.description}
                isSelected={localSelectedId === item.id}
                onChange={handleCheckboxChange}
              />
            ) : null
          )
        ) : (
          <div>No {label} available</div>
        )}
      </div>
    </Dropdown>
  );
}
