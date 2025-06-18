import { useEffect, useState } from "react";
import axios from "../../../axiosInstance";
import { FAQList } from "../../Common/FAQ/FAQList/FAQList";
import { ComparePackagesTable } from "../../Common/PaymentPlans/ComparePackages/PaymentPlan";
import { TagList } from "../../Common/Tags/TagList";
import "./GigPreview.css";
import { GigInfo } from "../GigDetails/GigDetails/GigInfo/GigInfo";
import { IconButton } from "../../../Shared/Buttons/IconButton/IconButton";

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
            <div className="gig-preview-info d-flex flex-column">               
                        <GigInfo
                            description={gigDetails?.description ?? ""}
                            title={gigDetails?.title ?? ""}
                            imageUrls={gigDetails?.imageUrls ?? []}
                            ordersInQueue={gigDetails?.ordersInQueue ?? 0}
                            
                            gigMetadata={gigDetails?.gigMetaData ?? []}
                        />
                        <div className="gig-preview-compare-packages" style={{width: '100%'}}>
                            <div className="gig-preview-tags-header" style={{marginBottom: '10px'}}>Compare packages</div>
                        <ComparePackagesTable plans={gigDetails?.paymentPlans ?? []} />     
                        </div>
                        <FAQList title={""} gigId={gigId ?? undefined} />
                        <div className="gig-preview-tags">
                            <div className="gig-preview-tags-header">Tags</div>
                                                    <TagList gigId={gigId ?? undefined} />
                        </div>
            </div>
                             <IconButton icon={<i className="bi bi-x"></i>} onClick={onGigPreviewClose} className={"close-button"} ariaLabel={"Close gig preview"} />
                      
          </div>  
        </div>
    );
}