import { useEffect, useState } from "react";
import { FAQList } from "../../../Common/FAQ/FAQList/FAQList";
import axiosInstance from "../../../../axiosInstance";
import { Breadcrumb } from "../../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { GigSellerDetails } from "./SellerForGigDetails/SellerDetails";
import { ReviewsList } from "../../../../Orders/Pages/Reviews/ReviewsLIst/ReviewsList";
import { FavouriteButtonDropdown } from "../../../../Users/Buyer/Common/Favourites/MakeFavourite/FavouriteButtonDropdown";
import { useParams } from "react-router-dom";
import { TagList } from "../../../Common/Tags/TagList";
import { BrowsingHistoryRow } from "../../../../Users/Buyer/Pages/BuyerBrowsingHistoryRow/BrowsingHistoryRowItem/BrowsingHistoryRow";
import { ReportButtonDropdown } from "../../../../Users/Buyer/Common/Report/ReportButtonDropdown";
import { ComparePackagesTable } from "../../../Common/PaymentPlans/ComparePackages/PaymentPlan";
import { GigInfo } from "./GigInfo/GigInfo";
import { GigMetaData, PaymentPlan } from "../../GigPreview/GigPreview";
import './GigDetailsPage.css';
import PaymentPlansCard from "../../../Common/PaymentPlans/ChoosePaymentPlan/PaymentPlansCard";
import { RatingDistribution } from "../../../../Orders/Pages/Reviews/RatingDistrbution/RatingDistribution";
import { ContactMe } from "../../../../Users/Buyer/Pages/SellerDetailsPage/ContactMe/ContactMe";


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
    userId: string;
}


export function GigDetailsPage() {
    const {id} = useParams<{ id: string }>();
    const gigId = id ? parseInt(id) : null;
    const [gigDetails, setGigDetails] = useState<GigDetails | null>(null);

    useEffect(() => {
        const fetchGigDetails = async () => {
            try {
        const params = new URLSearchParams();
        if (gigId !== null) {
            params.append("Id", gigId.toString());
        }
        params.append("Preview", false.toString());
        const url = `https://localhost:7267/gigs?${params.toString()}`;
        const response = await axiosInstance.get<GigDetails>(url);
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
        <div style={{padding: '20px 90px'}}>
            <div className="d-flex flex-row justify-content-between" style={{paddingRight: '40px'}}>
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
            <div className="gig-details-page-buyer d-flex flex-row justify-content-between">
                <div className="buyer-gig-details d-flex flex-column">
                     <GigInfo
                                                 description={gigDetails?.description ?? ""}
                                                 title={gigDetails?.title ?? ""}
                                                 imageUrls={gigDetails?.imageUrls ?? []}
                                                 ordersInQueue={gigDetails?.ordersInQueue ?? 0}
                                                 gigMetadata={gigDetails?.gigMetaData ?? []}
                                             />
                <div className="gig-seller-details-container" style={{ margin: "40px 0 0 0" }}>
                                    {gigId !== null && <GigSellerDetails gigId={gigId} />}

                </div>
                <div className="gig-details-tags" style={{ marginBottom: "20px" }}>
                    <div className="gig-details-title">Compare packages</div>
                    <ComparePackagesTable plans={gigDetails?.paymentPlans ?? []}/>
                </div>
<div className="reviews-container d-flex flex-column" style={{ margin: "20px 0" }}>
                    <RatingDistribution gigId={gigId ?? undefined} />
                    <ReviewsList gigId={gigId ?? undefined} userId={undefined} />


</div>
{gigId && <FAQList title={""} gigId={gigId} />}
                <div className="gig-details-tags">
                                                            <div className="gig-details-title">Tags</div>
                                                            {gigId &&                     <TagList gigId={gigId ?? undefined} />
}
                </div>
                </div>
                <div className="d-flex flex-column" style={{gap: '20px'}}>
                    <div className="d-flex flex-column" style={{gap: '30px'}}>
                    <div className="payment-plans-container">
                        {gigDetails && gigDetails.paymentPlans.length > 0 && gigId !== null &&
                                    <PaymentPlansCard plans={gigDetails.paymentPlans} gigId={gigId}/>}
                    </div>
                </div>
                <ContactMe userId={gigDetails?.userId ?? ""}/>
                </div>
                

                               
            </div>
                
        </div>
        <BrowsingHistoryRow/>
        </>

    );

}