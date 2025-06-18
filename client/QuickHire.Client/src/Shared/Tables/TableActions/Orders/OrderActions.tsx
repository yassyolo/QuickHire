import { useState } from "react";
import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../../../AuthContext";
interface OrderActionsProps {
    order: { id: number };
}

export function OrderActions ({order}: OrderActionsProps) {
    const [showActionsDropdown, setShowActionsDropdown] = useState(false); 
    const {user} = useAuth();

    const {id} = order;
    const navigate = useNavigate();

    const handlePreviewModalVisibility = () => {
        if(user?.mode === "buyer") {
            navigate(`/buyer/orders/${id}`);
            return;
        }
        if(user?.mode === "seller") {
            navigate(`/seller/orders/${id}`);
            return;
        }
        setShowActionsDropdown(false);
    };

    const handleActionsDropdownVisibility = () => setShowActionsDropdown(!showActionsDropdown);

  

    return (
        <>
            <div className="actions-container">
                <ActionButton text={<i className="bi bi-caret-down-fill"></i>} onClick={handleActionsDropdownVisibility} className={"actions-dropdown-visibility-button"} ariaLabel={"ActionsDropdownVisibility"} />
                {showActionsDropdown && (
                    <div className="actions-dropdown">
                        <div className="actions-dropdown-item" onClick={handlePreviewModalVisibility}>See</div>
                       
                    </div>
                )}
            </div>           
       </>
    );
};
