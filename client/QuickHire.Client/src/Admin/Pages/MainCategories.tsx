import { useCallback, useEffect, useState } from "react";
import { AddMainCategoryModal } from "../Components/Modals/Add/AddMainCategoryModal";
import { DataTable } from "../Components/Tables/Common/AdminDataTable";
import { ActionButton } from "../../Shared/Buttons/ActionButton/ActionButton";
import { MainCategoriesFilter } from "../Components/Filters/PageFilters/MainCategoriesFilter";
import { Pagination } from "../../Shared/Pagination/Pagination/Pagination";
import { PageTitle } from "./Common/PageTitle";
import { AdminPage } from "./Common/AdminPage";
import { MainCategoryActions } from "../Components/Tables/TableActions/MainCategoryActions";

export interface MainCategoryRowModel {
    id: number;
    name: string;
    description: string;
    subCategories: number;
    clicks: number;
    createdOn: string;
}

export interface MainCategoriesResult {
    data: MainCategoryRowModel[];
    totalPages: number;
}

const tableHeaders = {
    id: "ID",
    name: "Name",
    description: "Description",
    subCategories: "Sub Categories",
    clicks: "Clicks",
    createdOn: "Created On",
};

export function MainCategories() {
    const [id, setId] = useState<number | undefined>(undefined);
    const [keyword, setKeyword] = useState<string>('');
    const [categories, setCategories] = useState<MainCategoryRowModel[]>([]);
    const [loading, setLoading] = useState<boolean>(false);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const itemsPerPage = 5;
    const [totalPages, setTotalPages] = useState<number>(0);
    const [showAddModal, setShowAddModal] = useState<boolean>(false);

    const handleAddModalVisbility = () => setShowAddModal(!showAddModal);
    const handleAddMainCategorySuccess = () => {
        setShowAddModal(false);
        setKeyword('');
        setId(undefined);
        setCurrentPage(1);
    };
    const handleDeactivateSuccess = () => 
    {
        setKeyword('');
        setId(undefined);
        setCurrentPage(1);
    }
    const handleEditSuccess = (id: number, newName: string, newDescription: string) => {
        setCategories(prev => prev.map(cat => cat.id === id ? { ...cat, name: newName, description: newDescription } : cat));
    };
    const handlePageChange = (page: number) => setCurrentPage(page);

    useEffect(() => {
        setCurrentPage(1);
    }, [id, keyword]);

    const fetchCategories = useCallback(async () => {
        setLoading(true);
        try {
            const params = new URLSearchParams();
            if (id !== undefined) params.append("id", id.toString());
            if (keyword) params.append("keyword", keyword);
            params.append("CurrentPage", currentPage.toString());
            params.append("ItemsPerPage", itemsPerPage.toString());

            const url = `https://localhost:7267/admin/main-categories?${params.toString()}`;

            const response = await fetch(url, {
                method: 'GET',
                headers: {
                    'Accept': '*/*',
                },
            });
            if (!response.ok) {
                throw new Error("Failed to fetch categories");
            }

            const result: MainCategoriesResult = await response.json();
            console.log(result);
            setCategories(result.data);
            setTotalPages(result.totalPages);
        } catch (error) {
            console.error("Error fetching categories:", error);
        } finally {
            setLoading(false);
        }
    }, [id, keyword, currentPage]);

    useEffect(() => {
        fetchCategories();
    }, [id, keyword, currentPage, setCurrentPage, fetchCategories]);

    return (
        <AdminPage>
            <div className="filter-table">
                <PageTitle title="Main Categories" description="Manage and organize your platform’s main categories efficiently." breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: "/admin" }, { label: "Main Categories" }]}/>
                <div className="d-flex flex-column">
                    <MainCategoriesFilter setId={setId} setKeyword={setKeyword} />
                    <ActionButton text={"CREATE A NEW CATEGORY"} onClick={handleAddModalVisbility} className="add-category-button" ariaLabel={"Add Main Category Button"} />
                </div>
                <AddMainCategoryModal title={"Main Category"} showModal={showAddModal} onClose={handleAddModalVisbility} onAddMainCategorySuccess={handleAddMainCategorySuccess} />
                {loading ? (
                    <div className="loading">Loading...</div>
                ) : (
                    <div className="categories-list">
                        <DataTable data={categories} columns={["id", "name", "description", "subCategories", "clicks", "createdOn"]} headers={tableHeaders}
                            renderActions={(row: MainCategoryRowModel) => (
                                <MainCategoryActions category={row} onEditSuccess={handleEditSuccess} onDeactivateSuccess={handleDeactivateSuccess} details={{
                                    name: row.name,
                                    description: row.description,
                                }} />
                            )}/>
                    </div>
                )}
            </div>
            <div className="pagination-container">
                <Pagination totalPages={totalPages} currentPage={currentPage} onPageChange={handlePageChange} />
            </div>
        </AdminPage>
    );
}
