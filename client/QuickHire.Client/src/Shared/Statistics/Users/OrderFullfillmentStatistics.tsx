import { useEffect, useState } from "react";
import { StatisticCard, StatisticsSection } from "./Common/UserStatistics";
import axios from "../../../axiosInstance";

interface OrderFulfillmentData {
  date: string;
  newOrders: number;
  completedOrders: number;
  sales: number;
  averageRating: number;
  revenue: number;
}

export function OrderFullfillmentStatistics() {
  const [selectedRange, setSelectedRange] = useState("Last 30 days");
  const [statisticCards, setStatisticCards] = useState<StatisticCard[]>([]);
  const [statistics, setStatistics] = useState<OrderFulfillmentData[]>([]);

  const fetchStatisticCards = async () => {
    try {
        const url = `https://localhost:7267/seller/statistics/order-fulfillment/cards`;
        const response = await axios.get<StatisticCard[]>(url);
        setStatisticCards(response.data);
        }
    catch (error) {
        console.error("Error fetching order fulfillment statistics cards:", error);
       
    }
  };

    const fetchStatistics = async (range: string) => {
        try {
        const url = `https://localhost:7267/seller/statistics/order-fulfillment?range=${encodeURIComponent(range)}`;
        const response = await axios.get<OrderFulfillmentData[]>(url);
        setStatistics(response.data);
        } catch (error) {
        console.error("Error fetching order fulfillment statistics data:", error);
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
    { key: "newOrders", label: "New Orders", color: "#8884d8" },
    { key: "completedOrders", label: "Completed Orders", color: "#82ca9d" },
    { key: "sales", label: "Sales", color: "#ffc658" },
  ];

  const mappedStatistics = statistics.map((item) => ({
    date: item.date,
    newOrders: item.newOrders,
    completedOrders: item.completedOrders,
    sales: item.sales,
    averageRating: item.averageRating,
    revenue: item.revenue,
  }));

    return <StatisticsSection statisticCards={statisticCards} lines={lines}  data={mappedStatistics} onRangeChange={handleRangeChange}/>
  
}
