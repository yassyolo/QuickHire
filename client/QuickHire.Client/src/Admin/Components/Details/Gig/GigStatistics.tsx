import { useEffect, useState } from "react";
import { EngagementStatistics } from "../../Charts/Gig/EngagementStatistics";
import { CustomerFeedbackStatistics } from "../../Charts/Gig/CustomerFeedbackStatistics";
import { Item } from "../../Dropdowns/Common/PopulateDropdown";
import { TitleFilterSelector } from "../../../Pages/Common/TitleFilterSection";
import { SalesAndRevenueStatistics } from "../../Charts/Gig/SalesAndRevenueStatistics";
import { OrderFullfillmentStatistics } from "../../Charts/Gig/OrderFulfillmentStatistics";

export interface GigStatisticsProps {
    id: number;
}

export function GigStatistics({id} : GigStatisticsProps) {
    const [showEngagementStatistics, setShowEngagementStatistics] = useState(false);
    const [showCustomerFeedbackStatistics, setShowCustomerFeedbackStatistics] = useState(false);

    const [selectedId, setSelectedId] = useState(Number);
    const [showSalesAndRevenueStatistics, setShowSalesAndRevenueStatistics] = useState(false);
    const [showOrderFulfillmentStatistics, setShowOrderFulfillmentStatistics] = useState(false);

    useEffect(() => {   
        setSelectedId(1);
    }, []);

          const data: Item[] = [
    { id: 1, name: "Engagement" },
    { id: 2, name: "Customer Feedback" },
    { id: 3, name: "Sales & Revenue" },
    { id: 4, name: "Order Fulfillment" }
          ];

    const handleSelectedStatisticsType = (id: number | undefined) => {
        setSelectedId(id || 0);
        if(id === 1) {
            setShowEngagementStatistics(true);
            setShowCustomerFeedbackStatistics(false);
            setShowSalesAndRevenueStatistics(false);
            setShowOrderFulfillmentStatistics(false);
        }
        else if(id === 2) {
            setShowCustomerFeedbackStatistics(true);
            setShowEngagementStatistics(false);
            setShowSalesAndRevenueStatistics(false);
            setShowOrderFulfillmentStatistics(false);
        }
        else if(id === 3) {
            setShowSalesAndRevenueStatistics(true);
            setShowEngagementStatistics(false);
            setShowCustomerFeedbackStatistics(false);
            setShowOrderFulfillmentStatistics(false);
        }
        else if(id === 4) {
            setShowOrderFulfillmentStatistics(true);
            setShowEngagementStatistics(false);
            setShowCustomerFeedbackStatistics(false);
            setShowSalesAndRevenueStatistics(false);
        }
    }
    return(      
        <div className="gigs-statistics-admin">
            <div className="gig-statistics filter">
              <TitleFilterSelector selectedId={selectedId} setSelectedId={handleSelectedStatisticsType} data={data}/>           
            </div>
            {showEngagementStatistics && <EngagementStatistics id={id} ></EngagementStatistics>}
            {showCustomerFeedbackStatistics && <CustomerFeedbackStatistics id={id} ></CustomerFeedbackStatistics>}
            {showSalesAndRevenueStatistics && <SalesAndRevenueStatistics id={id} ></SalesAndRevenueStatistics>}
            {showOrderFulfillmentStatistics && <OrderFullfillmentStatistics id={id} ></OrderFullfillmentStatistics>}
           
        </div>        
    );
}
