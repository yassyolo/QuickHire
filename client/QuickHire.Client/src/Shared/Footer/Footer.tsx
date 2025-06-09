import { Link } from "react-router-dom";
import { Logo } from "../Logo/Logo";
import { useEffect, useState } from "react";
import axios from "axios";
import './Footer.css';

export interface CategoryLink{
    id: number;
    name: string;
}

export function Footer() {
    const [categories, setCategories] = useState<CategoryLink[]>([]);

    const fetchCategories = async () => {
        try {
            const response = await axios.get<CategoryLink[]>("https://localhost:7267/main-categories/link");
            setCategories(response.data);
        } catch (error) {
            console.error("Error fetching categories:", error);
        }
    };

    useEffect(() => 
    {
        fetchCategories();
    }
    , []);
    return (

        <footer className="footer-container">
            <div className="footer-content">
            <div className="footer-left">
                <Logo/>
                <div className="footer-info">
                    Connect with talented freelancers and get your projects done with quality and efficiency.
                </div>
                <div className="useful-links">
                    <i className="bi bi-link-45deg"></i>
                </div>
            </div>
            <div className="footer-middle">
                <ul className="footer-middle-links">
                    {categories.map((category) => (
                        <Link key={category.id} to={`/buyer/main-categories/${category.id}`} className="footer-middle-link">
                            {category.name}
                        </Link>
                    ))}
                </ul>

            </div>
            <div className="footer-right">
                <div className="d-flex flex-row footer-right-email-container">
                                    <i className="bi bi-at"></i>
                <span className="footer-right-email">support@email.com</span>
                </div>
            </div>
            </div>
            <div className="footer-bottom">
                <p>© 2025 QuickHire. All rights reserved.</p>
            </div>
        </footer>

    );
}