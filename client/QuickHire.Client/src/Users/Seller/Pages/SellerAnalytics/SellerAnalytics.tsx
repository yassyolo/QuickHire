import { useEffect, useState } from "react";
import { SellerPage } from "../Common/SellerPage";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { PageSelector } from "../../../../Shared/PageItems/PageSelector/PageSelector";
import { GigPerformanceStatistics } from "../../../../Shared/Statistics/Users/GigPerformanceStatistics";
import { RepeatBusinessStatistics } from "../../../../Shared/Statistics/Users/RepeatBusinessStatistics";
import { OrderFullfillmentStatistics } from "../../../../Shared/Statistics/Users/OrderFullfillmentStatistics";
import { UserEngagementStatistics } from "../../../../Shared/Statistics/Users/UserEngagementStatistics";

export function SellerAnalytics() {
    const [showEngagementStatistics, setShowEngagementStatistics] = useState(false);
    const [showRepeatBusinessStatistics, setShowRepeatBusinessStatistics] = useState(false);
    const [showGigPerformanceStatistics, setShowGigPerformanceStatistics] = useState(false);
    const [showOrderFulfillmentStatistics, setShowOrderFulfillmentStatistics] = useState(false);
    const [breadcrumbs, setBreadcrumbs] = useState<{ label: React.ReactNode; to?: string }[]>([]);

     useEffect(() => {
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/seller/dashboard` },
                { label: "Analytics" }
            ]);
    
            handleEngagementStatisticsVisibility();
        }, []);
    const handleEngagementStatisticsVisibility = () => {
            setShowEngagementStatistics(!showEngagementStatistics);
            setShowRepeatBusinessStatistics(false);
            setShowGigPerformanceStatistics(false);
            setShowOrderFulfillmentStatistics(false);
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/seller/dashboard` },
                { label: "Analytics", to: `/seller/analytics` },
                { label: "Engagement" }
            ]);
        }
    const handleRepeatBusinessStatisticsVisibility = () => {
            setShowRepeatBusinessStatistics(!showRepeatBusinessStatistics);
            setShowEngagementStatistics(false);
            setShowGigPerformanceStatistics(false);
            setShowOrderFulfillmentStatistics(false);
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/seller/dashboard` },
                { label: "Analytics", to: `/seller/analytics` },
                { label: "Repeat Business" }
            ]);
        }

    const handleGigPerformanceStatisticsVisibility = () => {
            setShowGigPerformanceStatistics(!showGigPerformanceStatistics);
            setShowEngagementStatistics(false);
            setShowRepeatBusinessStatistics(false);
            setShowOrderFulfillmentStatistics(false);
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/seller/dashboard` },
                { label: "Analytics", to: `/seller/analytics` },
                { label: "Gig Performance" }
            ]);
        }

    const handleOrderFulfillmentStatisticsVisibility = () => {
            setShowOrderFulfillmentStatistics(!showOrderFulfillmentStatistics);
            setShowEngagementStatistics(false);
            setShowRepeatBusinessStatistics(false);
            setShowGigPerformanceStatistics(false);
            setBreadcrumbs([
                { label: <i className="bi bi-house-door"></i>, to: `/seller/dashboard` },
                { label: "Analytics", to: `/seller/analytics` },
                { label: "Order Fulfillment" }
            ]);
        }
    return(      
        <SellerPage>
            <PageTitle title="Analytics"  description="Analyze your sales performance, track engagement, and monitor key metrics to grow your business." breadcrumbs={breadcrumbs}/>
            <PageSelector data={[
                        { name: "Engagement", onClick: handleEngagementStatisticsVisibility, isActive: showEngagementStatistics },
                        { name: "Repeat Business", onClick: handleRepeatBusinessStatisticsVisibility, isActive: showRepeatBusinessStatistics },
                        { name: "Gig Performance", onClick: handleGigPerformanceStatisticsVisibility, isActive: showGigPerformanceStatistics },
                        { name: "Order Fulfillment", onClick: handleOrderFulfillmentStatisticsVisibility, isActive: showOrderFulfillmentStatistics }
                    ]} />

                    {showGigPerformanceStatistics && <GigPerformanceStatistics />}
                    {showRepeatBusinessStatistics && <RepeatBusinessStatistics />}
                    {showOrderFulfillmentStatistics && <OrderFullfillmentStatistics />} 
                    {showEngagementStatistics && <UserEngagementStatistics />}
           
        </SellerPage>    
    );
}
