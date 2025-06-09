import { useEffect, useState } from "react";
import axios from "axios";
import { FAQList } from "../../Shared/FAQ/FAQList/FAQList";
import { ComparePackagesTable } from "../PaymentPlan/PaymentPlan";
import { TagList } from "../Tags/TagList";
import { Breadcrumb } from "../../Shared/Breadcrumb/Breadcrumb";
import PaymentPlansCard from "../PaymentPlan/PaymentPlansCard";
import "./GigPreview.css";
import { GigInfo } from "../Details/GigDetails/GigInfo";
import { IconButton } from "../../Shared/Buttons/IconButton/IconButton";

interface GigPreviewProps {
    gigId: number;
    onGigPreviewClose: () => void;
}

interface PaymentPlanInclude {
  name: string;
  value: string;
}

export interface PaymentPlan {
  id: number;
  name: string;
  price: number;
  description: string;
  deliveryTimeInDays: number;
  revisions: number;
  inclusions: PaymentPlanInclude[];
} 
interface GigDetails{
    mainCategoryId: number;
    subCategoryId: number;
    mainCategoryName: string;
    subCategoryName: string;
    title: string;
    description: string;
    imageUrls: string[];
    paymentPlans: PaymentPlan[];
    gigMetaData: GigMetaData[];
    ordersInQueue: number;
}

export interface GigMetaData {
    title: string;
    items: string[];
}
export function GigPreview({ gigId, onGigPreviewClose }: GigPreviewProps) {
    const [gigDetails, setGigDetails] = useState<GigDetails | null>(null);
    

        const fetchGigDetails = async () => {
            try {
                const params = new URLSearchParams();
                params.append("Id", gigId.toString());
                params.append("Preview", "true");
                const response = await axios.get<GigDetails>(`https://localhost:7267/gigs?${params.toString()}`);
                console.log("Gig Details:", response.data);
                setGigDetails(response.data);
            } catch (error) {
                console.error("Error fetching gig details:", error);
            }
        };

    useEffect(() => {
        fetchGigDetails();
    }, [gigId]);

    return (
        <div className="gig-preview-overlay">
          <div className="gig-details-page d-flex flex-row justify-content-between">
            <IconButton icon={<i className="bi bi-x"></i>} onClick={onGigPreviewClose} className={"Close gig preview"} ariaLabel={"Close gig preview"} />
            <div className="gig-preview-info d-flex flex-column">
                <Breadcrumb items={[
                        { label: <i className="bi bi-house-door" />, to: "/buyer" },
                        { label: gigDetails?.mainCategoryName, to: `/buyer/main-categories/${gigDetails?.mainCategoryId}` },
                        { label: gigDetails?.subCategoryName, to: `/buyer/sub-categories/${gigDetails?.subCategoryId}` },
                    ]} />
                        <GigInfo
                            description={gigDetails?.description ?? ""}
                            title={gigDetails?.title ?? ""}
                            imageUrls={gigDetails?.imageUrls ?? []}
                            ordersInQueue={gigDetails?.ordersInQueue ?? 0}
                            gigMetadata={gigDetails?.gigMetaData ?? []}
                        />
                        
                        <ComparePackagesTable plans={gigDetails?.paymentPlans ?? []} />     
                        <FAQList title={gigDetails?.title ?? ""} gigId={gigId ?? undefined} />
                        <TagList gigId={gigId ?? undefined} />
            </div>
                        <PaymentPlansCard plans={gigDetails?.paymentPlans ?? []} />

                                       
          </div>  
        </div>
    );
}