import React from "react";
import { MostPopularInMainCategoryItem } from "./MostPopularInMainCategoryItem";
import './MostPopularInMainCategory.css';

interface MostPopularInMainCategoryProps {
    mainCategoryName: string;
    mainCategoryId: number;
}

export interface MostPopularInMainCategoryItems {
    id: number;
    name: string;
    imageUrl: string;
}

export function MostPopularInMainCategory({ mainCategoryId, mainCategoryName }: MostPopularInMainCategoryProps) {
    const [mostPopularItems, setMostPopularItems] = React.useState<MostPopularInMainCategoryItems[]>([]);

    React.useEffect(() => {
        const fetchMostPopularItems = async () => {
            try {
                const response = await fetch(`https://localhost:7267/admin/sub-categories/popular/${mainCategoryId}`);
                const data: MostPopularInMainCategoryItems[] = await response.json();
                setMostPopularItems(data);
            } catch (error) {
                console.error("Error fetching most popular items:", error);
            }
        };

        fetchMostPopularItems();
    }, [mainCategoryId]);

    return (
        <div className="most-popular-in-main-category">
            <div className="most-popular-in-main-category-title">Most Popular in {mainCategoryName}</div>
            {mostPopularItems.length > 0 && 
                <div className="most-popular-items">
                    {mostPopularItems.map(item => (
                        <MostPopularInMainCategoryItem key={item.id}
                            id={item.id}
                            name={item.name} imageUrl={item.imageUrl}
                        />
                    ))}
                </div>
            }

        </div>
    );
}