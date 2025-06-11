import { useEffect, useState } from "react";
import { Item, PopulateDropdown } from "../../Common/PopulateDropdown";
import axios from "../../../../axiosInstance";

export interface RolePopulateProps {
  show: boolean;
  setShow: (show: boolean) => void;
  setSelectedId: (id: string | undefined) => void;
  selectedId: string | undefined;
}

export function RolePopulate({ show, setShow, setSelectedId, selectedId }: RolePopulateProps) {
  const [populatedData, setPopulatedData] = useState<Item<string>[]>([]);

  useEffect(() => {
    if (!show) return;
  const fetchData = async () => {
    try {
      const url = "https://localhost:7267/admin/filters/role";
      const response = await axios.get<Item<string>[]>(url);
setPopulatedData(response.data);
    } catch (error) {
      console.error("Error fetching role options:", error);
    }
  };

  fetchData();
  }, [show]);

  return <PopulateDropdown show={show} setShow={setShow} setSelectedId={setSelectedId} data={populatedData} label={"role"} selectedId={selectedId}></PopulateDropdown>
}
