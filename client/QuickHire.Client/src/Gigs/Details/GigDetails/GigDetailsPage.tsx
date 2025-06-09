import { useEffect, useState } from "react";
import { FAQList } from "../../../Shared/FAQ/FAQList/FAQList";
import axios from "axios";
import { SellerPage } from "../../../Users/Seller/Pages/Common/SellerPage";
import { Breadcrumb } from "../../../Shared/Breadcrumb/Breadcrumb";
import { GigSellerDetails } from "../SellerDetails/SellerDetails";
import { ReviewsList } from "../../Reviews/ReviewsList/ReviewsList";
import { FavouriteButtonDropdown } from "../../../Users/Buyer/Favourites/FavouriteButtonDropdown";
import { useParams } from "react-router-dom";
import { TagList } from "../../Tags/TagList";
import { BrowsingHistoryRow } from "../../../Users/Buyer/BrowsingHistory/BrowsingHistoryRow/BrowsingHistoryRow";
import { ReportButtonDropdown } from "../../../Shared/Report/ReportButtonDropdown";
import { ComparePackagesTable } from "../../PaymentPlan/PaymentPlan";
import { GigInfo } from "./GigInfo";
import { GigMetaData, PaymentPlan } from "../../Preview/GigPreview";


interface GigDetails{
    mainCategoryId: number;
    subCategoryId: number;
    mainCategoryName: string;
    subCategoryName: string;
    numberOfLikes: number;
    liked: boolean;
    title: string;
    description: string;
    ordersInQueue: number;
    imageUrls: string[];
    paymentPlans: PaymentPlan[];
    gigMetaData: GigMetaData[];
}


export function GigDetailsPage() {
    const {id} = useParams<{ id: string }>();
    const gigId = id ? parseInt(id) : null;
    const [gigDetails, setGigDetails] = useState<GigDetails | null>(null);

    useEffect(() => {
        const fetchGigDetails = async () => {
            try {
                const url = `https://localhost:7267/gigs`;
                const params = new URLSearchParams();
                if (gigId !== null) {
                    params.append("id", gigId.toString());
                }
                params.append("preview", "false");
                const response = await axios.get<GigDetails>(url, { params });
                setGigDetails(response.data);
            } catch (error) {
                console.error("Error fetching gig details:", error);
            }
        };
        fetchGigDetails();
    }, [gigId]);

    const setLiked = (liked: boolean) => {
        setGigDetails((prev) => {
            if (prev) {
                return { ...prev, liked };
            }
            return prev;
        });
    };

    return (
        <>
        <SellerPage>
            <div className="d-flex flex-row justify-content-between">
        <Breadcrumb items={[
            { label: <i className="bi bi-house-door" />, to: "/buyer" },
            { label: gigDetails?.mainCategoryName, to: `/buyer/main-categories/${gigDetails?.mainCategoryId}` },
            { label: gigDetails?.subCategoryName, to: `/buyer/sub-categories/${gigDetails?.subCategoryId}` },
        ]} />
          <div className="gig-buttons">
            {gigId !== null && (
                <FavouriteButtonDropdown liked={gigDetails?.liked ?? false} gigId={gigId} setLiked={setLiked} />
            )} 
            <ReportButtonDropdown gigId={gigId ?? undefined}/>
            </div>            
            
           
            </div>
            <div className="gig-details-page d-flex flex-row justify-content-between">
                <div className="d-flex flex-column">
                     <GigInfo
                                                 description={gigDetails?.description ?? ""}
                                                 title={gigDetails?.title ?? ""}
                                                 imageUrls={gigDetails?.imageUrls ?? []}
                                                 ordersInQueue={gigDetails?.ordersInQueue ?? 0}
                                                 gigMetadata={gigDetails?.gigMetaData ?? []}
                                             />
                
                {gigId !== null && <GigSellerDetails gigId={gigId} />}
                <ComparePackagesTable plans={gigDetails?.paymentPlans ?? []}/>

                <ReviewsList gigId={gigId ?? undefined} userId={undefined} />
                <FAQList title={gigDetails?.title ?? ""} gigId={gigId ?? undefined} />
                <TagList gigId={gigId ?? undefined} />
                </div>
                               
            </div>
                
        </SellerPage>
        <BrowsingHistoryRow/>
        </>

    );

}