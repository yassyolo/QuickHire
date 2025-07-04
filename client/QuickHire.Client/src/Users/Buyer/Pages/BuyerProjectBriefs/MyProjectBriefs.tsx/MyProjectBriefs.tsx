import { useEffect, useState } from "react";
import { SellerPage } from "../../../../Seller/Pages/Common/SellerPage";
import { PageTitle } from "../../../../../Shared/PageItems/PageTitle/PageTitle";
import { SearchByKeyword } from "../../../../../Shared/Forms/SearchInputs/SearchByKeyword/SearchByKeyword";
import { ButtonDropdownContainer } from "../../../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { DateRange } from "../../../../../Shared/Dropdowns/Populate/DateRange/DateRange";
import { DataTable } from "../../../../../Shared/Tables/Common/DataTable/AdminDataTable";
import axios from "../../../../../axiosInstance";
import { ProjectBriefActions } from "../../../../../Shared/Tables/TableActions/ProjectBriefs/ProjectBriefActions";
interface ProjectBriefRowModel{
    id: number;
    date: string;
    documentNumber: string;
    sellersReached: number;
    totalOffers: number;
    status: string;
}

const headers = {
    id: "ID",
    date: "Date",   
    documentNumber: "Document Number",
    sellersReached: "Sellers Reached",
    totalOffers: "Total Offers",
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

    const handleWithdrawProjectBriefSuccess = (id: number) => {
        setPtojectBriefs(prev => prev.filter(x => x.id !== id)); 
    }
    return (
        <SellerPage>
            <PageTitle title="My project briefs"   description="View and manage all the project briefs you've created for sellers."  breadcrumbs={[{ label: <i className="bi bi-house-door"></i>, to: `/buyer/dashboard` },{ label: "My project briefs" }]}/>            
       <div className="financial-documents-page"><div className="page-filter-section" style={{marginTop: "50px"}}>
                 <SearchByKeyword setKeyword={handleChangeKeyword} />
                 <ButtonDropdownContainer>
                     <ActionButton text={<>Date range <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Sub Categories Button"} />
                     <div className={`filter-dropdown ${showDropdown ? 'show' : ''}`}>
                         {showDropdown && <DateRange show={showDropdown} setFromDate={setFromDate} setToDate={setToDate} onClearAll={handleOnDateRangeClose} />}
                     </div>
                 </ButtonDropdownContainer>
               </div>
               <div style={{marginTop: '20px'}}>               <DataTable data={projectBriefs} columns={["id", "date", "documentNumber", "sellersReached", "status", "totalOffers"]} headers={headers}     renderActions={(row: ProjectBriefRowModel) => (<ProjectBriefActions project={row} showNuyerInfo={false} onWithdrawSuccess={handleWithdrawProjectBriefSuccess}  />)} />
</div>
           </div>
        </SellerPage>
    );
}