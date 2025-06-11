import { useEffect, useState } from "react";
import { Item, PopulateDropdown } from "../../Common/PopulateDropdown";
import axios from "../../../../axiosInstance";

export interface PriceRangePopulateProps {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: number | undefined) => void;
  selectedId: number | undefined;
}

export function PriceRangePopulate({ show, setShow, setSelectedId, selectedId }: PriceRangePopulateProps) {
  const [populatedData, setPopulatedData] = useState<Item<number>[]>([]);

  useEffect(() => {
    if (!show) return;
    const fetchData = async () => {
      try {
        const url = "https://localhost:7267/admin/filters/price-range";
        const response = await axios.get<Item<number>[]>(url);
        setPopulatedData(
          response.data.filter((item): item is Item<number> => typeof item.id === "number")
        );
      } catch (error) {
        console.error("Error fetching price options:", error);
      }
    };
    fetchData();
  }, [show]);

  return <PopulateDropdown show={show} setShow={setShow} setSelectedId={setSelectedId} data={populatedData} label={"price-range"} selectedId={selectedId}></PopulateDropdown>
}
