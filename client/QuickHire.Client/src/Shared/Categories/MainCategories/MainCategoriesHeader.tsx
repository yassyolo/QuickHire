import { useEffect, useRef, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import { MainCategoriesHeaderDropdown } from "./MainCategoriesHeaderDropdown";
import { CategoryLink } from "../../Footer/Footer";
import './MainCategoriesHeader.css';

export function MainCategoriesHeader() {
    const [categories, setCategories] = useState<CategoryLink[]>([]);
    const [hoveredCategoryId, setHoveredCategoryId] = useState<number | null>(null);

    const dropdownRefs = useRef<Record<number, HTMLDivElement | null>>({});

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const response = await axios.get<CategoryLink[]>("https://localhost:7267/main-categories/link");
                setCategories(response.data);
            } catch (error) {
                console.error("Error fetching categories:", error);
            }
        };

        fetchCategories();
    }, []);

    useEffect(() => {
        setHoveredCategoryId(null);
    }, []);
 

  return (
  <div className="main-categories-header">
    <div className="categories-list">
      {categories.map((category, index) => {
        const isLastFour = index >= categories.length - 4;

        return (
          <div
            className="category-hover-wrapper" 
            key={category.id}
            onMouseEnter={() => {setHoveredCategoryId(category.id);}}
            onMouseLeave={() => {setHoveredCategoryId(null);}}
          >
            <div className="category-item">
              <Link to={`/buyer/main-categories/${category.id}`} className="category-link">
                {category.name}
              </Link>
            </div>

            {hoveredCategoryId === category.id && (
              <div
                ref={(el) => { dropdownRefs.current[category.id] = el; }}
                className={`main-categories-dropdown-menu ${isLastFour ? "align-right" : ""}`}
              >
                <MainCategoriesHeaderDropdown id={category.id} show={true} />
              </div>
            )}
          </div>
        );
      })}
    </div>
  </div>
);

}
