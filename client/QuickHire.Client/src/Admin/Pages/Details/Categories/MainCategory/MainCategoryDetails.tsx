import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import "../../Gigs/GigDetailsPage.css";
import { Breadcrumb } from "../../../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { SideNavigation } from "../../../../../Shared/PageItems/SideNavigation/SideNavigation";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import { EditMainCategoryModal } from "../../../../../Shared/Modals/Edit/MainCategory/EditMainCategoryModal";
import { DeactivateMainCategoryModal } from "../../../../../Shared/Modals/Deactivate/MainCategory/DeactivateMainCategoryModal";
import { EditSubCategoryModal } from "../../../../../Shared/Modals/Edit/SubCategory/EditSubCategoryModal";
import { DeactivateSubCategoryModal } from "../../../../../Shared/Modals/Deactivate/SubCategory/DeactivateSubCategoryModal";
import { AddSubCategoryModal } from "../../../../../Shared/Modals/Add/SubCategory/AddSubCategoryModal";
import { CategoryActions } from "../../Common/Actions/CategoryActions";
import { CategoryDetails } from "../../Common/CategoryDetails/CategoryDetails";
import { SubCategoriesTableSection } from "../../Common/Tables/SubCategoriesTableSection";
import axios from "../../../../../axiosInstance";


export interface MainCategoryDetails {
  id: number;
  name: string;
  description: string;
  clicks: number;
  createdOn: string;
  subCategories?: SubCategoriesInMainCategory[];
}

export interface SubCategoriesInMainCategory {
  id: number;
  name: string;
  imageUrl: string;
}

