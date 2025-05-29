import { useEffect, useState, useCallback } from "react";
import { Dropdown } from "./Dropdown";
import { Checkbox } from "./Checkbox";

interface PopulateDropdownProps {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: number | undefined) => void;
  selectedId: number | undefined;
  data: Item[];
  label: string;
}

export interface Item {
  id?: number | string;
  name: string;
  description?: string;
}

export function PopulateDropdown({show, setShow, setSelectedId, data, label, selectedId,}: PopulateDropdownProps) {
  const [localSelectedId, setLocalSelectedId] = useState<number | undefined>(selectedId);

  useEffect(() => {
    if (show) {
      setLocalSelectedId(selectedId);
    } else {
      setLocalSelectedId(undefined);
    }
  }, [show, selectedId]);

  const handleCheckboxChange = useCallback((id: number) => {
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
          data.map((item) => item.id !== undefined ? (
              <Checkbox key={item.id} id={Number(item.id)} label={item.name} description={item.description} isSelected={localSelectedId === item.id} onChange={handleCheckboxChange}/>
            ) : null
          )) : (
          <div>No {label} available</div>
        )}
      </div>
    </Dropdown>
  );
}
