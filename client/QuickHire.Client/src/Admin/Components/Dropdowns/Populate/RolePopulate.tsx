import { useEffect, useState } from "react";
import { Item, PopulateDropdown } from "../Common/PopulateDropdown";
import axios from "axios";

export interface RolePopulateProps {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: number | undefined) => void;
  selectedId: number | undefined;
}

export function RolePopulate({ show, setShow, setSelectedId, selectedId }: RolePopulateProps) {
  const [populatedData, setPopulatedData] = useState<Item[]>([]);

  useEffect(() => {
    if (!show) return;
  const fetchData = async () => {
    try {
      const url = "https://localhost:7267/admin/filters/role";
      const response = await axios.get<Item[]>(url);
      setPopulatedData(response.data);
    } catch (error) {
      console.error("Error fetching role options:", error);
    }
  };

  fetchData();
  }, [show]);

  return <PopulateDropdown show={show} setShow={setShow} setSelectedId={setSelectedId} data={populatedData} label={"role"} selectedId={selectedId}></PopulateDropdown>
}