export function MainCategoryDetails() {
  const { id } = useParams<{ id: string }>();
  const parsedId = id ? parseInt(id, 10) : null;
  const navigate = useNavigate();

  const [details, setDetails] = useState<MainCategoryDetails | null>(null);
  const [subCategories, setSubCategories] = useState<SubCategoriesInMainCategory[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  const [view, setView] = useState<"details" | "subcategories">("details");

  const [showEditMainModal, setShowEditMainModal] = useState(false);
  const [showDeactivateMainModal, setShowDeactivateMainModal] = useState(false);
  const [showAddSubModal, setShowAddSubModal] = useState(false);
  const [editSubCategoryId, setEditSubCategoryId] = useState<number | null>(null);
  const [deactivateSubCategoryId, setDeactivateSubCategoryId] = useState<number | null>(null);

  useEffect(() => {
    console.log("chage selected id", editSubCategoryId);
  }, [editSubCategoryId]);

  useEffect(() => {
    if (!id) return;

    const fetchCategoryDetails = async () => {
      setIsLoading(true);
      setDetails(null);
      setSubCategories([]);
      try {
        const parsedId = parseInt(id, 10);
        const url = `https://localhost:7267/admin/main-categories/${parsedId}`;
        const response = await axios.get<MainCategoryDetails>(url);
        const data = response.data;
        
        setDetails(data);
        setSubCategories(data.subCategories || []);
      } catch (err) {
        console.error("Error loading main category:", err);
      } finally {
        setIsLoading(false);
      }
    };

    fetchCategoryDetails();
  }, [id]);

  const handleEditMainSuccess = (id: number, name: string, description: string) => {
    setDetails(prev => prev ? { ...prev, id, name, description } : null);
  };

  const handleDeactivateMainSuccess = () => navigate("/admin/main-categories");

  const handleAddSubCategorySuccess = (newSub: SubCategoriesInMainCategory) => {
    setSubCategories(prev => [...prev, newSub]);
    setDetails(prev => prev ? { ...prev, subCategories: [...(prev.subCategories || []), newSub] } : null);
  };

  const handleEditSubCategorySuccess = (id: number, newName: string, newImage: string) => 
  {
    setSubCategories(prev => prev.map(sc => sc.id === id ? { ...sc, name: newName, imageUrl: newImage } : sc));
    setDetails(prev => prev ? {
      ...prev,
      subCategories: prev.subCategories?.map(sc => sc.id === id ? { ...sc, name: newName, imageUrl: newImage } : sc)
    } : null);
  };

  const handleDeactivateSubCategorySuccess = (id: number) => {
    setSubCategories(prev => prev.filter(sc => sc.id !== id));
    setDetails(prev => prev ? {
      ...prev,
      subCategories: prev.subCategories?.filter(sc => sc.id !== id)
    } : null);
  };

  const handleSeeSubCategory = (id: number) => navigate(`/admin/sub-categories/${id}`);

  return (
      <div className="category-details-page d-flex flex-row" style={{ height: "100%" }}>
        <div className="breadcrumb-side-nav" style={{marginRight: "30px", width: '10%'}}>
          <Breadcrumb items={[ { label: <i className="bi bi-house-door" /> }, { label: "Main categories", to: "/admin/main-categories" }
          ]} />
<SideNavigation
  active={view}
  items={[
    { label: "Details", onClick: () => setView("details"), value: "details" },
    { label: "Sub categories", onClick: () => setView("subcategories"), value: "subcategories" }
  ]}
/>        </div>

        {isLoading && <div>Loading...</div>}

        {!isLoading && details && view === "details" && (
          <div className="d-flex flex-row" style={{marginTop: "20px", width: '100%'}}>
            <CategoryDetails details={details} showFAQ={true} faqMainCategoryId={details.id} />
            <CategoryActions onEditModalVisibility={() => setShowEditMainModal(true)} onDeactivateModalVisibility={() => setShowDeactivateMainModal(true)}/>

            <EditMainCategoryModal
              showModal={showEditMainModal} onClose={() => setShowEditMainModal(false)} id={details.id} onEditSuccess={handleEditMainSuccess}
              initialName={details.name} initialDescription={details.description}
            />

            <DeactivateMainCategoryModal
              showModal={showDeactivateMainModal} onClose={() => setShowDeactivateMainModal(false)} id={details.id}
              onDeactivateSuccess={handleDeactivateMainSuccess}
            />
          </div>
        )}

        {!isLoading && view === "subcategories" && (
          <SubCategoriesTableSection
            title="Sub categories" addButtonLabel="CREATE A NEW CATEGORY" items={subCategories}
            onAddClick={() => setShowAddSubModal(true)}
            addModal={
              parsedId !== null ? (
                <AddSubCategoryModal
                  showModal={showAddSubModal}
                  onClose={() => setShowAddSubModal(false)}
                  onAddSubCategorySuccess={handleAddSubCategorySuccess}
                  showCategoriesPopulate={false}
                  submitedMainCategoryId={parsedId}
                />
              ) : null
            }
            renderActions={(item) => (
              <>
                <IconButton icon={<i className="bi bi-eye" style={{ fontSize: 20, color: "#1DBF73" }} />}
                    onClick={() => handleSeeSubCategory(item.id)} ariaLabel="View SubCategory" className={"faq-delete-button"} />
                <IconButton icon={<i className="bi bi-x" style={{ fontSize: 20, color: "red" }} />}
                    onClick={() => setDeactivateSubCategoryId(item.id)} ariaLabel="Deactivate SubCategory" className={"faq-delete-button"} />
                <IconButton icon={<i className="bi bi-pencil" style={{ fontSize: 18 }} />}
                    onClick={() => setEditSubCategoryId(item.id)} ariaLabel="Edit SubCategory" className={"faq-delete-button"} />
              </>
            )}
            renderModals={(item) => (
              <>
                {editSubCategoryId === item.id && (
                  <EditSubCategoryModal showModal={true}
                            onClose={() => setEditSubCategoryId(null)}
                            id={item.id}
                            onEditSuccess={handleEditSubCategorySuccess} name={item.name ?? ""} imageUrl={item.imageUrl ?? ""} />
                )}
                {deactivateSubCategoryId === item.id && (
                  <DeactivateSubCategoryModal
                    showModal={true}
                    onClose={() => setDeactivateSubCategoryId(null)} id={item.id}
                    onDeactivateSuccess={handleDeactivateSubCategorySuccess}
                  />
                )}
              </>
            )}
          />
        )}
      </div>
  );
}
