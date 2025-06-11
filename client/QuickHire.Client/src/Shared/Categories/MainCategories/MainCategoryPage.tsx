import {  useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import axios from "../../../axiosInstance";
import { MostPopularInMainCategory } from "./MostPopularInMainCategory";
import { ExploreSubcategoriesInMainCategory } from "./ExploreSubcategoriesInMainCategory";
import { TagList } from "../../../Gigs/Tags/TagList";
import "./MainCategoryPage.css";
import { SellerPage } from "../../../Users/Seller/Pages/Common/SellerPage";
import { BrowsingHistoryRow } from "../../../Users/Buyer/BrowsingHistory/BrowsingHistoryRow/BrowsingHistoryRow";
import { FAQList } from "../../FAQ/FAQList/FAQList";

export interface MainCategory{
    id: number;
    name: string;
    description: string;
}

export function MainCategoryPage() {
    const { id } = useParams<{id: string}>();
    const mainCategoryId = id ? parseInt(id) : null;
    const [mainCategory, setMainCategory] = useState<MainCategory | null>(null);

    useEffect(() => {
        if (mainCategoryId) {
            const fetchMainCategory = async () => {
                try {
                    const response = await axios.get<MainCategory>(`https://localhost:7267/main-categories/page/${mainCategoryId}`);
                    setMainCategory(response.data);
                } catch (error) {
                    console.error("Error fetching main category:", error);
                }
            };
            fetchMainCategory();
        }
    }, [mainCategoryId]);


    return(
            <><SellerPage>
            <div className="main-category-page">

                {mainCategoryId !== null && mainCategory && (
                    <MostPopularInMainCategory
                        mainCategoryName={mainCategory.name}
                        mainCategoryId={mainCategoryId} />
                )}
                {mainCategoryId !== null && mainCategory && (
                    <div className="explore-main-category-page-subcategories">
                        <ExploreSubcategoriesInMainCategory
                        mainCategoryId={mainCategoryId}
                        mainCategoryName={mainCategory.name} />
                    </div>
                    
                )}
            </div>


        </SellerPage><div className="faqs-tags-main-category">
                {mainCategoryId !== null && mainCategory && (
                    <div className="main-category-faqs">
                        <FAQList mainCategoryId={mainCategoryId} title={mainCategory.name}></FAQList>
                    </div>
                )}
                <div className="tags-in-main-category">
                    <div className="you-might-be-interested-in-title">You might be interested in {mainCategory?.name}</div>
                    {mainCategoryId !== null && mainCategory && (
                        <TagList mainCategoryId={mainCategoryId}></TagList>
                    )}
                </div>
            </div>
            <BrowsingHistoryRow/>
            </>)
}