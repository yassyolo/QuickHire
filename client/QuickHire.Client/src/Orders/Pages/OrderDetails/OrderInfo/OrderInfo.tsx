import { useNavigate } from "react-router-dom";
import { GigRequirement, GigRequirements } from "../GigRequirements/GigRequirements";
import ChosenPlan from "../../PlaceOrder/Steps/ChosenPlan";
import { PaymentPlan } from "../../../../Gigs/Pages/GigPreview/GigPreview";
import './OrderInfo.css';

interface OrderInfoProps { 
    gigRequirements: GigRequirement[];
    gigId: number;
    gigTitle: string;
    gigImageUrl: string;
    orderNumber: string;
    paymentPlan: PaymentPlan;
}

export function OrderInfo({ paymentPlan, gigRequirements, gigId, gigImageUrl, gigTitle, orderNumber }: OrderInfoProps) {
    const navigate = useNavigate();
    const handleGigClick = () => {
      navigate(`/buyer/gigs/${gigId}`);

    };
    return(
        <div className="order-info d-flex flex-column">
                <div className="order-number">Order Number: {orderNumber}</div>
                <div className="order-info-title">Service</div>
                <div className="order-info-gig d-flex flex-row" onClick={handleGigClick}>
                    <img className="gig-image" src={gigImageUrl} alt={gigTitle} />
                        <div className="order-info-gig-gig-title">{gigTitle}</div>
                </div>
                                <div className="order-info-title">Chosen plan</div>

                <ChosenPlan selectedPlan={paymentPlan} />
                                <div className="order-info-title">Requirements</div>

                <GigRequirements requirements={gigRequirements ?? []}/>
        </div>
    )
}