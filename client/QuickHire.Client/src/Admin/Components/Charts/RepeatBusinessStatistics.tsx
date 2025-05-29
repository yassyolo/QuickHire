import { useState } from "react";
import { parseISO, isAfter, subDays, subMonths } from "date-fns";
import { SortBy } from "../Dropdowns/Common/SortBy";
import { MultiLineChartComponent } from "./Common/MultiLineDataChart";

export function MixedStatistics() {
    const [selectedRange, setSelectedRange] = useState("Last 30 days");

    const mock = [
        { date: "2025-04-20", returningBuyers: 33, averageRepeatOrders: 2, revenue: 120 },
        { date: "2025-04-25", returningBuyers: 21, averageRepeatOrders: 3, revenue: 150 },
        { date: "2025-05-01", returningBuyers: 10, averageRepeatOrders: 1, revenue: 190 },
        { date: "2025-05-15", returningBuyers: 5, averageRepeatOrders: 4, revenue: 300 },
        { date: "2025-06-01", returningBuyers: 15, averageRepeatOrders: 2, revenue: 250 },
        { date: "2025-06-15", returningBuyers: 20, averageRepeatOrders: 3, revenue: 400 },
        { date: "2025-07-01", returningBuyers: 25, averageRepeatOrders: 5, revenue: 500 },
        { date: "2025-07-15", returningBuyers: 30, averageRepeatOrders: 6, revenue: 600 },
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


