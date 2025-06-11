import { useEffect, useState } from "react";
import axios from "../../../axiosInstance";
import { StatisticCard, StatisticsSection } from "./Common/UserStatistics";
interface EngagementStatisticsData {
    date: string;
    profileViews: number;
    gigClicks: number;
    messagesStarted: number;
    gigSaves: number;
    briefsReceived: number;
}

export function UserEngagementStatistics() {
    const [statisticCards, setStatisticCards] = useState<StatisticCard[]>([]);
    const [selectedRange, setSelectedRange] = useState("Last 30 days");
    const [statistics, setStatistics] = useState<EngagementStatisticsData[]>([]);

    const fetchStatisticCards = async () => {
        try {
            const url = `https://localhost:7267/seller/statistics/engagement/cards`;
            const response = await axios.get<StatisticCard[]>(url);
            setStatisticCards(response.data);
        } catch (error) {
            console.error("Error fetching engagement statistics:", error);
            setStatisticCards([
                { title: "Profile Views", value: "0"},
                { title: "Gig Clicks", value: "0" },
                { title: "Message Initiations", value: "0" },
                { title: "Gig Saves", value: "0" },
                { title: "Project Briefs", value: "0" },
            ]);
        }
    };

   const fetchStatistics = async (range: string) => {
        try {
            const url = `https://localhost:7267/seller/statistics/engagement?range=${encodeURIComponent(range)}`;
            const response = await axios.get<EngagementStatisticsData[]>(url);
            setStatistics(response.data);
        } catch (error) {
            console.error("Error fetching engagement statistics data:", error);
            setStatistics([]);
        }
    };
   
    useEffect(() => {
        fetchStatisticCards();
        fetchStatistics(selectedRange);
    }, [selectedRange]);

    const handleRangeChange = (range: string) => {
        setSelectedRange(range);
    };

    const lines = [
        { key: "profileViews", label: "Profile Views", color: "#8884d8" },
        { key: "gigClicks", label: "Gig Clicks", color: "#82ca9d" },
        { key: "messagesStarted", label: "Message Initiations", color: "#ffc658" },
        { key: "gigSaves", label: "Gig Saves", color: "#ff7f50" },
        { key: "briefsReceived", label: "Project Briefs", color: "#00bfff" },
    ];

    const mappedStatistics = statistics.map(stat => ({
        date: stat.date,
        profileViews: stat.profileViews,
        gigClicks: stat.gigClicks,
        messagesStarted: stat.messagesStarted,
        gigSaves: stat.gigSaves,
        briefsReceived: stat.briefsReceived,
    }));

    return <StatisticsSection statisticCards={statisticCards} lines={lines}  data={mappedStatistics} onRangeChange={handleRangeChange}/>

}
