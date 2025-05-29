import { useEffect, useState } from "react";
import { Item, PopulateDropdown } from "../Common/PopulateDropdown";
import axios from "axios";

export interface CategoryPopulateProps {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: number | undefined) => void;
  selectedId: number | undefined;
  type: string;
}

export function CategoriesPopulate({ show, setShow, setSelectedId, type, selectedId}: CategoryPopulateProps) {
  const [populatedData, setPopulatedData] = useState<Item[]>([]);

  useEffect(() => {
    if (!show) return;
    const fetchData = async () => {
      try {
        if (type === "Main") {
          const response = await axios.get<Item[]>("https://localhost:7267/admin/main-categories/populate");
          setPopulatedData(response.data);
        } else if (type === "Sub") {
           const response = await axios.get<Item[]>(`https://localhost:7267/admin/sub-categories/populate`);
          setPopulatedData(response.data);
        } else if (type === "Sub Sub") {
            const response = await axios.get<Item[]>(`https://localhost:7267/admin/sub-sub-categories/populate`);
          setPopulatedData(response.data);
        }
      } catch (error) {
        console.error("Error fetching categories:", error);
      }
    };
    fetchData();
  }, [show, type]);

  return <PopulateDropdown show={show} setShow={setShow} setSelectedId={setSelectedId} data={populatedData} label="categories" selectedId={selectedId} />
}
