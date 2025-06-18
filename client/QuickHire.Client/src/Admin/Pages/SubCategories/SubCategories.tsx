import { useCallback, useEffect, useState } from "react";
import {Pagination} from "../../../Shared/PageItems/Pagination/Pagination/Pagination";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { DataTable } from "../../../Shared/Tables/Common/DataTable/AdminDataTable";
import { AddSubCategoryModal } from "../../../Shared/Modals/Add/SubCategory/AddSubCategoryModal";
import { SubCategoriesFilter } from "../PageFilters/SubCategoriesFilter";
import axios from "../../../axiosInstance";
import { PageTitle } from "../../../Shared/PageItems/PageTitle/PageTitle";
import { SubCategoryActions } from "../../../Shared/Tables/TableActions/Categories/SubCategory/SubCategoryActions";

export interface SubCategoryRow{
id: number;
name: string;
mainCategoryName: string;
subSubCategories: number;
createdOn: string;
clicks: number;
imageUrl: string;
}

const tableHeaders = {
    id: "ID",
    name: "Name",
    mainCategoryName: "Main Category",
    subSubCategories: "Sub Sub Categories",
    clicks: "Clicks",
    createdOn: "Created On",
    imageUrl: "Image"
};

export interface PaginatedResult{
    data: SubCategoryRow[];
    totalPages: number;
}

export function SubCategories (){
    const [id, setId] = useState<number | undefined>(undefined);
    const [keyword, setKeyword] = useState<string>('');
    const [mainCategoryId, setMainCategoryId] = useState<number>(0);
    const [subCategories, setSubCategories] = useState<SubCategoryRow[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const itemsPerPage = 10;
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [showAddModal, setShowAddModal] = useState<boolean>(false);
    const [totalPages, setTotalPages] = useState<number>(0);

    const handlePageChange = (page: number) => setCurrentPage(page);

    useEffect(() => {
        setCurrentPage(1);
    }, [id, keyword, mainCategoryId]);
    
    const fetchCategories = useCallback(async () => {
        setLoading(true);
        try {
            const params = new URLSearchParams();
            if (id !== undefined) params.append("Id", id.toString());
            if (keyword) params.append("Keyword", keyword);
            if (mainCategoryId) params.append("MainCategoryId", mainCategoryId.toString());
            params.append("CurrentPage", currentPage.toString());
            params.append("ItemsPerPage", itemsPerPage.toString());

            const url = `https://localhost:7267/admin/sub-categories?${params.toString()}`;
    
            const response = await axios.get<PaginatedResult>(url);

            setSubCategories(response.data.data);
            setTotalPages(response.data.totalPages);
        } catch (error) {
            console.error("Error fetching categories:", error);
        } finally {
            setLoading(false);
        }
    }, [id, keyword, currentPage, mainCategoryId]);
    
    useEffect(() => {
        fetchCategories();
    }, [id, keyword, currentPage, mainCategoryId, fetchCategories]);

    const handleAddModalVisbility = () => setShowAddModal(!showAddModal);
    const handleAddSubCategorySucess = () => {
        setShowAddModal(false);
        setKeyword('');
        setId(undefined);
        setMainCategoryId(0);
        setCurrentPage(1);
        fetchCategories();
    };
    const handleDeactivateSuccess = () => 
    {
        setKeyword('');
        setId(undefined);
        setMainCategoryId(0);
        setCurrentPage(1);
    }  
      const handleEditSuccess = (id: number, newName: string, newImageUrl: string)  => {
        setSubCategories(prev => prev.map(sc => sc.id === id ? { ...sc, name: newName, imageUrl: newImageUrl } : sc));
    };
    const handleMainCategoryIdSelect = (id: number) => setMainCategoryId(id);



    return(
            <><div className="filter-table">
            <PageTitle title="Sub Categories" description="Manage and organize your platform’s sub categories efficiently." breadcrumbs={[{ label: <i className="bi bi-house-door"></i> }, { label: "Sub Categories" }]} />
            <div className="d-flex flex-column">
                <SubCategoriesFilter setId={setId} setKeyword={setKeyword} setSelectedMainCategoryId={handleMainCategoryIdSelect} selectedMainCategoryId={mainCategoryId} />
                <ActionButton onClick={handleAddModalVisbility} text={"CREATE A NEW CATEGORY"} className="add-category-button" ariaLabel={"Add Sub Category Button"} />
            </div>
            <AddSubCategoryModal showModal={showAddModal} onClose={handleAddModalVisbility} onAddSubCategorySuccess={handleAddSubCategorySucess} showCategoriesPopulate={true} submitedMainCategoryId={0} />
            {loading ? (<div className="loading">Loading...</div>
            ) : (
                <div className="categories-list">
                    <DataTable data={subCategories} columns={["id", "name", "mainCategoryName", "subSubCategories", "clicks", "createdOn"]} headers={tableHeaders}
                        renderActions={(row: SubCategoryRow) => (
                            <SubCategoryActions category={row} onEditSuccess={handleEditSuccess} onDeactivateSuccess={handleDeactivateSuccess} details={{ name: row.name, imageUrl: row.imageUrl }}></SubCategoryActions>)} />
                </div>
            )}
        </div><div className="pagination-container">
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange}></Pagination>
            </div></>
);

}