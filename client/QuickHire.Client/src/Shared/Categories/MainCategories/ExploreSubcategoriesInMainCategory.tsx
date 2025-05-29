import { useEffect, useState } from "react";
import './ExploreSubcategoriesInMainCategory.css';

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
                const response = await fetch(`https://localhost:7267/admin/sub-categories-in-main-category/${mainCategoryId}`);
                const data: Subcategory[] = await response.json();
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
                    <li key={subcategory.id} >
                        <img src={subcategory.imageUrl} alt={subcategory.name} className="explore-subcategories-item-image" />
                        <span>{subcategory.name}</span>
                        <ul>
                            {subcategory.subSubCategories.map(subSubCategory => (
                                <li key={subSubCategory.id}>
                                    <span>{subSubCategory.name}</span>
                                </li>
                            ))}
                        </ul>
                    </li>
                ))}
            </ul>
        </div>
    );
}