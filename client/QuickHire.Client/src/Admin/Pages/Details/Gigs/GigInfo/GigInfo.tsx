import { TagList } from "../../../../../Gigs/Common/Tags/TagList";
import "./GigInfo.css";
import { FAQList } from "../../../../../Gigs/Common/FAQ/FAQList/FAQList";
import { ComparePackagesTable } from "../../../../../Gigs/Common/PaymentPlans/ComparePackages/PaymentPlan";
import { GigInfo } from "../../../../../Gigs/Pages/GigDetails/GigDetails/GigInfo/GigInfo";
import { useParams } from "react-router-dom";
import axiosInstance from "../../../../../axiosInstance";
import { PaymentPlan } from "../../../../../Gigs/Pages/GigPreview/GigPreview";
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
        <div className="gig-info" style={{width: '1000px'}}>
                    <div className="gig-preview-info d-flex flex-column">
                        <div style={{height: '800px', width: '100%'}}>
                             <GigInfo
                                    description={gigDetails?.description ?? ""}
                                    title={gigDetails?.title ?? ""}
                                    imageUrls={gigDetails?.imageUrls ?? []}
                                    ordersInQueue={gigDetails?.ordersInQueue ?? 0}
                                    gigMetadata={gigDetails?.gigMetaData ?? []}
                                />   
                        </div>
                                               
                               
                            <div style={{marginBottom: '10px', fontSize: '20px', fontWeight: '600', width: '100%'}}>Compare packages</div>
                                <ComparePackagesTable plans={gigDetails?.paymentPlans ?? []} />     
                                <FAQList title={""} gigId={gigId ?? undefined} />
                                <div className="tags-lis-gig-info" style={{ marginTop: "20px" , justifyContent: "flex-start", width: "100%" }}>
                                    <div style={{marginBottom: '10px', fontSize: '20px', fontWeight: '600'}}>Tags</div>
                                                                    <TagList gigId={gigId ?? undefined} />
                                </div>
                    </div>
                                               
                </div>
    );
}