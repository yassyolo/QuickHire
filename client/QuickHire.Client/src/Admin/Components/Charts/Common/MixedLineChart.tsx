import { useState } from "react";
import { SortBy } from "../../Dropdowns/Common/SortBy";
import { MultiLineChartComponent } from "./MultiLineDataChart";
import { parseISO, isAfter, subDays, subMonths } from "date-fns";

export function MixedStatistics() {
    const [selectedRange, setSelectedRange] = useState("Last 30 days");

    const mock = [
        { date: "2025-04-20", newOrders: 33, completedOrders: 1, sales: 120, averageRating: 120, revenue: 150 },
        { date: "2025-04-25", newOrders: 21, completedOrders: 2, sales: 150, averageRating: 150, revenue: 190 },
        { date: "2025-05-01", newOrders: 10, completedOrders: 3, sales: 190, averageRating: 190, revenue: 300 },
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
        { key: "newOrders", label: "New Orders", color: "#8884d8" },
        { key: "completedOrders", label: "Completed Orders", color: "#82ca9d" },
        { key: "sales", label: "Sales", color: "#ffc658" },
    ];

    return (
        <div className="statistics-section">
            <div className="d-flex justify-between items-center mb-4">
                <h3 className="text-xl font-bold">Sales & Orders</h3>
                <SortBy
                    type="Date"
                    setSelectedName={(name: string) => { if (typeof name === "string") setSelectedRange(name);}}
                />
            </div>
            <MultiLineChartComponent data={filteredData} lines={lines} />
        </div>
    );
}


