import { useNavigate } from 'react-router-dom';
import './Logo.css';
import { useAuth } from '../../../AuthContext';

export function Logo() {
    const {user} = useAuth();
    const navigate = useNavigate();
    const handleOnClick = () => {
        if(user?.mode === "buyer") {
            navigate("/buyer");
        }
        else if(user?.mode === "seller") {
            navigate("/seller/dashboard");
        }
        else if(user?.roles.includes("admin")) {
            navigate("/admin/main-categories");
        }
        else {
            navigate("/users");
        }
    };
    return (
        <div className="logo-text" onClick={handleOnClick}>
           <span >Quick</span>
           <span>Hire</span>
        </div>
    );
}