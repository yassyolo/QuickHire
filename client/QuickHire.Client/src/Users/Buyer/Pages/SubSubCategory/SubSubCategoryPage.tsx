import { useEffect, useState } from "react";
import { Gig } from "../FirstPage/FrontPage";
import { useParams } from "react-router-dom";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import { BuyerGigsFilter } from "../../../../Admin/Pages/PageFilters/BuyerGigsFilter";
import { GigCard } from "../../../../Gigs/Common/GigCard/GigCard";
import { Pagination } from "../../../../Shared/PageItems/Pagination/Pagination/Pagination";
import { SelectedOption } from "../../../../Shared/Dropdowns/Populate/ServiceIncludes/ServiceIncludes";
import axios from "../../../../axiosInstance";

export interface PaginatedResult {
    data: Gig[];
    totalPages: number;
}

interface SubSubCategoryPageData {
    mainCategoryId: number;
    mainCategoryName: string;
    mainCategoryDescription: string;
    subSubCategoryName: string;
    subCategoryName: string;
    subCategoryId: number;
}

export function SubSubCategoryPage() {
    const { id } = useParams<{ id: string }>();
    const subSubCategoryId = id ? parseInt(id, 10) : 0;

    const [gigs, setGigs] = useState<Gig[]>([]);
    const [mainCategory, setMainCategory] = useState<SubSubCategoryPageData | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);

    const [selectedPriceRangeId, setSelectedPriceRangeId] = useState<number | undefined>(undefined);
    const [selectedDeliveryTimeId, setSelectedDeliveryTimeId] = useState<number | undefined>(undefined);
    const [selectedCountryIds, setSelectedCountryIds] = useState<number[]>([]);
    const [selectedLanguageIds, setSelectedLanguageIds] = useState<number[]>([]);
    const [selectedOptions, setSelectedOptions] = useState<SelectedOption[]>([]);

    const itemsPerPage = 10;

    const fetchMainCategory = async (subSubCategoryId: number) => {
        try {
            const url = `https://localhost:7267/sub-sub-categories/page/${subSubCategoryId}`;
            const response = await axios.get<SubSubCategoryPageData>(url);
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
            subSubCategoryId,
            currentPage,
            itemsPerPage,
            priceRangeId: selectedPriceRangeId || null,
            deliveryTimeId: selectedDeliveryTimeId || null,
            countryIds: selectedCountryIds,
            languageIds: selectedLanguageIds,
            selectedOptionIds: selectedOptions.map(option => option.optionId)
        };

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
        fetchMainCategory(subSubCategoryId);
    }, [subSubCategoryId]);

    useEffect(() => {
        fetchGigs();
    }, [subSubCategoryId, currentPage, selectedPriceRangeId, selectedDeliveryTimeId, selectedCountryIds, selectedLanguageIds]);

    const handleOnSellerFiltersApply = (filters: { selectedCountryIds: number[]; selectedLanguageIds: number[] }) => {
        setSelectedCountryIds(filters.selectedCountryIds);
        setSelectedLanguageIds(filters.selectedLanguageIds);
        setCurrentPage(1); 
    };

    return (
        <SellerPage>
            <PageTitle
                title={mainCategory?.subSubCategoryName ?? "Sub-Sub Category"}
                description={mainCategory?.mainCategoryDescription ?? ""}
                breadcrumbs={[
                    { label: <i className="bi bi-house-door"></i>, to: "/buyer" },
                    { label: mainCategory?.mainCategoryName ?? "", to: `/buyer/main-categories/${mainCategory?.mainCategoryId}` },
                    { label: mainCategory?.subCategoryName ?? "", to: `/buyer/sub-categories/${mainCategory?.subCategoryId}` },
                ]}
            />

            <BuyerGigsFilter
            showServiceIncludesFilter={true}
            subSubCategoryId={subSubCategoryId}
                selectedPriceRangeId={selectedPriceRangeId}
                setSelectedPriceRangeId={setSelectedPriceRangeId}
                selectedDeliveryTimeId={selectedDeliveryTimeId}
                setSelectedDeliveryTimeId={setSelectedDeliveryTimeId}
                selectedCountryIds={selectedCountryIds}
                selectedLanguageIds={selectedLanguageIds}
                handleOnSellerFiltersApply={handleOnSellerFiltersApply} selectedOptions={selectedOptions} setSelectedOptions={setSelectedOptions}            />

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

            <div className="pagination-container" style={{marginBottom: '50px', marginTop: '20px'}}>
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
          </div>
        </SellerPage>
    );
}
