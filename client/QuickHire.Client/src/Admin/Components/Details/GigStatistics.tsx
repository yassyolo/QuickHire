import { useState } from "react";
import { EngagementStatistics } from "../Charts/EngagementStatistics";
import { CustomerFeedbackStatistics } from "../Charts/CustomerFeedbackStatistics";
import { Item } from "../Dropdowns/Common/PopulateDropdown";
import { TitleFilterSelector } from "../../Pages/Common/TitleFilterSection";

export interface GigStatisticsProps {
    id: number;
    selectedStatisticsTypeId: number;
}

export function GigStatistics({id, selectedStatisticsTypeId} : GigStatisticsProps) {
    const [showEngagementStatistics, setShowEngagementStatistics] = useState(false);
    const [showCustomerFeedbackStatistics, setShowCustomerFeedbackStatistics] = useState(false);

    const [selectedId, setSelectedId] = useState(selectedStatisticsTypeId);
    /*const [showSalesAndRevenueStatistics, setShowSalesAndRevenueStatistics] = useState(false);
    const [showOrderFulfillmentStatistics, setShowOrderFulfillmentStatistics] = useState(false);*/

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
        }
        else if(id === 2) {
            setShowCustomerFeedbackStatistics(true);
            setShowEngagementStatistics(false);
        }
        /*else if(id === 3) {
            setShowSalesAndRevenueStatistics(false);
        }
        else if(id === 4) {
            setShowOrderFulfillmentStatistics(false);
        }*/
    }
    return(      
        <div className="gigs-statistics-admin">
            <div className="gig-statistics filter">
              <TitleFilterSelector selectedId={selectedId} setSelectedId={handleSelectedStatisticsType} data={data}/>           
            </div>
            {showEngagementStatistics && <EngagementStatistics id={id} ></EngagementStatistics>}
            {showCustomerFeedbackStatistics && <CustomerFeedbackStatistics id={id} ></CustomerFeedbackStatistics>}
           
        </div>        
    );
}
 /*{showCustomerFeedbackStatistics && }
            {showSalesAndRevenueStatistics && }
            {showOrderFulfillmentStatistics && }*/