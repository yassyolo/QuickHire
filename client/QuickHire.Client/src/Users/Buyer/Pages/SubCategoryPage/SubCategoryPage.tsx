import { useEffect, useState } from "react";
import { Gig } from "../FirstPage/FrontPage";
import { useParams } from "react-router-dom";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import { BuyerGigsFilter } from "../../../../Admin/Pages/PageFilters/BuyerGigsFilter";
import { GigCard } from "../../../../Gigs/Common/GigCard/GigCard";
import { Pagination } from "../../../../Shared/PageItems/Pagination/Pagination/Pagination";
import axios from "../../../../axiosInstance";
import { SubSubCategoriesInSubCategory } from "../../../../Shared/Modals/Deactivate/SubCategory/DeactivateSubCategoryModal";

export interface PaginatedResult {
    data: Gig[];
    totalPages: number;
}

interface SubCategoryPageData {
    mainCategoryId: number;
    mainCategoryName: string;
    mainCategoryDescription: string;
    subCategoryName: string;
}


export function SubCategoryPage() {
    const { id } = useParams<{ id: string }>();
    const subCategoryId = id ? parseInt(id, 10) : 0;

    const [gigs, setGigs] = useState<Gig[]>([]);
    const [mainCategory, setMainCategory] = useState<SubCategoryPageData | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);

    const [selectedPriceRangeId, setSelectedPriceRangeId] = useState<number | undefined>(undefined);
    const [selectedDeliveryTimeId, setSelectedDeliveryTimeId] = useState<number | undefined>(undefined);
    const [selectedCountryIds, setSelectedCountryIds] = useState<number[]>([]);
    const [selectedLanguageIds, setSelectedLanguageIds] = useState<number[]>([]);

    const itemsPerPage = 10;

    const [subCategories, setSubCategories] = useState<SubSubCategoriesInSubCategory[]>([]);
    const fetchSubSubCategories = async () => {
        try {
            const url = `https://localhost:7267/sub-sub-categories-in-sub-category/${id}`;
            const response = await axios.get<SubSubCategoriesInSubCategory[]>(url);
            setSubCategories(response.data);
        } catch (error) {
            console.error("Error fetching sub-categories:", error);
        }
    };

    const fetchMainCategory = async () => {
        try {
            const url = `https://localhost:7267/sub-categories/page/${subCategoryId}`;
            const response = await axios.get<SubCategoryPageData>(url);
            if (!response.data) {
                throw new Error("Main category data not found");
            }
            const data = response.data;
            setMainCategory(data);
        } catch (error) {
            console.error("Error fetching main category:", error);
        }
    };

const fetchGigs = async () => {
    setLoading(true);
    try {
        const body = {
            subCategoryId,
            currentPage,
            itemsPerPage,
            priceRangeId: selectedPriceRangeId || null,
            deliveryTimeId: selectedDeliveryTimeId || null,
            countryIds: selectedCountryIds,
            languageIds: selectedLanguageIds        };

        const url = `https://localhost:7267/sub-sub-category/gigs`;
        const result = await axios.post<PaginatedResult>(url, body);

        if (result.status !== 200) {
            throw new Error(`Failed to fetch gigs, status code: ${result.status}`);
        }

        setGigs(result.data.data);
        setTotalPages(result.data.totalPages);
    } catch (error) {
        console.error("Error fetching gigs:", error);
    } finally {
        setLoading(false);
    }
};


    const handleLikeGig = (liked: boolean, gigId: number) => {
        setGigs(prevGigs => 
            prevGigs.map(gig => 
                gig.id === gigId ? { ...gig, liked: liked } : gig
            )
        );
    }

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
    };

    useEffect(() => {
        fetchMainCategory();
fetchSubSubCategories();
        fetchGigs();
    }, [subCategoryId, currentPage, selectedPriceRangeId, selectedDeliveryTimeId, selectedCountryIds, selectedLanguageIds]);

    const handleOnSellerFiltersApply = (filters: { selectedCountryIds: number[]; selectedLanguageIds: number[] }) => {
        setSelectedCountryIds(filters.selectedCountryIds);
        setSelectedLanguageIds(filters.selectedLanguageIds);
        setCurrentPage(1); 
    };

    return (
        <SellerPage>
            <PageTitle
                title={mainCategory?.subCategoryName ?? "Sub Category"}
                description={mainCategory?.mainCategoryDescription ?? ""}
                breadcrumbs={[
                    { label: <i className="bi bi-house-door"></i>, to: "/buyer" },
                    { label: mainCategory?.mainCategoryName ?? "", to: `/buyer/main-categories/${mainCategory?.mainCategoryId}` },
                ]}
            />
            {subCategories && subCategories.length > 0 &&
                <div className="subCategories-row" style={{ display: 'flex', flexWrap: 'wrap', gap: '10px', marginTop: '20px' , marginBottom: '40px'}}>
                    {subCategories.map(subCategory => (
                        <div key={subCategory.id} className="subCategory-item" style={{ flex: '1', maxWidth: '200px', padding: '10px', border: '1px solid rgb(214, 213, 213)', borderRadius: '20px' }}>
                            <a href={`/buyer/sub-sub-categories/${subCategory.id}`} style={{fontSize: '15px', textDecoration: 'none', fontWeight: '600', color: '#95979D'}}>{subCategory.name}</a>
                        </div>
                    ))}
                </div>
            }

            <BuyerGigsFilter
            showServiceIncludesFilter={false}
               
                selectedPriceRangeId={selectedPriceRangeId}
                setSelectedPriceRangeId={setSelectedPriceRangeId}
                selectedDeliveryTimeId={selectedDeliveryTimeId}
                setSelectedDeliveryTimeId={setSelectedDeliveryTimeId}
                selectedCountryIds={selectedCountryIds}
                selectedLanguageIds={selectedLanguageIds}
                handleOnSellerFiltersApply={handleOnSellerFiltersApply}           />

            {loading ? (
                <p>Loading gigs...</p>
            ) : (
                <>
                    {gigs.length === 0 ? <p>No gigs found.</p> : (
                        <div className="gigs-grid" style={{ display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '20px', marginTop: '40px' }}>
                            {gigs.map(gig => (
                                <GigCard key={gig.id} gig={gig} showSeller={true} setLiked={handleLikeGig}/>
                            ))}
                        </div>
                    )}
                </>
            )}

            <div className="pagination-container" style={{ marginBottom: '50px'}}>
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
          </div>
        </SellerPage>
    );
}
