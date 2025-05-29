import { useEffect, useState } from "react";
import axios from "axios";
import { StatisticsLineChart } from "./Common/StatisticsLineChart";
import { StatisticsData, StatisticsTable } from "./Common/StatisticsTables";
import { StatisticsPieChart } from "./Common/StatisticsPieChart";

export interface PieChartData {
    data: {label: string; value: string; percentage: string}[];
}
export interface CustomerFeedbackStatisticsProps {
    id: number;
}

export function CustomerFeedbackStatistics({id} : CustomerFeedbackStatisticsProps) {
    const [viewsStars, setStarsData] = useState<StatisticsData | null>(null) ;   
    const [reviewsData, setReviewsDataa] = useState<StatisticsData | null>(null) ;
    const [ratingDistributionData, setRatingDistributionData] = useState<PieChartData>({ data: [] }) ;
    const [reviewResponseData, setReviewResponseData] = useState<PieChartData>({ data: [] }) ;

    useEffect(() => {
        console.log(viewsStars);
        console.log(reviewsData);
        console.log(ratingDistributionData);
        console.log(reviewResponseData);
    }, [viewsStars, reviewsData, ratingDistributionData, reviewResponseData]);

    useEffect(() => {
        fetchReviewsData();
        fetchStarsData();
        fetchRatingDistributionData();
        fetchReviewResponseData();
    }, [id]);

    const fetchStarsData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/stars-statistics/${id}`);
        setStarsData(response.data);
    } catch (error) {
        console.error("Error fetching views data:", error);
    }
};

const fetchReviewsData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/reviews-statistics/${id}`);
        setReviewsDataa(response.data);
    } catch (error) {
        console.error("Error fetching likes data:", error);
    }
};

const fetchRatingDistributionData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/rating-distribution-statistics/${id}`);
        setRatingDistributionData(response.data);
    } catch (error) {
        console.error("Error fetching rating distribution data:", error);
    }
}

const fetchReviewResponseData = async () => {
    try {
        const response = await axios.get(`https://localhost:7267/gigs/statistics/review-response-statistics/${id}`);
        setReviewResponseData(response.data);
    } catch (error) {
        console.error("Error fetching review response data:", error);
    }
}

    return(
    <StatisticsTable>
        <StatisticsLineChart icon={<i className="bi bi-star-half"></i>} label={"Stars"} thisMonthCount={viewsStars?.thisMonthItem.count || ""} thisMonthPercentage={viewsStars?.thisMonthItem.percentage || ""} peakDate={viewsStars?.peakItem.date || ""} data={viewsStars?.data || []}></StatisticsLineChart>
        <StatisticsLineChart icon={<i className="bi bi-chat-quote"></i>} label={"Reviews"} totalItemsCount={reviewsData?.totalItem.count || ""} totalItemslabel={reviewsData?.totalItem.label || ""} thisMonthCount={reviewsData?.thisMonthItem.count || ""} thisMonthPercentage={reviewsData?.thisMonthItem.percentage || ""} peakDate={reviewsData?.peakItem.date || ""} data={reviewsData?.data || []}></StatisticsLineChart>
        <StatisticsPieChart /*icon={<i className="bi bi-chat-square-heart"></i>} */ label={"Rating Distribution"} data={ratingDistributionData?.data || []}></StatisticsPieChart>
        <StatisticsPieChart label={"Review Response Rate"} data={reviewResponseData?.data || []}></StatisticsPieChart>
    </StatisticsTable>
    );
}