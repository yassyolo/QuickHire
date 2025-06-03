import { useEffect, useState } from "react";
import { StatisticCard, StatisticsSection } from "./Common/UserStatistics";
import axios from "axios";


interface EarningsStatisticsData {
  date: string;
  totalRevenue: number;
  completedRevenue: number;
  averageOrderValue: number;
  inProgressRevenue: number;
}

export function EarningsStatistics() {
  const [statisticCards, setStatisticCards] = useState<StatisticCard[]>([]);
  const [selectedRange, setSelectedRange] = useState("Last 30 days");
  const [statistics, setStatistics] = useState<EarningsStatisticsData[]>([]);

 

  
  const fetchStatisticCards = async () => {
    try {
      const url = `https://localhost:7267/seller/statistics/earnings/cards`;
      const response = await axios.get<StatisticCard[]>(url);
      setStatisticCards(response.data);
    } catch (error) {
      console.error("Error fetching earnings statistic cards:", error);
    }
  };

  const fetchStatistics = async () => {
    try {
      const url = `https://localhost:7267/seller/statistics/earnings?range=${encodeURIComponent(selectedRange)}`;
      const response = await axios.get<EarningsStatisticsData[]>(url);
      setStatistics(response.data);
    } catch (error) {
      console.error("Error fetching earnings statistics:", error);
      setStatistics([]);
    }
  };

  useEffect(() => {
    fetchStatisticCards();
    fetchStatistics();
  }, [selectedRange]);

      const handleRangeChange = (range: string) => {
        setSelectedRange(range);
    };

  const lines = [
    { key: "totalRevenue", label: "Total Revenue", color: "#8884d8" },
    { key: "completedRevenue", label: "Completed Orders Revenue", color: "#82ca9d" },
    { key: "averageOrderValue", label: "Average Order Value", color: "#ffc658" },
    { key: "inProgressRevenue", label: "In Progress Revenue", color: "#ff8042" },
  ];

  const mappedStatistics = statistics.map((stat) => ({
    date: stat.date,
    totalRevenue: stat.totalRevenue,
    completedRevenue: stat.completedRevenue,
    averageOrderValue: stat.averageOrderValue,
    inProgressRevenue: stat.inProgressRevenue,
  }));

    return <StatisticsSection statisticCards={statisticCards} lines={lines}  data={mappedStatistics} onRangeChange={handleRangeChange}/>
  
}
