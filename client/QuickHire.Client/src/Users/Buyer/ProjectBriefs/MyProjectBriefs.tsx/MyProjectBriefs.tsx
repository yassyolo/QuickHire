import { useEffect, useState } from "react";
import { SellerPage } from "../../../Seller/Pages/Common/SellerPage";
import { PageTitle } from "../../../../Admin/Pages/Common/PageTitle";
import { SearchByKeyword } from "../../../../Admin/Components/Filters/Inputs/SearchByKeyword/SearchByKeyword";
import { ButtonDropdownContainer } from "../../../../Admin/Components/Dropdowns/Common/ButtonDropdownContainer";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import { DateRange } from "../../../../Admin/Components/Dropdowns/Populate/DateRange";
import { DataTable } from "../../../../Admin/Components/Tables/Common/AdminDataTable";
import axios from "axios";

interface ProjectBriefRowModel{
    id: number;
    date: string;
    documentNumber: string;
    sellersReaced: number;
    totalOffers: number;
    order: boolean;
    status: string;
}

const headers = {
    id: "ID",
    date: "Date",   
    documentNumber: "Document Number",
    sellersReaced: "Sellers Reached",
    totalOffers: "Total Offers",
    order: "Order",
    status: "Status"
};
export function MyProjectBriefs() {
    const [keyword, setKeyword] = useState<string | undefined>(undefined);
    const [projectBriefs, setPtojectBriefs] = useState<ProjectBriefRowModel[]>([]);
    const [showDropdown, setShowDropdown] = useState<boolean>(false);
    const [fromDate, setFromDate] = useState<string | undefined>(undefined);
    const [toDate, setToDate] = useState<string | undefined>(undefined);

    const fetchProjectBriefs = async () => {
        try {
            const url = new URL(`https://localhost:7267/buyers/project-briefs`);
            const params = new URLSearchParams();
            if (keyword) {
                params.append("keyword", keyword);
            }
            if (fromDate) {
                params.append("fromDate", fromDate);
            }
            if (toDate) {
                params.append("toDate", toDate);
            }
            const response = await axios.get<ProjectBriefRowModel[]>(url.toString(), { params });
            setPtojectBriefs(response.data);
            console.log("Invoices fetched:", response.data);
        } catch (error) {
            console.error("Error fetching billing history:", error);
        }
    };

    useEffect(() => {
        fetchProjectBriefs();
    }, [keyword, fromDate, toDate]);

    const handleChangeKeyword = (newKeyword: string) => {
        setKeyword(newKeyword);
    };

    const handleDropdownVisibility = () => {setShowDropdown(!showDropdown)};
    const handleOnDateRangeClose = () => {
        setFromDate(undefined);
        setToDate(undefined);
        setShowDropdown(false);
        fetchProjectBriefs();
    }
    return (
        <SellerPage>
            <PageTitle title="My project briefs"   description="View your billing history, manage payment methods, and keep your account information up to date."  breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: `/buyer/dashboard` },{ label: "Billing and Payments" }]}/>            
       <div className="financial-documents-page"><div className="page-filter-section">
                 <SearchByKeyword setKeyword={handleChangeKeyword} />
                 <ButtonDropdownContainer>
                     <ActionButton text={<>Date range <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Sub Categories Button"} />
                     <div className={`filter-dropdown ${showDropdown ? 'show' : ''}`}>
                         {showDropdown && <DateRange show={showDropdown} setFromDate={setFromDate} setToDate={setToDate} onClearAll={handleOnDateRangeClose} />}
                     </div>
                 </ButtonDropdownContainer>
               </div>
               <DataTable data={projectBriefs} columns={["id", "date", "documentNumber", "sellersReaced", "order", "status", "totalOffers"]} headers={headers} />
           </div>
        </SellerPage>
    );
}