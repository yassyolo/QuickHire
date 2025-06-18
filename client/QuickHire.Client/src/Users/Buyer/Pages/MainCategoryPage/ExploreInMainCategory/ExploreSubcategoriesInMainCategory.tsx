import { useEffect, useState } from "react";
import './ExploreSubcategoriesInMainCategory.css';
import { Link } from "react-router-dom";
import axios from "../../../../../axiosInstance";

interface ExploreSubcategoriesInMainCategoryProps {
    mainCategoryId: number;
    mainCategoryName: string; 
}

interface Subcategory {
    id: number;
    name: string;
    imageUrl: string;
    subSubCategories: SubSubCategory[];
}

interface SubSubCategory{
    id: number;
    name: string;
}

export function ExploreSubcategoriesInMainCategory({ mainCategoryId, mainCategoryName }: ExploreSubcategoriesInMainCategoryProps) {
    const [subcategories, setSubcategories] = useState<Subcategory[]>([]);

    useEffect(() => {
        const fetchSubcategories = async () => {
            try {
                const url = `https://localhost:7267/admin/sub-categories-in-main-category/${mainCategoryId}`;
                const response = await axios.get<Subcategory[]>(url);
                const data = response.data.map(subcategory => ({
                    ...subcategory,
                    subSubCategories: subcategory.subSubCategories || [] 
                }));

                setSubcategories(data);
            } catch (error) {
                console.error("Error fetching subcategories:", error);
            }
        };

        fetchSubcategories();
    }, [mainCategoryId]);

    return (
        <div className="explore-subcategories">
            <div className="explore-subcategories-title">Explore {mainCategoryName}</div>
            <ul className="explore-subcategories-list">
                {subcategories.map(subcategory => (
                    <li key={subcategory.id} className="explore-subcategories-item" >
                        <img src={subcategory.imageUrl} alt={subcategory.name} className="explore-subcategories-item-image" />
                        <div className="explore-subcategories-item-name">{subcategory.name}</div>
                        <ul className="explore-subcategories-subsubcategories">
                            {subcategory.subSubCategories.map(subSubCategory => (
                                <li key={subSubCategory.id}>
                                    <Link className="itemm" to={`/buyer/sub-sub-categories/${subSubCategory.id}`} style={{textDecoration:'none'}}>{subSubCategory.name}</Link>
                                </li>
                            ))}
                        </ul>
                    </li>
                ))}
            </ul>
        </div>
    );
}