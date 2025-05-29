import { Link } from "react-router-dom";
import { SubCategoryInMainCategory } from "./MainCategoriesHeaderDropdown";
import "./MainCategoriesHeaderDropdownItem.css";

interface MainCategoriesHeaderDropdownItemProps {
    data: SubCategoryInMainCategory;
}

export function MainCategoriesHeaderDropdownItem({ data }: MainCategoriesHeaderDropdownItemProps) {
    return (
        <div className="main-categories-header-dropdown-item">
                <div key={data.id} className="name-and-subsubcategory-list">
                    <Link to={`/buyer/sub-categories/${data.id}`} className="item-name">{data.name}</Link>
                    {data.subSubCategories && data.subSubCategories.length > 0 && (
                        <ul className="main-categories-header-dropdown-item-subsubcategory-list">
                            {data.subSubCategories.map((subSubCategory) => (
                                <li key={subSubCategory.id} className="main-categories-header-dropdown-item-subsubcategory">{subSubCategory.name}</li>
                            ))}
                        </ul>
                    )}
                </div>
        </div>
    );
}

