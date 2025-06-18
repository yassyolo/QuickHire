import { useState } from "react";
import { ActionButton } from "../../../Buttons/ActionButton/ActionButton";
import { useNavigate } from "react-router-dom";
interface OrderActionsProps {
    order: { id: number };
}

export function OrderActions ({order}: OrderActionsProps) {
    const [showActionsDropdown, setShowActionsDropdown] = useState(false); 
    const {id} = order;
    const navigate = useNavigate();

    const handlePreviewModalVisibility = () => {
        setShowActionsDropdown(false);
        navigate(   `/seller/orders/${id}`);
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
