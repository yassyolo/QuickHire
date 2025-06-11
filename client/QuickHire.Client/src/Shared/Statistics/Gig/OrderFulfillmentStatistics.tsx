import { useEffect, useState } from "react";
import axios from "../../../axiosInstance";
import { StatisticsLineChart } from "../Common/LineChart/StatisticsLineChart";
import { StatisticsData, StatisticsTable } from "../Common/LineChart/Common/StatisticsTables";
import { StatisticsPieChart } from "../Common/PieChart/StatisticsPieChart";

export interface PieChartData {
    data: {label: string; value: string; percentage: string}[];
}
export interface OrderFullfillmentStatisticsProps {
    id: number;
}

export function OrderFullfillmentStatistics({id} : OrderFullfillmentStatisticsProps) {
    const [averageDeliveryTimeData, setaverageDeliveryTimeData] = useState<StatisticsData | null>(null) ;   
    const [ordersData, setordersDataa] = useState<StatisticsData | null>(null) ;
    const [orderStatusData, setorderStatusData] = useState<PieChartData>({ data: [] }) ;
    const [revisionData, setRevisionData] = useState<PieChartData>({ data: [] }) ;

    useEffect(() => {
        fetchOrdersData();
        fetchAverageDeliveryTimeData();
        fetchOrderStatusData();
        fetchRevisionData();
    }, [id]);

    const fetchAverageDeliveryTimeData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/average-delivery-time-statistics/${id}`);
        setaverageDeliveryTimeData(response.data);
    } catch (error) {
        console.error("Error fetching views data:", error);
    }
};

const fetchOrdersData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/order-statistics/${id}`);
        setordersDataa(response.data);
    } catch (error) {
        console.error("Error fetching likes data:", error);
    }
};

const fetchOrderStatusData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/order-status-statistics/${id}`);
        setorderStatusData(response.data);
    } catch (error) {
        console.error("Error fetching rating distribution data:", error);
    }
}

const fetchRevisionData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/revision-statistics/${id}`);
        setRevisionData(response.data);
    } catch (error) {
        console.error("Error fetching review response data:", error);
    }
}

    return(
    <StatisticsTable>
        <StatisticsLineChart icon={<i className="bi bi-star-half"></i>} label={"Average delivery tim"} thisMonthCount={averageDeliveryTimeData?.thisMonthItem.count || ""} thisMonthPercentage={averageDeliveryTimeData?.thisMonthItem.percentage || ""} peakDate={averageDeliveryTimeData?.peakItem.date || ""} data={averageDeliveryTimeData?.data || []}></StatisticsLineChart>
        <StatisticsLineChart icon={<i className="bi bi-chat-quote"></i>} label={"Orders"} totalItemsCount={ordersData?.totalItem.count || ""} totalItemslabel={ordersData?.totalItem.label || ""} thisMonthCount={ordersData?.thisMonthItem.count || ""} thisMonthPercentage={ordersData?.thisMonthItem.percentage || ""} peakDate={ordersData?.peakItem.date || ""} data={ordersData?.data || []}></StatisticsLineChart>
        <StatisticsPieChart label={"Order status"} data={orderStatusData?.data || []}></StatisticsPieChart>
        <StatisticsPieChart label={"Revision"} data={revisionData?.data || []}></StatisticsPieChart>
    </StatisticsTable>
    );
}