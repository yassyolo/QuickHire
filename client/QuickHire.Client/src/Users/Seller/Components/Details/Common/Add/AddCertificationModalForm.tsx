import { useCallback, useEffect, useState } from "react";
import { useTooltip } from "../../../../../../Shared/Tooltip/Tooltip";
import axios from "axios";
import { FormGroup } from "../../../../../../Shared/Forms/FormGroup";
import { AddDetailsModal } from "./AddDetailsForm";

export function AddCertificationModalForm() {
    const [certification, setCertification] = useState<string>("");
    const [issuer, setIssuer] = useState<string>("");
    const [showIssuerTooltip, handleShowIssuerTooltip] = useTooltip();
    const [date, setDate] = useState<string>("");
    const [showDateTooltip, handleShowDateTooltip] = useTooltip();
    const [showCertificationTooltip, handleShowCertificationTooltip] = useTooltip();
    const [validationErrors, setValidationErrors] = useState<{ Certification?: string[] }>({});

    useEffect(() => {
        if (!certification) {
            setCertification("");
            setIssuer("");
            setDate("");
            setValidationErrors({});
        }
    }, [certification, issuer, date]);

    const handleCertificationChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {setCertification(event.target.value)}, []);
    const handleIssuerChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {setIssuer(event.target.value)}, []);
    const handleDateChange = useCallback((event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {setDate(event.target.value)}, []);

    const onAdd = async () => {
        try {
            const url = "https://localhost:7267/users/certification/add";
            const data = {
                certification: certification
            };
            const response = await axios.post(url, data);
            if (response.status === 200) {
                setCertification("");
            }
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setValidationErrors({
                    Certification: error.response.data.errors?.Certification || []
                });
            } else {
                console.error("An unexpected error occurred:", error);
            }
        }
    };

    return (
        <AddDetailsModal onSave={onAdd}>     
        <FormGroup id={"certification"} label={"Certification"} tooltipDescription={"Enter the name of the certification."} type={"text"} value={certification} onChange={handleCertificationChange} placeholder={"Enter Certification"} ariaDescribedBy={"certification-help"} onShowTooltip={handleShowCertificationTooltip} showTooltip={showCertificationTooltip} error={validationErrors.Certification || []}/>
        <FormGroup id={"issuer"} label={"Issuer"} tooltipDescription={"Enter the name of the organization or institution that issued the certification."} type={"text"} value={issuer} onChange={handleIssuerChange} placeholder={"Enter Issuer"} ariaDescribedBy={"issuer-help"} onShowTooltip={handleShowIssuerTooltip} showTooltip={showIssuerTooltip} error={validationErrors.Certification || []}/>
        <FormGroup id={"date"} label={"Date"} tooltipDescription={"Enter the date you received the certification."} type={"text"} value={date} onChange={handleDateChange} placeholder={"Enter Date"} ariaDescribedBy={"date-help"} onShowTooltip={handleShowDateTooltip} showTooltip={showDateTooltip} error={validationErrors.Certification || []}/>
        </AddDetailsModal>
    );
}