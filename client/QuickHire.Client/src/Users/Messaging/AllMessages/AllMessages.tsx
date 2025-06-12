import { useState } from "react";
import { ButtonDropdownContainer } from "../../../Shared/Dropdowns/Common/Dropdown/ButtonDropdownContainer";
import { ActionButton } from "../../../Shared/Buttons/ActionButton/ActionButton";
import { AllMessagesItem } from "../InboxPage";
import { AllMessagesItemComponent } from "./AllMessagesItem";
import { MessageFilterDropdown } from "./MessagesFilter";
import "./AllMessages.css";

interface AllMessagesProps {
  setSelectedMessageId: (id: number | null) => void;
  onMessageClick: (messageId: number) => void;
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

export function AllMessages({
  messages,
  selectedOrderStatusIds,
  filterByCustomOffer,
  filterByStarred,
  onApply,
  setSelectedMessageId,
  onMessageClick,
}: AllMessagesProps) {
  const [showFilterDropdown, setShowFilterDropdown] = useState(false);
  const handleFilterDropdownVisibility = () => {
    setShowFilterDropdown(!showFilterDropdown);
  };

  return (
    <div className="inbox-messages-all-filter d-flex flex-column">
      {messages.length !== 0 &&
      <ButtonDropdownContainer>
        <ActionButton
          text={
            <>
              Conversations{" "}
              <i className="bi bi-chevron-down" style={{ fontSize: "520 !important" }}
              ></i>
            </>
          }
          onClick={handleFilterDropdownVisibility}
          className={"dropdown-button conversation"}
          ariaLabel={"Dropdown Conversations Type Button"}
        />
        <div className={`filter-dropdown ${showFilterDropdown ? "show" : ""}`}>
          {showFilterDropdown && (
            <MessageFilterDropdown
              selectedOrderStatusIds={selectedOrderStatusIds}
              filterByCustomOffer={filterByCustomOffer}
              filterByStarred={filterByStarred}
              onApply={onApply}
            />
          )}
        </div>
      </ButtonDropdownContainer>
}

      <div className="all-messages-container">
        {messages.length === 0 ? (
          <><div className="no-messages-yet">
            No messages yet
          </div><div className="no-messages-yet-description">
              You haven't received any messages yet. Once you do, they will appear here.
            </div></>
        ) : (
          messages.map((message) => (
            <AllMessagesItemComponent
              key={message.id}
              message={message}
              setSelectedMessageId={setSelectedMessageId}
              onMessageClick={onMessageClick}
            />
          ))
        )}
      </div>
    </div>
  );
}
