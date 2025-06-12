import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { EditSubCategoryModal } from "../../../../Components/Modals/Edit/EditSubCategoryModal";
import { Breadcrumb } from "../../../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { EditSubSubCategoryModal } from "../../../../Components/Modals/Edit/EditSubSubCategoryModal";
import { DeactivateSubSubCategoryModal } from "../../../../Components/Modals/Deactivate/SubSubCategory/DeactivateSubSubCategoryModal";
import { AddSubSubCategoryModal } from "../../../../Components/Modals/Add/SubSubCategory/AddSubSubCategoryModal";
import { CategoryActions } from "../../Common/Actions/CategoryActions";
import { CategoryDetails } from "../../Common/CategoryDetails/CategoryDetails";
import { SubCategoriesTableSection } from "../../Common/Tables/SubCategoriesTableSection";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import { DeactivateSubCategoryModal } from "../../../../Components/Modals/Deactivate/SubCategory/DeactivateSubCategoryModal";
import { SideNavigation } from "../../../../../Shared/PageItems/SideNavigation/SideNavigation";
import axios from "../../../../../axiosInstance";

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
      const [view, setView] = useState<"details" | "subSubCategories">("details");


    const [showAddSubSubCategoryModal, setShowAddSubSubCategoryModal] = useState(false);
    const [showEditSubCategoryModal, setShowEditSubCategoryModal] = useState(false);
    const [showDeactivateSubCategoryModal, setShowDeactivateSubCategoryModal] = useState(false);
    const [deactivateSubSubCategoryId, setDeactivateSubSubCategoryId] = useState<number | null>(null);
    const [editSubSubCategoryId, setEditSubSubCategoryId] = useState<number | null>(null);
    const userNavigate = useNavigate();

    useEffect(() => {
        if (isNaN(parsedId)) return;
        setView("details");

        const fetchSubCategoryDetails = async () => {
            setIsLoading(true);
            setDetails(null);
            setSubSubCategories([]);

            try {
                const url = `https://localhost:7267/admin/sub-categories/${parsedId}`;
                const response = await axios.get<SubCategoryDetails>(url);
                const data = response.data;
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

    const handleEditSubCategoryModalVisibility = () => setShowEditSubCategoryModal(!showEditSubCategoryModal);
    const handleDeactivateSubCategoryModalVisibility = () => setShowDeactivateSubCategoryModal(!showDeactivateSubCategoryModal);

    const handleEditSubCategorySucess = (id: number, newName: string, newImageUrl: string | undefined) =>
        setDetails(prev => prev ? { ...prev, name: newName, imageUrl: newImageUrl } : null);

    const handleDeactivateSubCategorySuccess = () => {
        setDetails(null);
        setSubSubCategories([]);
        userNavigate("/admin/sub-categories");
    };

    const handleAddSubSubCategoryModalVisibility = () => setShowAddSubSubCategoryModal(!showAddSubSubCategoryModal);
    const handleAddSubSubCategorySuccess = (id: number, name: string) => {
        setSubSubCategories(prev => [...prev, { id, name }]);
        setDetails(prev => prev ? { ...prev, subSubCategories: [...(prev.subSubCategories || []), { id, name }] } : null);
        setShowAddSubSubCategoryModal(false);
    }

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
            <div className="main-category-details d-flex flex-row">
                <div className="breadcrumb-side-nav" style={{marginRight: "30px", width: '10%'}}>
                <Breadcrumb items={[
                    { label: <i className="bi bi-house-door"></i> },
                    { label: "Sub categories", to: "/admin/sub-categories" }
                ]} />
                <SideNavigation
                    active={view}
                    items={[
                        { label: "Details", onClick: () => setView("details"), value: "details" },
                        { label: "Sub sub categories", onClick: () => setView("subSubCategories"), value: "subSubCategories" }
                    ]}
                />
               
                </div>

                {isLoading && <div>Loading...</div>}

                {view === "details" && details &&(
                    <div className="d-flex flex-row" style={{ marginTop: "20px"}}>
                        <CategoryDetails details={details} showFAQ={false} faqMainCategoryId={parsedId} />
                        <CategoryActions onEditModalVisibility={handleEditSubCategoryModalVisibility}
                            onDeactivateModalVisibility={handleDeactivateSubCategoryModalVisibility}
                        />
                        {showEditSubCategoryModal && <EditSubCategoryModal
                            showModal={true}
                            onClose={handleEditSubCategoryModalVisibility} id={parsedId}
                            onEditSuccess={handleEditSubCategorySucess}
                            name={details?.name ?? ""}
                            imageUrl={details?.imageUrl ?? ""}
                        />}
                        <DeactivateSubCategoryModal showModal={showDeactivateSubCategoryModal}
                            onClose={handleDeactivateSubCategoryModalVisibility}
                            id={parsedId}
                            onDeactivateSuccess={handleDeactivateSubCategorySuccess}
                        />
                    </div>
                )}

                {view === "subSubCategories"  && details && (
                    <>

                        <SubCategoriesTableSection title="Sub Sub Categories"
                            addButtonLabel="CREATE A NEW CATEGORY"
                            items={subSubCategories}
                            onAddClick={handleAddSubSubCategoryModalVisibility}
                            addModal={
                                <AddSubSubCategoryModal
                                    showModal={showAddSubSubCategoryModal}
                                    onClose={handleAddSubSubCategoryModalVisibility}
                                    onAddSubSubCategorySuccess={handleAddSubSubCategorySuccess}
                                    title="Sub Sub Category" showCategoriesPopulate={false} submitedSubCategoryId={parsedId}                                />
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
    );
}
