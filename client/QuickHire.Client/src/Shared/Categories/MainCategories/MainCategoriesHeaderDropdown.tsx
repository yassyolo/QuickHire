import axios from "axios";
import { useEffect, useState } from "react";
import { MainCategoriesHeaderDropdownItem } from "./MainCategoriesHeaderDropdownItem";
import './MainCategoriesHeaderDropdown.css';
interface MainCategoriesHeaderDropdownProps {
    id: number;
    show: boolean;
}

export interface SubCategoryInMainCategory {
    id: number;
    name: string;
    subSubCategories: SubSubCategoryInSubCategory[] | undefined;
}

export interface SubSubCategoryInSubCategory{
    id: number;
    name: string;
}

export function MainCategoriesHeaderDropdown({ id, show }: MainCategoriesHeaderDropdownProps) {
    const [subCategories, setSubCategories] = useState<SubCategoryInMainCategory[]>([]);
    const fetchSubCategories = async () => {
        try {
            const url = `https://localhost:7267/admin/sub-categories/header/${id}`;
            const response = await axios.get<SubCategoryInMainCategory[]>(url);
            setSubCategories(response.data);
        } catch (error) {
            console.error("Error fetching sub-categories:", error);
        }
    };

    useEffect(() => {
        if (show) {
            fetchSubCategories();
        }
    }, [show, id]);

    if (!show) {
        return null;
    }
    return (
        <div className="main-categories-header-dropdown">
            <div className="sub-categories-list">
                {subCategories.map((subCategory) => (
                    <MainCategoriesHeaderDropdownItem key={subCategory.id}
                        data={subCategory} 
                        />
                ))}
            </div>
           
        </div>
    );
}