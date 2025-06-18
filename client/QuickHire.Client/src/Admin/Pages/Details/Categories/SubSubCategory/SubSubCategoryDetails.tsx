import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Breadcrumb } from "../../../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { CategoryActions } from "../../Common/Actions/CategoryActions";
import { CategoryDetails } from "../../Common/CategoryDetails/CategoryDetails";
import { DeactivateSubSubCategoryModal, FilterItem } from "../../../../../Shared/Modals/Deactivate/SubSubCategory/DeactivateSubSubCategoryModal";
import { SubSubCategoriesTableSection } from "../../Common/Tables/GigFiltersTableSection";
import { EditSubSubCategoryModal } from "../../../../../Shared/Modals/Edit/SubSubCategory/EditSubSubCategoryModal";
import { SideNavigation } from "../../../../../Shared/PageItems/SideNavigation/SideNavigation";
import axios from "../../../../../axiosInstance";


export interface SubSubCategoryDetails {
    id: number;
    name: string;
    clicks: number;
    createdOn: string;
    gigFilters:  GigFilter[];
}

export interface GigFilter{
    id: number;
    title: string;
    items: FilterItem[];
}

export interface SubSubCategoriesInSubCategory {
    id: number;
    name: string;
}

export function SubSubCategoryDetails(){
    const { id } = useParams<{ id: string }>();
    const parsedId = id ? parseInt(id, 10) : NaN;

    const [details, setDetails] = useState<SubSubCategoryDetails | null>(null);
    const [showEditSubSubCategoryModal, setShowEditSubSubCategoryModal] = useState(false);
    const [showDeactivateSubSubCategoryModal, setShowDeactivateSubSubCategoryModal] = useState(false);
    const userNavigate = useNavigate();  
    const [view, setView] = useState<"details" | "filters">("details");
    

    const handleEditSubSubCategoryModalVisibility = () => setShowEditSubSubCategoryModal(!showEditSubSubCategoryModal);
    const handleDeactivateSubCategoryModalVisibility = () => setShowDeactivateSubSubCategoryModal(!showDeactivateSubSubCategoryModal);
    const handleDeactivateSubSubCategorySuccess = () => {
        setDetails(null);
        userNavigate("/admin/sub-sub-categories");
    }   

    const handleEditSubSubCategorySuccess = (id: number, name: string) => {
        if (!details) return;
        setDetails({
            ...details,
            id,
            name
        });
    }

    const handleOnAddGigFilterSuccess = (newGigFilter: GigFilter) => {
        if (!details) return;
        setDetails({
            ...details,
            gigFilters: [...details.gigFilters, newGigFilter]
        });
    }

   const handleEditGigFilterSuccess = (id: number, newTitle: string) => {
  if (!details || !details.gigFilters) return;

  const updatedGigFilters = details.gigFilters.map(filter =>
    filter.id === id ? { ...filter, title: newTitle } : filter
  );

  setDetails(prevDetails => ({
    ...prevDetails!,
    gigFilters: updatedGigFilters,
  }));
};

const onDeactivateGigFilterSuccess = (id: number) => {
  if (!details || !details.gigFilters) return;

  const updatedGigFilters = details.gigFilters.filter(filter => filter.id !== id);

  setDetails(prevDetails => ({
    ...prevDetails!,
    gigFilters: updatedGigFilters,
  }));
};

    const handleDeactivateFilterOptionsSuccess = (id: number) => {
        if (!details) return;
        const updatedGigFilters = details.gigFilters.map(filter => {
            return {
                ...filter,
                items: filter.items.filter(item => item.id !== id)
            };
        });
        setDetails({
            ...details,
            gigFilters: updatedGigFilters
        });
    }

    const handleEditFilterOptionsSuccess = (id: number, newValue: string) => {
        if (!details) return;
        const updatedGigFilters = details.gigFilters.map(filter => {
            return {
                ...filter,
                items: filter.items.map(item => 
                    item.id === id ? { ...item, value: newValue } : item
                )
            };
        });
        setDetails({
            ...details,
            gigFilters: updatedGigFilters
        });
    }

     useEffect(() => {    
        setView("details");
            const fetchSubCategoryDetails = async () => {    
                try {
                    const url = `https://localhost:7267/admin/sub-sub-categories/${parsedId}`;
                    const response = await axios.get<SubSubCategoryDetails>(url);
                    const data = response.data;
                    setDetails(data);
                } catch (error) {
                    console.error("Error fetching sub sub category details:", error);
                } 
            };
    
            fetchSubCategoryDetails();
        }, [parsedId]);
    return (
        <>
            <div className="main-category-details d-flex flex-row">
                <div className="breadcrumb-sidenav" style={{marginRight: "30px", width: '10%'}}>
                    <Breadcrumb items={[{ label:<i className="bi bi-house-door"></i>}, { label: "Sub sub categories", to: "/admin/sub-sub-categories" }]}/>
                    <SideNavigation active={view} items={[{ label: "Details", onClick: () => setView("details"), value: 'details'}, { label: "Filters", onClick: () => setView("filters") , value: 'filters'}]} />
                </div>
           
            {view === "details" && details && (
                <div className="d-flex flex-row" style={{ marginTop: "20px"}}>
                    <CategoryDetails details={details} showFAQ={false} faqMainCategoryId={parseInt(id || "0", 10)}/>
                    <CategoryActions onEditModalVisibility={handleEditSubSubCategoryModalVisibility} onDeactivateModalVisibility={handleDeactivateSubCategoryModalVisibility}/>
                    <DeactivateSubSubCategoryModal showModal={showDeactivateSubSubCategoryModal} onClose={handleDeactivateSubCategoryModalVisibility} id={parsedId} onDeactivateSuccess={handleDeactivateSubSubCategorySuccess}/>
                </div>
            )}
            {view === "filters" &&  details &&
                <SubSubCategoriesTableSection items={details.gigFilters} onAddGigFilterSuccess={handleOnAddGigFilterSuccess} onEditGigFilterSuccess={handleEditGigFilterSuccess} onDeactivateGigFilterSuccess={onDeactivateGigFilterSuccess} onEditFilterOptionSuccess={handleEditFilterOptionsSuccess} onDeactivateFilterOptionSuccess={handleDeactivateFilterOptionsSuccess}/>                       
            }
        
           </div>
           {showEditSubSubCategoryModal && <EditSubSubCategoryModal id={parsedId} showModal={true} onClose={handleEditSubSubCategoryModalVisibility} onEditSuccess={handleEditSubSubCategorySuccess} name={details?.name ?? ""}/>}
        </>
    );
}