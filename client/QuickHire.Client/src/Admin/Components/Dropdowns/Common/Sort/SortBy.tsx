
import { useEffect, useState } from "react";
import { Item } from "../PopulateDropdown";
import "./SortBy.css";

export interface SortByProps {
  setSelectedName: (name: string) => void;
  type: string;
}

export function SortBy({setSelectedName, type }: SortByProps) {
  const [populatedData, setPopulatedData] = useState<Item[]>([]);
  const [currentName, setCurrentName] = useState<string>("");
  const [showSortingsModal, setShowSortingsModal] = useState<boolean>(false);

  useEffect(() => {
    if( type === "Reviews"){
    setCurrentName("Most recent");
    const data: Item[] = [
    { name: "Most recent" },
    { name: "Price"},
    { name: "Duration" }];
    setPopulatedData(data);}
    else if( type === "Date"){
      setCurrentName("Last 30 days");
      const data: Item[] = [
        { name: "Last 30 days" },
        { name: "Last 3 months" },
        { name: "Yearly" }];
      setPopulatedData(data);
    }
    else if(type === "Orders"){
      setCurrentName("Active");
      const data: Item[] = [
        { name: "Active" },
        { name: "Completed"}]
        setPopulatedData(data);
    }
  }, [type]);

  const handleSortingsModalShowcase = () => setShowSortingsModal(!showSortingsModal);
  const handleSortingChoice = (name: string) => {
      setCurrentName(name);
      setSelectedName(name);
      setShowSortingsModal(false);
  }

  return (
    <div className="sort-by-component">
      <label htmlFor="sort-by" className={"sort-by-label"}>Sort By</label>
      <button className="current-sorting" onClick={handleSortingsModalShowcase}>{currentName} {showSortingsModal !== true ? <i className="bi bi-chevron-down"></i> : <i className="bi bi-chevron-up"></i>}</button>
      
      {showSortingsModal && (
        <div className="sortings-modal">
          {populatedData.map((item, index) => (
            <div key={index} className="sort-by-item" onClick={() => handleSortingChoice(item.name)}> {item.name}</div>))}
        </div>
      )}
    </div>
  );
}
