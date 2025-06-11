import { useEffect, useState } from "react";
import axios from "../../../axiosInstance";
import { StatisticsLineChart } from "../Common/LineChart/StatisticsLineChart";
import { StatisticsData, StatisticsTable } from "../Common/LineChart/Common/StatisticsTables";
import { StatisticsPieChart } from "../Common/PieChart/StatisticsPieChart";

export interface PieChartData {
    data: {label: string; value: string; percentage: string}[];
}
export interface SalesAndRevenueStatisticsProps {
    id: number;
}

export function SalesAndRevenueStatistics({id} : SalesAndRevenueStatisticsProps) {
    const [revenueData, setRevenueData] = useState<StatisticsData | null>(null) ;   
    const [averageOrder, setAverageOrder] = useState<StatisticsData | null>(null) ;
    const [salesVolumeData, setSalesVolumeData] = useState<StatisticsData | null>(null) ;

    const [paymentPlanChoiceData, setpaymentPlanChoiceData] = useState<PieChartData>({ data: [] }) ;

    useEffect(() => {
        fetchAverageOrderData();
        fetchRevenueData();
        fetchSalesVolumeData();
        fetchPaymentPlanChoiceData();
    }, [id]);

    const fetchRevenueData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/revenue-statistics/${id}`);
        setRevenueData(response.data);
    } catch (error) {
        console.error("Error fetching views data:", error);
    }
};

const fetchAverageOrderData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/average-order-value-statistics/${id}`);
        setAverageOrder(response.data);
    } catch (error) {
        console.error("Error fetching likes data:", error);
    }
};

const fetchSalesVolumeData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/sales-volume-statistics/${id}`);
        setSalesVolumeData(response.data);
    } catch (error) {
        console.error("Error fetching rating distribution data:", error);
    }
}

const fetchPaymentPlanChoiceData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/payment-plan-choice-statistics/${id}`);
        setpaymentPlanChoiceData(response.data);
    } catch (error) {
        console.error("Error fetching review response data:", error);
    }
}

    return(
    <StatisticsTable>
        <StatisticsLineChart icon={<i className="bi bi-star-half"></i>} label={"Revenue"}   totalItemsCount={revenueData?.totalItem.count || ""}      thisMonthCount={revenueData?.thisMonthItem.count || ""} thisMonthPercentage={revenueData?.thisMonthItem.percentage || ""} peakDate={revenueData?.peakItem.date || ""} data={revenueData?.data || []}></StatisticsLineChart>
        <StatisticsLineChart icon={<i className="bi bi-chat-quote"></i>} label={"Average order value"} totalItemsCount={averageOrder?.totalItem.count || ""} totalItemslabel={averageOrder?.totalItem.label || ""} thisMonthCount={averageOrder?.thisMonthItem.count || ""} thisMonthPercentage={averageOrder?.thisMonthItem.percentage || ""} peakDate={averageOrder?.peakItem.date || ""} data={averageOrder?.data || []}></StatisticsLineChart>
        <StatisticsLineChart icon={<i className="bi bi-chat-quote"></i>} label={"Sales volume"} totalItemsCount={salesVolumeData?.totalItem.count || ""} totalItemslabel={salesVolumeData?.totalItem.label || ""} thisMonthCount={salesVolumeData?.thisMonthItem.count || ""} thisMonthPercentage={salesVolumeData?.thisMonthItem.percentage || ""} peakDate={salesVolumeData?.peakItem.date || ""} data={salesVolumeData?.data || []}></StatisticsLineChart>

        <StatisticsPieChart label={"Payment plans choice"} data={paymentPlanChoiceData?.data || []}></StatisticsPieChart>
    </StatisticsTable>
    );
}