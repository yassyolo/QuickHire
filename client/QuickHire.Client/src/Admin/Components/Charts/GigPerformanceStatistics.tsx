import { useState } from "react";
import { parseISO, isAfter, subDays, subMonths } from "date-fns";
import { SortBy } from "../Dropdowns/Common/SortBy";
import { MultiLineChartComponent } from "./Common/MultiLineDataChart";

export function MixedStatistics() {
    const [selectedRange, setSelectedRange] = useState("Last 30 days");

    const mock = [
        { date: "2025-04-20", completionRate: 2, impressions: 200, clicks: 100, conversionRate: 0.75 },
    ];

    const filteredData = mock.filter((entry) => {
        const now = new Date();
        const date = parseISO(entry.date);
        if (selectedRange === "Last 30 days") return isAfter(date, subDays(now, 30));
        if (selectedRange === "Last 3 months") return isAfter(date, subMonths(now, 3));
        if (selectedRange === "Yearly") return isAfter(date, subMonths(now, 12));
        return true;
    });

    const lines = [
        { key: "returningBuyers", label: "Returning Buyers", color: "#8884d8" },
        { key: "averageRepeatOrders", label: "Average Repeat Orders per Buyer", color: "#82ca9d" },
        { key: "revenue", label: "Revenue from Returning Buyers", color: "#ffc658" },
    ];

    return (
        <div className="statistics-section">
            <div className="d-flex justify-between items-center mb-4">
                <SortBy type="Date" setSelectedName={(name: string) => { if (typeof name === "string") setSelectedRange(name);}}/>
            </div>
            <MultiLineChartComponent data={filteredData} lines={lines} />
        </div>
    );
}


