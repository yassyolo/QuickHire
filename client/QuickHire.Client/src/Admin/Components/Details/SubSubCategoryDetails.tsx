import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { DeactivateMainCategoryModal } from "../Modals/Deactivate/DeactivateMainCategoryModal";
import { Breadcrumb } from "../../../Shared/Breadcrumb/Breadcrumb";
import { CategoryActions } from "./Common/CategoryActions";
import { CategoryDetails } from "./Common/CategoryDetails";
import { AdminPage } from "../../Pages/Common/AdminPage";

export interface SubSubCategoryDetails {
    id: number;
    name: string;
    clicks: number;
    createdOn: string;
    subCategoryName: string;
    subSubCategories?: SubSubCategoriesInSubCategory[];
}

export interface SubSubCategoriesInSubCategory {
    id: number;
    name: string;
}

export function SubSubCategoryDetails(){
    const { id } = useParams<{ id: string }>();
    const parsedId = id ? parseInt(id, 10) : NaN;

    const [details, setDetails] = useState<SubSubCategoryDetails | null>(null);
    const [showEditSubSubCategoryModal, setShowEditSubCategoryModal] = useState(false);
    const [showDeactivateSubSubCategoryModal, setShowDeactivateSubCategoryModal] = useState(false);
    const userNavigate = useNavigate();  

    useEffect(() => {
        const mockData: SubSubCategoryDetails = {
            id: parsedId,
            name: "Mock Category",
            subCategoryName: "Main Category",
            clicks: 123,
            createdOn: "2025-01-01",
            subSubCategories: [
                { id: 1, name: "Subcat 1" },
                { id: 2, name: "Subcat 2" }
            ]
        };
    
        setDetails(mockData);
    }, [parsedId]);

    

    const handleEditSubCategoryModalVisibility = () => setShowEditSubCategoryModal(!showEditSubSubCategoryModal);
    const handleDeactivateSubCategoryModalVisibility = () => setShowDeactivateSubCategoryModal(!showDeactivateSubSubCategoryModal);
    //const handleEditSubCategorySucess = (id: number, newName: string) => setDetails(prev => prev ? { ...prev, name: newName} : null);
    const handleDeactivateSubCategorySuccess = () => {
        setDetails(null);
        userNavigate("/admin/sub-sub-categories");
    }   

    /*const fetchDetails = async () => {
        try {
            const response = await fetch(`/api/main-category/${parsedId}`);
            const data: MainCategoryDetails = await response.json();
            setDetails(data);
            setSubCategories(data.subCategories || []);
        } catch (error) {
            console.error("Error fetching main category details:", error);
        }
    };*/
    return (
        <AdminPage>
            <div className="main-category-details">
            <Breadcrumb items={[{ label:<i className="bi bi-house-door"></i>}, { label: "Sub Categories", to: "/admin/sub-categories" }]}/>
            {details && (
                <><div className="d-flex flex-row">
                    <CategoryDetails details={details} showFAQ={false} faqMainCategoryId={parseInt(id || "0", 10)}/>                   
                    <CategoryActions onEditModalVisibility={handleEditSubCategoryModalVisibility} onDeactivateModalVisibility={handleDeactivateSubCategoryModalVisibility}/>   
                    <DeactivateMainCategoryModal showModal={showDeactivateSubSubCategoryModal} onClose={handleDeactivateSubCategoryModalVisibility} id={parsedId} onDeactivateSuccess={handleDeactivateSubCategorySuccess}/>         
                </div>                         

                </> 
            )}
        </div>
        </AdminPage>
    );
}