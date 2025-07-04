import { Link } from "react-router-dom";
import "./MainCategoriesHeaderDropdownItem.css";
import { SubCategoryInMainCategory } from "../MainCategoriesHeaderDropdown";

interface MainCategoriesHeaderDropdownItemProps {
    data: SubCategoryInMainCategory;
}

export function MainCategoriesHeaderDropdownItem({ data }: MainCategoriesHeaderDropdownItemProps) {
    return (
        <div className="main-categories-header-dropdown-item">
                <div key={data.id} className="name-and-subsubcategory-list">
                    <div className="item-name">{data.name}</div>
                    {data.subSubCategories && data.subSubCategories.length > 0 && (
                        <ul className="main-categories-header-dropdown-item-subsubcategory-list">
                            {data.subSubCategories.map((subSubCategory) => (
                                <Link key={subSubCategory.id} className="main-categories-header-dropdown-item-subsubcategory" to={`sub-sub-categories/${subSubCategory.id}`}>{subSubCategory.name}</Link>
                            ))}
                        </ul>
                    )}
                </div>
        </div>
    );
}

