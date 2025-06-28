import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";
import { PageTitle } from "../../../../Shared/PageItems/PageTitle/PageTitle";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import { GigCard } from "../../../../Gigs/Common/GigCard/GigCard";
import { Pagination } from "../../../../Shared/PageItems/Pagination/Pagination/Pagination";
import axios from "../../../../axiosInstance";
import { Gig } from "../FirstPage/FrontPage";
import { BuyerGigsFilter } from "../../../../Admin/Pages/PageFilters/BuyerGigsFilter";

interface PaginatedResult {
    data: Gig[];
    totalPages: number;
}

export function BuyerSearchResultsPage() {
    const [searchParams] = useSearchParams();
    const keyword = searchParams.get("keyword") || "";

    const [gigs, setGigs] = useState<Gig[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);

    const [selectedPriceRangeId, setSelectedPriceRangeId] = useState<number | undefined>(undefined);
    const [selectedDeliveryTimeId, setSelectedDeliveryTimeId] = useState<number | undefined>(undefined);
    const [selectedCountryIds, setSelectedCountryIds] = useState<number[]>([]);
    const [selectedLanguageIds, setSelectedLanguageIds] = useState<number[]>([]);

    const itemsPerPage = 12;

    const fetchSearchResults = async () => {
        if (!keyword.trim()) return;
        setLoading(true);
        try {
            const body = {
                keyword: keyword.trim(),
                currentPage,
                itemsPerPage,
                priceRangeId: selectedPriceRangeId || null,
                deliveryTimeId: selectedDeliveryTimeId || null,
                countryIds: selectedCountryIds,
                languageIds: selectedLanguageIds,
            };

        const url = `https://localhost:7267/sub-sub-category/gigs`;
        const response = await axios.post<PaginatedResult>(url, body);

            setGigs(response.data.data);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            console.error("Error fetching search results:", error);
        } finally {
            setLoading(false);
        }
    };

    const handleLikeGig = (liked: boolean, gigId: number) => {
        setGigs(prev => prev.map(g => g.id === gigId ? { ...g, liked } : g));
    };

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
    };

    const handleOnSellerFiltersApply = (filters: { selectedCountryIds: number[]; selectedLanguageIds: number[] }) => {
        setSelectedCountryIds(filters.selectedCountryIds);
        setSelectedLanguageIds(filters.selectedLanguageIds);
        setCurrentPage(1);
    };

    useEffect(() => {
        setCurrentPage(1);
    }, [keyword]);

    useEffect(() => {
        fetchSearchResults();
    }, [keyword, currentPage, selectedPriceRangeId, selectedDeliveryTimeId, selectedCountryIds, selectedLanguageIds]);

    return (
        <SellerPage>
            <PageTitle
                title={`Results for: "${keyword}"`}
                description="Browse gigs matching your search"
            />

            <BuyerGigsFilter
            showServiceIncludesFilter={false}
                selectedPriceRangeId={selectedPriceRangeId}
                setSelectedPriceRangeId={setSelectedPriceRangeId}
                selectedDeliveryTimeId={selectedDeliveryTimeId}
                setSelectedDeliveryTimeId={setSelectedDeliveryTimeId}
                selectedCountryIds={selectedCountryIds}
                selectedLanguageIds={selectedLanguageIds}
                handleOnSellerFiltersApply={handleOnSellerFiltersApply}
            />

            {loading ? (
                <p>Loading search results...</p>
            ) : gigs.length === 0 ? (
                <p>No gigs found for "{keyword}".</p>
            ) : (
                <div className="gigs-grid" style={{ display: 'grid', gridTemplateColumns: 'repeat(4, 1fr)', gap: '20px', marginTop: '40px' }}>
                    {gigs.map(gig => (
                        <GigCard key={gig.id} gig={gig} showSeller={true} setLiked={handleLikeGig} />
                    ))}
                </div>
            )}

            <div className="pagination-container" style={{marginBottom: '30px', marginTop: '50px'}}>
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange} />
            </div>
        </SellerPage>
    );
}
