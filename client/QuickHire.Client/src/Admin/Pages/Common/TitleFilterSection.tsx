import { useEffect, useState } from "react";
import axios from "axios";
import "./TitleFilterSection.css";
import { Item } from "../../Components/Dropdowns/Common/PopulateDropdown";


export interface TitleFilterSelectorProps {
  selectedId: number;
  setSelectedId: (id: number) => void;
  endpoint?: string; 
  data?: Item[];
}

export function TitleFilterSelector({selectedId, setSelectedId, data, endpoint}: TitleFilterSelectorProps) {
  const [options, setOptions] = useState<Item[]>([]);

  useEffect(() => {
    if (typeof endpoint === "string" && endpoint) {
      const fetchOptions = async () => {
        try {
          const response = await axios.get<Item[]>(endpoint);
          setOptions(response.data);
        } catch (error) {
          console.error("Error fetching pill options:", error);
        }
      };
      fetchOptions();
    }
    else if (data && data.length > 0) {
      setOptions(data || []);
    }
  }, [endpoint, data]);

  const handleClick = (id: number) => () => setSelectedId(id);

  return (
    <div className="pill-filter-selector">
      {options.length > 0 && (
        <div className="pill-items">
          {options.map(item =>
            typeof item.id === "number" ? (
              <div key={item.id} className={`status-pill ${selectedId === item.id ? "active" : ""}`} onClick={handleClick(item.id)}>
                {item.name}</div>
            ) : null
          )}
        </div>
      )}
    </div>
  );
}
