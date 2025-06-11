import { useEffect, useState } from "react";
import { Item, PopulateDropdown } from "../../Common/PopulateDropdown";
import axios from "../../../../axiosInstance";

export interface CountriesPopulateProps {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: number | undefined) => void;
  selectedId: number | undefined;
}


export function CountriesPopulate({ show, setShow, setSelectedId, selectedId }: CountriesPopulateProps) {
  const [populatedData, setPopulatedData] = useState<Item<number>[]>([]);

  useEffect(() => {
    if (!show) return;
    const fetchData = async () => {
      try {
        const url = "https://localhost:7267/admin/filters/countries";
        const response = await axios.get<Item<number>[]>(url);
        setPopulatedData(
          response.data.map(item => ({
            ...item,
            id: typeof item.id === "string" ? Number(item.id) : item.id
          }))
        );
      } catch (error) {
        console.error("Error fetching countries options:", error);
      }
    };
    fetchData();
  }, [show, populatedData]);

  return <PopulateDropdown show={show} setShow={setShow} setSelectedId={setSelectedId} data={populatedData} label={"countries"} selectedId={selectedId}></PopulateDropdown>
}
