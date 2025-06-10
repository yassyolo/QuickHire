import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { Breadcrumb } from "../../../Shared/Breadcrumb/Breadcrumb";
import { CategoryActions } from "./Common/CategoryActions";
import { CategoryDetails } from "./Common/CategoryDetails";
import { DeactivateSubSubCategoryModal, FilterItem } from "../Modals/Deactivate/DeactivateSubSubCategoryModal";
import { SubSubCategoriesTableSection } from "./Common/GigFiltersTableSection";
import { EditSubSubCategoryModal } from "../Modals/Edit/EditSubSubCategoryModal";
import { SideNavigation } from "../../../Shared/SideNavigation/SideNavigation";

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
            const fetchSubCategoryDetails = async () => {    
                try {
                    const response = await fetch(`https://localhost:7267/admin/sub-sub-categories/${parsedId}`);
                    if (!response.ok) throw new Error("Failed to fetch sub category details");
    
                    const data: SubSubCategoryDetails = await response.json();
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
                <div className="breadcrumb-sidenav" style={{marginRight: "30px"}}>
                     <Breadcrumb items={[{ label:<i className="bi bi-house-door"></i>}, { label: "Sub sub categories", to: "/admin/sub-sub-categories" }]}/>
                            <SideNavigation items={[{ label: "Details", onClick: () => setView("details") }, { label: "Filters", onClick: () => setView("filters") }]} />
                </div>
           
            {view === "details" && details && (
                <div className="d-flex flex-row">
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