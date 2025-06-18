import { useCallback, useEffect, useState } from "react";
import {Pagination} from "../../../Shared/PageItems/Pagination/Pagination/Pagination";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { DataTable } from "../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { SubSubCategoriesFilter } from "../PageFilters/SubSubCategoriesFilter";
import { SubSubCategoryActions } from "../../../Shared/Tables/TableActions/Categories/SubSubcategory/SubSubCategoryActions";
import axios from "../../../axiosInstance";
import { PageTitle } from "../../../Shared/PageItems/PageTitle/PageTitle";
import { AddSubSubCategoryModal } from "../../../Shared/Modals/Add/SubSubCategory/AddSubSubCategoryModal";

export interface SubSubCategoryRow{
id: number;
name: string;
filters: number;
createdOn: string;
clicks: number;
gigs: number;
}

const tableHeaders = {
    id: "ID",
    name: "Name",
    gigs: "Gigs",
    filters: "Filters",
    clicks: "Clicks",
    createdOn: "Created On",
};

export interface PaginatedResult{
    data: SubSubCategoryRow[];
    totalPages: number;
}

export function SubSubCategories (){
    const [id, setId] = useState<number | undefined>(undefined);
    const [keyword, setKeyword] = useState<string>('');
    const [subCategoryId, setSubCategoryId] = useState<number>(0);
    const [subSubCategories, setSubSubCategories] = useState<SubSubCategoryRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const itemsPerPage = 30;
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [showAddModal, setShowAddModal] = useState<boolean>(false);
    const [totalPages, setTotalPages] = useState<number>(0);

    const handlePageChange = (page: number) => {
        setCurrentPage(page);
        fetchSubCategories();
    };

    useEffect(() => {
        setCurrentPage(1);
    }
    , [id, keyword, subCategoryId]);

    useEffect(() => {
        fetchSubCategories();
    }, [id, keyword, currentPage, subCategoryId]);

    const handleAddClick = () => setShowAddModal(true);
    const handleCloseModal = () => setShowAddModal(false);
    const handleDeactivateSuccess = (id: number) => setSubSubCategories(prev => prev.filter(cat => cat.id !== id));
    const handleEditSuccess = (id: number, newName: string) => {
        setSubSubCategories(prev => prev.map(cat => cat.id === id ? { ...cat, name: newName } : cat));
    }
    const handleSubCategoryIdSelect = (id: number) => {
        setSubCategoryId(id);
    };
    const handleAddSubSubCategorySucess = () => {
        setShowAddModal(false);
    };

     const fetchSubCategories = useCallback(async () => {
            setLoading(true);
            try {
                const params = new URLSearchParams();
                if (id !== undefined) params.append("id", id.toString());
                if (keyword) params.append("keyword", keyword);
                params.append("CurrentPage", currentPage.toString());
                params.append("ItemsPerPage", itemsPerPage.toString());
    
                const url = `https://localhost:7267/admin/sub-sub-categories?${params.toString()}`;
    
                const response = await axios.get(url, {
                    headers: {
                        'Accept': '*/*',
                    },
                });
    
                const result: PaginatedResult = response.data;
                console.log(result);
                setSubSubCategories(result.data);
                setTotalPages(result.totalPages);
            } catch (error) {
                console.error("Error fetching categories:", error);
            } finally {
                setLoading(false);
            }
        }, [id, keyword, currentPage]);


      useEffect(() => {
        fetchSubCategories();
    }, [id, keyword, currentPage, subCategoryId, fetchSubCategories]);

    return(
            <><div className="filter-table">
            <PageTitle title="Sub Sub Categories" description="Manage and organize your platform’s sub sub categories efficiently." breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/admin" }, { label: "Sub Sub Categories" }]} />
            <div className="d-flex flex-column">
                <SubSubCategoriesFilter setId={setId} setKeyword={setKeyword} setSelectedSubCategoryId={handleSubCategoryIdSelect} selectedSubCategoryId={subCategoryId} />
                <ActionButton onClick={handleAddClick} text={"CREATE A NEW CATEGORY"} className="add-category-button" ariaLabel={"Add Sub Sub Category Button"} />
            </div>
            <AddSubSubCategoryModal showModal={showAddModal} onClose={handleCloseModal} onAddSubSubCategorySuccess={handleAddSubSubCategorySucess} title={"Sub Sub Category"} showCategoriesPopulate={true} submitedSubCategoryId={0} />
            {loading ? (
                <div className="loading">Loading...</div>
            ) : (
                <div className="categories-list">
                    <DataTable data={subSubCategories} columns={["id", "name", "filters", "gigs", "clicks", "createdOn"]} headers={tableHeaders}
                        renderActions={(row: SubSubCategoryRow) => (
                            <SubSubCategoryActions details={{ name: row.name }} category={row} onEditSuccess={handleEditSuccess} onDeactivateSuccess={handleDeactivateSuccess}></SubSubCategoryActions>
                        )} />
                </div>
            )}
        </div>
                    <div className="pagination-container" style={{marginBottom: '30px', marginTop: '50px'}}>

                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div></>
);

}