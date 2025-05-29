import { useEffect, useState } from "react";
import axios from "axios";
import { StatisticsLineChart } from "./Common/StatisticsLineChart";
import { StatisticsData, StatisticsTable } from "./Common/StatisticsTables";

export interface EngagementStatisticsProps {
    id: number;
}

export function EngagementStatistics({id} : EngagementStatisticsProps) {
    const [viewsData, setViewsData] = useState<StatisticsData | null>(null) ;   
    const [likesData, setLikesData] = useState<StatisticsData | null>(null) ;

    useEffect(() => {
        fetchLikesData();
        fetchViewsData();
    }, [id]);

    const fetchViewsData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/views-statistics/${id}`);
        setViewsData(response.data);
    } catch (error) {
        console.error("Error fetching views data:", error);
    }
};

const fetchLikesData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/likes-statistics/${id}`);
        setLikesData(response.data);
    } catch (error) {
        console.error("Error fetching likes data:", error);
    }
};
    
    return(
    <StatisticsTable>
        <StatisticsLineChart icon={<i className="bi bi-eyeglasses"></i>}label={"Views"} totalItemsCount={viewsData?.totalItem.count || ""} totalItemslabel={viewsData?.totalItem.label || ""} thisMonthCount={viewsData?.thisMonthItem.count || ""} thisMonthPercentage={viewsData?.thisMonthItem.percentage || ""} peakDate={viewsData?.peakItem.date || ""} data={viewsData?.data || []}></StatisticsLineChart>
        <StatisticsLineChart icon={<i className="bi bi-heart"></i>} label={"Likes"} totalItemsCount={likesData?.totalItem.count || ""} totalItemslabel={likesData?.totalItem.label || ""} thisMonthCount={likesData?.thisMonthItem.count || ""} thisMonthPercentage={likesData?.thisMonthItem.percentage || ""} peakDate={likesData?.peakItem.date || ""} data={likesData?.data || []}></StatisticsLineChart>
    </StatisticsTable>
    );
}