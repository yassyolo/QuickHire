import { useEffect, useState } from "react";
import { StatisticCard, StatisticsSection } from "./Common/UserStatistics";
import axios from "../../../axiosInstance";


interface GigPerformanceStatistics {
  date: string;
  gigsSold: number;
  gigLikes: number;
  gigFavourites: number;
  repeatViews: number;
  commentsCount: number;
}

export function GigPerformanceStatistics() {
  const [statisticCards, setStatisticCards] = useState<StatisticCard[]>([]);
  const [selectedRange, setSelectedRange] = useState("Last 30 days");
  const [statistics, setStatistics] = useState<GigPerformanceStatistics[]>([]);

   const fetchStatisticCards = async () => {
        try {
        const url = `https://localhost:7267/seller/statistics/gig-performance/cards`;
        const response = await axios.get<StatisticCard[]>(url);
        setStatisticCards(response.data);
        } catch (error) {
        console.error("Error fetching gig performance statistics cards:", error);
        setStatisticCards([
            { title: "Gigs Sold", value: "0" },
            { title: "Gig Likes", value: "0" },
            { title: "Gig Favourites", value: "0" },
            { title: "Repeat Views", value: "0" },
            { title: "Reviews Count", value: "0" },
        ]);
        }
    };

    const fetchStatistics = async () => {
        try {
            const url = `https://localhost:7267/seller/statistics/gig-performance?range=${encodeURIComponent(selectedRange)}`;
            const response = await axios.get<GigPerformanceStatistics[]>(url);
            setStatistics(response.data);
        } catch (error) {
            console.error("Error fetching gig performance statistics data:", error);
            setStatistics([]);
        }
    }

  useEffect(() => {
    fetchStatisticCards();
    fetchStatistics();
  }, [selectedRange]);

      const handleRangeChange = (range: string) => {
        setSelectedRange(range);
    };

  const lines = [
    { key: "gigsSold", label: "Gigs Sold", color: "#8884d8" },
    { key: "gigLikes", label: "Gig Likes", color: "#82ca9d" },
    { key: "gigFavourites", label: "Gig Favourites", color: "#ffc658" },
    { key: "repeatViews", label: "Repeat Views", color: "#ff8042" },
    { key: "commentsCount", label: "Comments Count", color: "#8dd1e1" },
  ];

  const mappedStatistics = statistics.map((stat) => ({
    date: stat.date,
    gigsSold: stat.gigsSold,
    gigLikes: stat.gigLikes,
    gigFavourites: stat.gigFavourites,
    repeatViews: stat.repeatViews,
    commentsCount: stat.commentsCount,
  }));

    return <StatisticsSection statisticCards={statisticCards} lines={lines}  data={mappedStatistics} onRangeChange={handleRangeChange}/>
  
}
