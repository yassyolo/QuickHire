import { Label } from "../LineChart/Common/Label";
import { PieChartComponent } from "./PieChartComponent";
import './StatisticsPieChart.css';

interface StatisticsPieChartProps {
    label: string;
    data: {label: string; value: string; percentage: string}[];
}

export function StatisticsPieChart({ label, data }: StatisticsPieChartProps) {
    return (
        <div className="statistics-pie-chart" aria-label="statistics-pie-chart">
            <Label label={label}></Label>
            <PieChartComponent data={data}></PieChartComponent>
        </div>
    );
}