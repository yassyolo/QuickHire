import { useEffect, useState } from "react";
import axios from "axios";
import { StatisticCard, StatisticsSection } from "./Common/UserStatistics";

interface RepeatBusinessStatistics {
    date: string;
    returningBuyers: number;
    averageRepeatOrders: number;
    revenue: number;
}

export function RepeatBusinessStatistics() {
    const [statisticCards, setStatisticCards] = useState<StatisticCard[]>([]);
    const [selectedRange, setSelectedRange] = useState("Last 30 days");
    const [statistics, setStatistics] = useState<RepeatBusinessStatistics[]>([]);

    const fetchStatissticCards = async () => {
        try {
            const url = `https://localhost:7267/seller/statistics/repeat-business/cards`;
            const response = await axios.get<StatisticCard[]>(url);
            setStatisticCards(response.data);
        } catch (error) {
            console.error("Error fetching repeat business statistics:", error);
            setStatisticCards([
                { title: "Returning Buyers", value: "0"},
                { title: "Avg Repeat Orders", value: "0" },
                { title: "Revenue from Returning Buyers", value: "0" },
            ]);
        }
    };

    const fetchStatistics = async (range: string) => {
    try {
        const url = `https://localhost:7267/seller/statistics/repeat-business?range=${encodeURIComponent(range)}`;
        const response = await axios.get<RepeatBusinessStatistics[]>(url);
        setStatistics(response.data);
    } catch (error) {
        console.error("Error fetching repeat business statistics data:", error);
        setStatistics([]);
    }
};

    useEffect(() => {
        fetchStatissticCards();
        fetchStatistics(selectedRange);
    }
    , [selectedRange]);

    const handleRangeChange = (range: string) => {
        setSelectedRange(range);
    };


    const lines = [
        { key: "returningBuyers", label: "Returning buyers", color: "#8884d8" },
        { key: "averageRepeatOrders", label: "Avg repeat orders per buyer", color: "#82ca9d" },
        { key: "revenue", label: "Revenue from repeat customers", color: "#ffc658" },
    ];

    const mappedStatistics = statistics.map(stat => ({
        date: stat.date,
        returningBuyers: stat.returningBuyers,
        averageRepeatOrders: stat.averageRepeatOrders,
        revenue: stat.revenue,
    }));

        return <StatisticsSection statisticCards={statisticCards} lines={lines}  data={mappedStatistics} onRangeChange={handleRangeChange}/>    
}


