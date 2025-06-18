import { useState } from "react";
import { IconButton } from "../../../../Shared/Buttons/IconButton/IconButton";
import { ReportModal } from "./ReportForm/ReportModal";
import "./ReportButtonDropdown.css";

interface ReportButtonDropdownProps {
    gigId?: number;
    userId?: number;
}

export function ReportButtonDropdown({ gigId, userId }: ReportButtonDropdownProps) {
    const [showDropdown, setShowDropdown] = useState(false);
    const [showReportForm, setShowReportForm] = useState(false);
    const toggleDropdown = () => {
        setShowDropdown(!showDropdown);
    };

    const toggleReportForm = () => {
        setShowReportForm(!showReportForm);
        setShowDropdown(false);
    };
    return (
        <div className="report-button-dropdown">
             <IconButton icon={<i className="bi bi-three-dots"></i>} onClick={toggleDropdown} className={"report-button"} ariaLabel={"Report ietm"}  buttonInfo="Report item"       />
            {showDropdown && 
            <div className="report-dropdown-menu">
                <div className="report-dropdown-item" onClick={toggleReportForm}>Report item</div>
            </div>}   
            {showReportForm && <ReportModal onClose={toggleReportForm}  gigId={gigId} userId={userId} />}        
        </div>
    );
}