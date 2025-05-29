import { ChangeEvent, useEffect, useState } from "react";
import { AddModal } from "./Common/AddModal";
import axios from "axios";
import { FormGroup } from "../../../../Shared/Forms/FormGroup";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton";
import "./AddSubSubCategoryModal.css";
import { FormLabel } from "../../../../Shared/Forms/FormLabel";
import { IconButton } from "../../../../Shared/Buttons/IconButton";

export interface AddSubCategoryModalProps {
    title: string;
    showModal: boolean;
    onClose: () => void;
    onAddSubSubCategorySuccess: () => void;
}

interface FilterOption {
    id: number;
    value: string;
}

interface Filter {
    id: number;
    name: string;
    options: FilterOption[];
}

export function AddSubSubCategoryModal({ title, showModal, onClose, onAddSubSubCategorySuccess } : AddSubCategoryModalProps) {
    const [name, setName] = useState<string>("");
    const [filters, setFilters] = useState<Filter[]>([]);
    const [showNameTooltip, setShowNameTooltip] = useState<boolean>(false);
    const [showFilterTooltip, setShowFilterTooltip] = useState<boolean>(false);
    const [optionsError, setOptionsError] = useState<string[]>([]);
    let counter = 0;

    useEffect(() => {
        if (!showModal) {
            setName("");
            setFilters([]);
        }
    }, [showModal]);

    const handleNameChange = (event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        event.preventDefault();
        setName(event.target.value);
    };

    const handleShowNameTooltip = () => {
        setShowNameTooltip(true);
        setTimeout(() => setShowNameTooltip(false), 2000);
    };

    const handleShowFilterTooltip = () => {
        setShowFilterTooltip(true);
        setTimeout(() => setShowFilterTooltip(false), 2000);
    };

    const handleAddFilter = () => setFilters(prev => [...prev,{ id: counter++, name: "", options: [] }]);
    const handleRemoveFilter = (id: number) => setFilters(prev => prev.filter(x => x.id !== id));

    const handleFilterNameChange = (id: number, value: string) => {
        setFilters(prev => prev.map(f => f.id === id ? { ...f, name: value } : f));
    };

    const handleAddFilterOption = (filterId: number) => {
        setFilters(prev =>
            prev.map(f => f.id === filterId
                ? { ...f, options: [...f.options, { id: counter++, value: "" }] } : f
            )
        );
    };

    const handleRemoveFilterOption = (filterId: number, optionId: number) => {
        setFilters(prev =>
            prev.map(f => f.id === filterId
                ? { ...f, options: f.options.filter(o => o.id !== optionId) }
                : f
            )
        );
    };

    const handleOptionValueChange = (filterId: number, optionId: number, value: string) => {
        setFilters(prev => prev.map(f => f.id === filterId
                ? { ...f, options: f.options.map(o =>
                        o.id === optionId ? { ...o, value } : o
                    )
                }
                : f
            )
        );
    };

    const handleSubmit = async () => {
        try {
            const payload = {
                name,
                filters: filters.map(f => ({
                    name: f.name,
                    options: f.options.map(o => o.value)
                }))
            };

            const response = await axios.post("/api/main-category", payload);
            console.log("Main Sub sub added successfully:", response.data);

            onAddSubSubCategorySuccess();
            onClose();
        } catch (error : unknown) {
            if (axios.isAxiosError(error) && error.response && error.response.status === 400) {
                console.error("Validation Errors:", error.response.data.errors);
                setOptionsError(error.response.data.errors?.Options || []);
            } else {
                console.error("Error adding Sub sub Category:", error);
            }
        } finally {
            setName("");
            setFilters([]);
        }
    };

    if (!showModal) return null;

    return (
        <AddModal title={title} onClose={onClose} onContinue={handleSubmit}>

          <FormGroup id={"sub-sub-category-name"} label={"Name"} tooltipDescription={"Use a clear, descriptive name."} type={"text"} value={name} onChange={handleNameChange} placeholder={"Enter Name"} ariaDescribedBy={"name-help"} onShowTooltip={handleShowNameTooltip} showTooltip={showNameTooltip} />
            <div className="form-group">
                <div className="d-flex flex-row justify-content-between">
                    <FormLabel id={"sub-sub-category-filters"} label={"Filters"} tooltipDescription={"Filters let users refine gigs based on specific criteria."} onShowTooltip={handleShowFilterTooltip} showTooltip={showFilterTooltip} ariaDescribedBy={"filter-help"} />
                   <ActionButton text="Add filter +" onClick={handleAddFilter} className="add-filter-button" ariaLabel="Add filter button"/>
                </div>
                {filters.map((filter) => (
                    <div key={filter.id} className="new-filter-section">
                        <div className="title-section d-flex flex-row justify-content-between">
                            <div className="d-flex flex-row align-items-center">
                            <input type="text" className="form-control me-2" placeholder="Enter Title" value={filter.name} style={{width: "40%"}} onChange={(e) => handleFilterNameChange(filter.id, e.target.value)}/>
                            <IconButton icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }} />} onClick={() => handleRemoveFilter(filter.id)} className="faq-delete-button" ariaLabel="Remove Filter Button" />  
                            </div>                        
                            <ActionButton text="Add option +" onClick={() => handleAddFilterOption(filter.id)} className="add-filter-button" ariaLabel="Add filter option"/>
                        </div>

                        {filter.options.map((option) => (
                            <div key={option.id} className="d-flex mb-1">

                           <input id={"filter-option"} type="text" className={`form-control me-2 ${optionsError && optionsError.length > 0 ? 'error' : ''}`} placeholder={"Filter Option"} value={option.value} onChange={(e) => handleOptionValueChange(filter.id, option.id, e.target.value)}/>
           {optionsError && optionsError.length > 0 && (
                <div className="validation-error" key={optionsError[0]} >{optionsError[0]}</div> 
            )}            
            <IconButton icon={<i className="bi bi-x" style={{ fontSize: "20px", color: "red" }} />} onClick={() =>  handleRemoveFilterOption(filter.id, option.id)} className="faq-delete-button" ariaLabel="Remove Filter Option Button" />                          
                </div>
                        ))}

                    </div>
                ))}
            </div>
        </AddModal>
    );
}
