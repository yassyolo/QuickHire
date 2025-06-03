import { SortBy } from "../../../Dropdowns/Common/Sort/SortBy";
import { MultiLineChartComponent } from "./MultiLineDataChart";
import './SortingChart.css'

interface SortingChartProps {
    lines: { key: string; label: string; color: string }[];
    data: { date: string; [key: string]: number | string }[];
    onRangeChange: (range: string) => void;  
}
export function SortingChart({ lines, data, onRangeChange }: SortingChartProps) {
    return (
        <div className="sorting-chart">
            <div className="sorting-wrapper">
               <SortBy type="Date" setSelectedName={(name: string) => {if (typeof name === "string") onRangeChange(name);}} />
            </div>
            <div className="chart-wrapper">
                <MultiLineChartComponent data={data} lines={lines} />
            </div>
        </div>
    );
}