import { useState } from "react";
import { ButtonDropdownContainer } from "../../../Admin/Components/Dropdowns/Common/ButtonDropdownContainer";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { AllMessagesItem } from "../InboxPage";
import { AllMessagesItemComponent } from "./AllMessagesItem";
import { MessageFilterDropdown } from "./MessagesFilter";
import "./AllMessages.css";

interface AllMessagesProps {
    messages: AllMessagesItem[];
    selectedOrderStatusIds: number[];
  filterByCustomOffer: boolean;
  filterByStarred: boolean;
  onApply: (filters: {
    selectedOrderStatusIds: number[]; 
    filterByCustomOffer: boolean;
    filterByStarred: boolean;
  }) => void;
}

export function AllMessages({ messages, selectedOrderStatusIds, filterByCustomOffer, filterByStarred, onApply }: AllMessagesProps) {

    const [showFilterDropdown, setShowFilterDropdown] = useState(false);
    const handleFilterDropdownVisibility = () => {
        setShowFilterDropdown(!showFilterDropdown); 
    }
    return (
        <div className="inbox-messages-all-preview d-flex flex-column">
            <ButtonDropdownContainer>  
                        <ActionButton text={<> Conversations <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}></i></>} onClick={handleFilterDropdownVisibility} className={"dropdown-button conversation"} ariaLabel={"Dropdown Coversations Type Button"} />
                        <div className={`filter-dropdown ${showFilterDropdown ? 'show' : ''}`}>
                         {showFilterDropdown && <MessageFilterDropdown selectedOrderStatusIds={selectedOrderStatusIds} filterByCustomOffer={filterByCustomOffer} filterByStarred={filterByStarred} onApply={onApply}/> }
                        </div>
            </ButtonDropdownContainer>
            <div className="all-messages-container">
                 {messages.map((message) => (
               <AllMessagesItemComponent key={message.id} message={message}/>
            ))}
            </div>
           
        </div>
    );
}