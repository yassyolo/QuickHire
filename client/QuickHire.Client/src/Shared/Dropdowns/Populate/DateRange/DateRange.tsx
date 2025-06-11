import { useEffect, useState } from "react";
import { FormGroup } from "../../../Forms/FormGroup/FormGroup";
import { Dropdown } from "../../Common/Dropdown/Dropdown";
import "./DateRange.css";

interface DateRangeProps {
    show: boolean;
    setFromDate: (date: string) => void;
    setToDate: (date: string) => void;
    onClearAll: () => void;
}

export function DateRange({show, setFromDate, setToDate, onClearAll}: DateRangeProps) {
    const [LocalFromDate, setLocalFromDate] = useState<string>("");
    const [LocalToDate, setLocalToDate] = useState<string>("");

    useEffect(() => {
        if (!show) {
            setLocalFromDate("");
            setLocalToDate("");

        }
    }, [show]);

    const handleFromDateChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setLocalFromDate(event.target.value);
    };

    const handleToDateChange = (event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setLocalToDate(event.target.value);
    };

    const handleClearAll = () => {
        setLocalFromDate("");
        setLocalToDate("");
        onClearAll();
    };

    const handleOnApply = () => {
        if (LocalFromDate) {
            setFromDate(LocalFromDate);
        }
        if (LocalToDate) {
            setToDate(LocalToDate);
        }
    };

    if (!show) return null;

    return (
        <Dropdown onClearAll={handleClearAll} onApply={handleOnApply}>
        <div className="from-to d-flex flex-row">
            <FormGroup id={"from-date"} label={"From"} type={"date"} value={LocalFromDate} onChange={handleFromDateChange}   />
            <FormGroup id={"to-date"} label={"To"} type={"date"} value={LocalToDate} onChange={handleToDateChange}    />
        </div>       
    </Dropdown>
    )
}