import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { EditSubCategoryModal } from "../Modals/Edit/EditSubCategoryModal";
import { Breadcrumb } from "../../../Shared/Breadcrumb/Breadcrumb";
import { EditSubSubCategoryModal } from "../Modals/Edit/EditSubSubCategoryModal";
import { DeactivateSubSubCategoryModal } from "../Modals/Deactivate/DeactivateSubSubCategoryModal";
import { AddSubSubCategoryModal } from "../Modals/Add/AddSubSubCategoryModal";
import { CategoryActions } from "./Common/CategoryActions";
import { CategoryDetails } from "./Common/CategoryDetails";
import { SubCategoriesTableSection } from "./Common/SubCategoriesTableSection";
import { IconButton } from "../../../Shared/Buttons/IconButton";
import { AdminPage } from "../../Pages/Common/AdminPage";
import { DeactivateSubCategoryModal } from "../Modals/Deactivate/DeactivateSubCategoryModal";

export interface SubCategoryDetails {
    id: number;
    name: string;
    imageUrl: string | undefined;
    clicks: number;
    createdOn: string;
    mainCategoryName: string;
    subSubCategories?: SubSubCategoriesInSubCategory[];
}

export interface SubSubCategoriesInSubCategory {
    id: number;
    name: string;
}

export function SubCategoryDetails() {
    const { id } = useParams<{ id: string }>();
    const parsedId = id ? parseInt(id, 10) : NaN;

    const [details, setDetails] = useState<SubCategoryDetails | null>(null);
    const [subSubCategories, setSubSubCategories] = useState<SubSubCategoriesInSubCategory[]>([]);
    const [isLoading, setIsLoading] = useState(false);

    const [showAddSubSubCategoryModal, setShowAddSubSubCategoryModal] = useState(false);
    const [showEditSubCategoryModal, setShowEditSubCategoryModal] = useState(false);
    const [showDeactivateSubCategoryModal, setShowDeactivateSubCategoryModal] = useState(false);
    const [deactivateSubSubCategoryId, setDeactivateSubSubCategoryId] = useState<number | null>(null);
    const [editSubSubCategoryId, setEditSubSubCategoryId] = useState<number | null>(null);
    const userNavigate = useNavigate();

    // FETCH LOGIC (updated like in MainCategoryDetails)
    useEffect(() => {
        if (isNaN(parsedId)) return;

        const fetchSubCategoryDetails = async () => {
            setIsLoading(true);
            setDetails(null);
            setSubSubCategories([]);

            try {
                const response = await fetch(`https://localhost:7267/admin/sub-categories/${parsedId}`);
                if (!response.ok) throw new Error("Failed to fetch sub category details");

                const data: SubCategoryDetails = await response.json();
                setDetails(data);
                setSubSubCategories(data.subSubCategories || []);
            } catch (error) {
                console.error("Error fetching sub category details:", error);
            } finally {
                setIsLoading(false);
            }
        };

        fetchSubCategoryDetails();
    }, [parsedId]);

    const handleEditSubCategoryModalVisibility = () => setShowEditSubCategoryModal(prev => !prev);
    const handleDeactivateSubCategoryModalVisibility = () => setShowDeactivateSubCategoryModal(prev => !prev);

    const handleEditSubCategorySucess = (id: number, newName: string, newImageUrl: string | undefined) =>
        setDetails(prev => prev ? { ...prev, name: newName, imageUrl: newImageUrl } : null);

    const handleDeactivateSubCategorySuccess = () => {
        setDetails(null);
        setSubSubCategories([]);
        userNavigate("/admin/sub-categories");
    };

    const handleAddSubSubCategoryModalVisibility = () => setShowAddSubSubCategoryModal(prev => !prev);
    const handleAddSubSubCategorySuccess = () => {
        // trigger re-fetch or push new item
    };

    const handleEditSubSubCategoryButtonClick = (id: number) => setEditSubSubCategoryId(id);
    const handleEditSubSubCategoryModalClose = () => setEditSubSubCategoryId(null);

    const handleEditSubSubCategoySuccess = (id: number, newName: string) => {
        setSubSubCategories(prev => prev.map(x => x.id === id ? { ...x, name: newName } : x));
    };

    const handleDeactivateSubSubCategoryButtonClick = (id: number) => setDeactivateSubSubCategoryId(id);
    const handleDeactivateSubSubCategoryModalClose = () => setDeactivateSubSubCategoryId(null);

    const handleSeeSubSubCategoryButtonClick = (id: number) => userNavigate(`/admin/sub-sub-categories/${id}`);
    const handleDeactivateSubSubCategorySuccess = (id: number) => {
        setSubSubCategories(prev => prev.filter(cat => cat.id !== id));
    };

    return (
        <AdminPage>
            <div className="main-category-details">
                <Breadcrumb items={[
                    { label: <i className="bi bi-house-door"></i> },
                    { label: "Sub Categories", to: "/admin/sub-categories" }
                ]} />

                {isLoading && <div>Loading...</div>}

                {!isLoading && details && (
                    <>
                        <div className="d-flex flex-row">
                            <CategoryDetails details={details} showFAQ={false} faqMainCategoryId={parsedId} />
                            <CategoryActions onEditModalVisibility={handleEditSubCategoryModalVisibility}
                                onDeactivateModalVisibility={handleDeactivateSubCategoryModalVisibility}
                            />
                            <EditSubCategoryModal
                                showModal={showEditSubCategoryModal}
                                onClose={handleEditSubCategoryModalVisibility} id={parsedId}
                                onEditSuccess={handleEditSubCategorySucess}
                                name={details.name}
                                imageUrl={details.imageUrl ?? ""}
                            />
                            <DeactivateSubCategoryModal showModal={showDeactivateSubCategoryModal}
                                onClose={handleDeactivateSubCategoryModalVisibility}
                                id={parsedId}
                                onDeactivateSuccess={handleDeactivateSubCategorySuccess}
                            />
                        </div>

                        <SubCategoriesTableSection title="Sub Sub Categories"
                            addButtonLabel="CREATE A NEW CATEGORY"
                            items={subSubCategories}
                            onAddClick={handleAddSubSubCategoryModalVisibility}
                            addModal={
                                <AddSubSubCategoryModal
                                    showModal={showAddSubSubCategoryModal}
                                    onClose={handleAddSubSubCategoryModalVisibility}
                                    onAddSubSubCategorySuccess={handleAddSubSubCategorySuccess}
                                    title="Sub Sub Category"
                                />
                            }
                            renderActions={(item) => (
                                <>
                                    <IconButton
                                        icon={<i className="bi bi-eye" style={{ fontSize: "20px", color: "#1DBF73" }} />}
                                        onClick={() => handleSeeSubSubCategoryButtonClick(item.id)}
                                        className="faq-delete-button"
                                        ariaLabel="See Sub Category Button"
                                    />
                                    <IconButton
                                        icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }} />}
                                        onClick={() => handleDeactivateSubSubCategoryButtonClick(item.id)}
                                        className="faq-delete-button"
                                        ariaLabel="Deactivate Sub Sub Category Button"
                                    />
                                    <IconButton
                                        icon={<i className="bi bi-pencil" style={{ fontSize: "18px" }} />}
                                        onClick={() => handleEditSubSubCategoryButtonClick(item.id)}
                                        className="faq-edit-button"
                                        ariaLabel="Edit Sub Sub Category Button"
                                    />
                                </>
                            )}
                            renderModals={(item) => (
                                <>
                                    {editSubSubCategoryId === item.id && (
                                        <EditSubSubCategoryModal
                                            showModal={true}
                                            onClose={handleEditSubSubCategoryModalClose}
                                            id={item.id}
                                            onEditSuccess={handleEditSubSubCategoySuccess}
                                            name={item.name}
                                        />
                                    )}
                                    {deactivateSubSubCategoryId === item.id && (
                                        <DeactivateSubSubCategoryModal
                                            showModal={true}
                                            onClose={handleDeactivateSubSubCategoryModalClose}
                                            id={item.id}
                                            onDeactivateSuccess={handleDeactivateSubSubCategorySuccess}
                                        />
                                    )}
                                </>
                            )}
                        />
                    </>
                )}
            </div>
        </AdminPage>
    );
}
