import { useEffect, useState } from "react";
import { SearchByKeyword } from "../../Admin/Components/Filters/Inputs/SearchByKeyword";
import axios from "axios";
import { ButtonDropdownContainer } from "../../Admin/Components/Dropdowns/Common/ButtonDropdownContainer";
import { ActionButton } from "../../Shared/Buttons/ActionButton";
import { DateRange } from "../../Admin/Components/Dropdowns/Populate/DateRange";
import { DataTable } from "../../Admin/Components/Tables/Common/AdminDataTable";
import { NoInvoiceDataTd } from "../../Admin/Components/Tables/Common/NoInvoiceDataTd";
import "./FinancialDocuments.css";


interface BillingHistoryRowModel {
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
    documentNumber: "Document",
    service: "Service",
    orderNumber: "Order",
    total: "Total",
    pdfLink: "PDF"
};

export function FinancialDocuments() {
    const [keyword, setKeyword] = useState<string | undefined>(undefined);
    const [invoices, setInvoices] = useState<BillingHistoryRowModel[]>([]);
    const [showDropdown, setShowDropdown] = useState<boolean>(false);
    const [fromDate, setFromDate] = useState<string | undefined>(undefined);
    const [toDate, setToDate] = useState<string | undefined>(undefined);

    const fetchInvoices = async () => {
        try {
            const url = new URL("https://localhost:5001/api/BillingHistory");
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
            const response = await axios.get<BillingHistoryRowModel[]>(url.toString(), { params });
            setInvoices(response.data);
        } catch (error) {
            console.error("Error fetching billing history:", error);
        }
    };

    useEffect(() => {
        fetchInvoices();
    }, []);

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
    <><div className="page-filter-section">
          <SearchByKeyword setKeyword={handleChangeKeyword} />
          <ButtonDropdownContainer>
              <ActionButton text={<>Date range <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleDropdownVisibility} className={"dropdown-button"} ariaLabel={"Dropdown Sub Categories Button"} />
              <div className={`filter-dropdown ${showDropdown ? 'show' : ''}`}>
                  {showDropdown && <DateRange show={showDropdown} setFromDate={setFromDate} setToDate={setToDate} onClearAll={handleOnDateRangeClose} />}
              </div>
          </ButtonDropdownContainer>
      </div>
              <DataTable data={invoices} columns={["id", "date", "documentNumber", "service", "orderNumber", "total", "pdfLink"]} headers={tableHeaders} noDataTd={<NoInvoiceDataTd colSpan={7} />} />
      </>
  );
}