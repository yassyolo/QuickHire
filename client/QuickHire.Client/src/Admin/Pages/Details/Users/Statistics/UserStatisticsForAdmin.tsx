import { useEffect, useState } from "react";

import "./UserStatisticsForAdmin.css";
import { PageSelector } from "../../../../../Shared/PageItems/PageSelector/PageSelector";
import { GigPerformanceStatistics } from "../../../../../Shared/Statistics/Users/GigPerformanceStatistics";
import { OrderFullfillmentStatistics } from "../../../../../Shared/Statistics/Users/OrderFullfillmentStatistics";
import { RepeatBusinessStatistics } from "../../../../../Shared/Statistics/Users/RepeatBusinessStatistics";
import { UserEngagementStatistics } from "../../../../../Shared/Statistics/Users/UserEngagementStatistics";

export function UserStatisticsForAdmin() {
    const [showEngagementStatistics, setShowEngagementStatistics] = useState(false);
            const [showRepeatBusinessStatistics, setShowRepeatBusinessStatistics] = useState(false);
            const [showGigPerformanceStatistics, setShowGigPerformanceStatistics] = useState(false);
            const [showOrderFulfillmentStatistics, setShowOrderFulfillmentStatistics] = useState(false);

            useEffect(() => {
                handleEngagementStatisticsVisibility();
            }, []);
    
            const handleEngagementStatisticsVisibility = () => {
                setShowEngagementStatistics(!showEngagementStatistics);
                setShowRepeatBusinessStatistics(false);
                setShowGigPerformanceStatistics(false);
                setShowOrderFulfillmentStatistics(false);
            };
            const handleRepeatBusinessStatisticsVisibility = () => {
                setShowRepeatBusinessStatistics(!showRepeatBusinessStatistics);
                setShowEngagementStatistics(false);
                setShowGigPerformanceStatistics(false);
                setShowOrderFulfillmentStatistics(false);
            };
            const handleGigPerformanceStatisticsVisibility = () => {
                setShowGigPerformanceStatistics(!showGigPerformanceStatistics);
                setShowEngagementStatistics(false);
                setShowRepeatBusinessStatistics(false);
                setShowOrderFulfillmentStatistics(false);
            };
            const handleOrderFulfillmentStatisticsVisibility = () => {
                setShowOrderFulfillmentStatistics(!showOrderFulfillmentStatistics);
                setShowEngagementStatistics(false);
                setShowRepeatBusinessStatistics(false);
                setShowGigPerformanceStatistics(false);
            };

            return(<div className="user-for-admin-statistics">          <PageSelector data={[
                                                { name: "Engagement", onClick: handleEngagementStatisticsVisibility, isActive: showEngagementStatistics },
                                                { name: "Repeat Business", onClick: handleRepeatBusinessStatisticsVisibility, isActive: showRepeatBusinessStatistics },
                                                { name: "Gig Performance", onClick: handleGigPerformanceStatisticsVisibility, isActive: showGigPerformanceStatistics },
                                                { name: "Order Fulfillment", onClick: handleOrderFulfillmentStatisticsVisibility, isActive: showOrderFulfillmentStatistics }
                                            ]} />
                        
                                            {showGigPerformanceStatistics && <GigPerformanceStatistics />}
                                            {showRepeatBusinessStatistics && <RepeatBusinessStatistics />}
                                            {showOrderFulfillmentStatistics && <OrderFullfillmentStatistics />} 
                                            {showEngagementStatistics && <UserEngagementStatistics />} 
                                            </div>)
}