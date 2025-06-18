import { useEffect, useState } from "react";
import { SearchByKeyword } from "../../../../../Shared/Forms/SearchInputs/SearchByKeyword/SearchByKeyword";
import { ButtonDropdownContainer } from "../../../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { DateRange } from "../../../../../Shared/Dropdowns/Populate/DateRange/DateRange";
import { DataTable } from "../../../../../Shared/Tables/Common/DataTable/AdminDataTable";
import "./FinancialDocuments.css";
import axios from "../../../../../axiosInstance";


export interface BillingHistoryRowModel {
    id: number;
    date: string;
    documentNumber: string;
    service: string;
    orderNumber: string;
    total: string;
    pdfLink: string;
}

const tableHeaders = {
    id: "ID",
    date: "Date",
    documentNumber: "Invoice Number",
    service: "Service",
    orderNumber: "Order",
    total: "Total",
    pdfLink: "Document Preview"
};

interface FinancialDocumentsProps {
    buyer: boolean;
}

export function FinancialDocuments({ buyer }: FinancialDocumentsProps) {
    const [keyword, setKeyword] = useState<string | undefined>(undefined);
    const [invoices, setInvoices] = useState<BillingHistoryRowModel[]>([]);
    const [showDropdown, setShowDropdown] = useState<boolean>(false);
    const [fromDate, setFromDate] = useState<string | undefined>(undefined);
    const [toDate, setToDate] = useState<string | undefined>(undefined);

    const fetchInvoices = async () => {
        try {
            const url = new URL(`https://localhost:7267/users/billings-and-payments/financial-documents`);
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
            params.append("buyer", buyer.toString());
            const response = await axios.get<BillingHistoryRowModel[]>(url.toString(), { params });
            setInvoices(response.data);
            console.log("Invoices fetched:", response.data);
        } catch (error) {
            console.error("Error fetching billing history:", error);
        }
    };

    useEffect(() => {
        fetchInvoices();
    }, [keyword, fromDate, toDate, buyer]);

    const handleChangeKeyword = (newKeyword: string) => {
        setKeyword(newKeyword);
    };

    const handleDropdownVisibility = () => {setShowDropdown(!showDropdown)};
    const handleOnDateRangeClose = () => {
        setFromDate(undefined);
        setToDate(undefined);
        setShowDropdown(false);
        fetchInvoices();
    }
  return (
    <div className="financial-documents-page"><div className="page-filter-section">
          <SearchByKeyword setKeyword={handleChangeKeyword} />
          <ButtonDropdownContainer>
              <ActionButton text={<>Date range <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Sub Categories Button"} />
              <div className={`filter-dropdown ${showDropdown ? 'show' : ''}`}>
                  {showDropdown && <DateRange show={showDropdown} setFromDate={setFromDate} setToDate={setToDate} onClearAll={handleOnDateRangeClose} />}
              </div>
          </ButtonDropdownContainer>
        </div>
        <div style={{marginTop: "30px"}}>
                    <DataTable data={invoices} columns={[ "id", "date", "documentNumber", "service", "orderNumber", "total", "pdfLink"]} headers={tableHeaders} />

        </div>
    </div>
  );
}