import { TagList } from "../../../../../Gigs/Tags/TagList";
import "./GigInfo.css";
import { FAQList } from "../../../../../Shared/FAQ/FAQList/FAQList";
import { ComparePackagesTable } from "../../../../../Gigs/PaymentPlan/PaymentPlan";
import { GigInfo } from "../../../../../Gigs/Details/GigDetails/GigInfo";
import { useParams } from "react-router-dom";
import axiosInstance from "../../../../../axiosInstance";
import { PaymentPlan } from "../../../../../Gigs/Preview/GigPreview";
import { useEffect, useState } from "react";

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

export function GigDetails() {
    const {id} = useParams<{ id: string }>();
    const gigId = id ? parseInt(id, 10) : NaN;
    const [gigDetails, setGigDetails] = useState<GigDetails | null>(null);

    const fetchGigDetails = async () => {
        try {
            const params = new URLSearchParams();
            params.append("Id", gigId.toString());
            params.append("Preview", "true");
            const url = `https://localhost:7267/gigs?${params.toString()}`;
            const response = await axiosInstance.get<GigDetails>(url);
           setGigDetails(response.data);
        } catch (error) {
            console.error("Error fetching gig details:", error);
        }
    };

    useEffect(() => {
        if (!isNaN(gigId)) {
            fetchGigDetails();
        }
    }, [gigId]);

    return (
        <div className="gig-info">
                    <div className="gig-preview-info d-flex flex-column">                       
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
                                               
                </div>
    );
}