import { Peak } from "./Common/Peak";
import { TotalItems } from "./Common/TotalItems";
import { LineChartComponent } from "./LineChart";
import './StatisticsLineChart.css';
import { Label } from "./Common/Label";
import { ThisMonth } from "./Common/ThisMonths";

export interface StatisticsLineChartDataProps {
    label: string;
    totalItemsCount?: string;
    totalItemslabel?: string;
    thisMonthCount: string;
    thisMonthPercentage: string;
    peakDate: string;
    data: { month: string; value: string }[];
    icon: React.ReactNode;
}

export function StatisticsLineChart({icon, totalItemsCount, totalItemslabel, thisMonthCount, thisMonthPercentage, peakDate, data, label}: StatisticsLineChartDataProps) {
  return (
    <div className="statistics-data" aria-label="statistics-data">
        <Label label={label}></Label>
        <div className="d-flex flex-row w-100 justify-content-between" >
          <TotalItems label={totalItemslabel || "N/A"} count={totalItemsCount || "0"} icon={icon}></TotalItems>
          <ThisMonth count={thisMonthCount} percentage={thisMonthPercentage} icon={<i className="bi bi-calendar-check-fill"></i>}></ThisMonth>
        </div>
        <Peak date={peakDate}></Peak>
        <LineChartComponent data={data}></LineChartComponent>
    </div>
    );
}