import { Breadcrumb } from "../../../Shared/PageItems/Breadcrumb/Breadcrumb";
import { useAuth } from "../../../AuthContext";
import { useEffect, useState, useCallback } from "react";
import axios from "../../../axiosInstance";
import { AllMessages } from "../AllMessages/AllMessages";
import { MessageBox, NewMessageSignalRDto } from "../MessageBox/MessageBox/MessageBox";
import "./InboxPage.css";

export interface AllMessagesItem {
  id: number;
  content: string;
  timestamp: string;
  isRead: boolean;
  senderUsername: string;
  senderProfilePictureUrl: string;
  isStarred: boolean;
}

export function InboxPage() {
  const auth = useAuth();

  const [allMessages, setAllMessages] = useState<AllMessagesItem[]>([]);
  const [selectedOrderStatusIds, setSelectedOrderStatusIds] = useState<number[]>([]);
  const [filterByCustomOffer, setFilterByCustomOffer] = useState(false);
  const [filterByStarred, setFilterByStarred] = useState(false);
  const [selectedMessageId, setSelectedMessageId] = useState<number | null>(null);

  const fetchAllMessages = useCallback(async (filters: {
    selectedOrderStatusIds: number[];
    filterByCustomOffer: boolean;
    filterByStarred: boolean;
  }) => {
    try {
      const url = "https://localhost:7267/messages";
      const response = await axios.get<AllMessagesItem[]>(url, {
        params: {
          orderStatusIds: filters.selectedOrderStatusIds,
          hasCustomOffer: filters.filterByCustomOffer,
          isStarred: filters.filterByStarred,
        }
      });
      console.log("Fetched messages:", response.data);
      setAllMessages(response.data);
    } catch (error) {
      console.error("Error fetching filtered messages:", error);
    }
  }, []);

  const handleApplyFilters = (filters: {
    selectedOrderStatusIds: number[];
    filterByCustomOffer: boolean;
    filterByStarred: boolean;
  }) => {
    setSelectedOrderStatusIds(filters.selectedOrderStatusIds);
    setFilterByCustomOffer(filters.filterByCustomOffer);
    setFilterByStarred(filters.filterByStarred);
    fetchAllMessages(filters);
  };

  const handleMessageClick = (messageId: number) => {
    setSelectedMessageId(messageId);
    setAllMessages(prev =>
      prev.map(message =>
        message.id === messageId ? { ...message, isRead: true } : message
      )
    );
  };

  useEffect(() => {
    fetchAllMessages({
    selectedOrderStatusIds,
    filterByCustomOffer,
    filterByStarred,
  });
  }, []);

  useEffect(() => {
  fetchAllMessages({
    selectedOrderStatusIds,
    filterByCustomOffer,
    filterByStarred,
  });
}, [fetchAllMessages, selectedOrderStatusIds, filterByCustomOffer, filterByStarred]);


  useEffect(() => {
    if (allMessages.length > 0) {
      setSelectedMessageId(allMessages[0].id);
    }
  }, [allMessages]);

  useEffect(() => {
  const unsubscribe = auth.registerOnReceiveMessage?.((incoming: unknown) => {
    if (
      typeof incoming !== "object" ||
      incoming === null ||
      !("conversationId" in incoming) ||
      !("conversationPreview" in incoming)
    ) {
      return; 
    }

    const dto = incoming as NewMessageSignalRDto;

    const newMessagePreview: AllMessagesItem = {
      id: dto.conversationPreview.id,
      content: dto.conversationPreview.content,
      timestamp: dto.conversationPreview.timestamp,
      isRead: dto.conversationPreview.isRead,
      senderUsername: dto.conversationPreview.senderUsername,
      senderProfilePictureUrl: dto.conversationPreview.senderProfilePictureUrl,
      isStarred: dto.conversationPreview.isStarred,
    };

    setAllMessages(prev => {
      if (!Array.isArray(prev)) return [newMessagePreview];

      const alreadyExists = prev.some(m => m.id === newMessagePreview.id);
      if (alreadyExists) return prev;

      return [...prev, newMessagePreview];
    });
  }) as (() => void) | undefined;

  return () => {
    if (typeof unsubscribe === "function") {
      unsubscribe();
    }
  };
}, [auth]);

  return (
    <div className="inbox-page">
      <Breadcrumb items={[ { label: <i className="bi bi-house-door" />, to: `/${auth?.user?.mode}/dashboard` }, { label: "Inbox messages" }, ]}/>

      <div className="inbox-messages-all-preview d-flex flex-row">

        <AllMessages
          messages={allMessages}
          selectedOrderStatusIds={selectedOrderStatusIds}
          filterByCustomOffer={filterByCustomOffer}
          filterByStarred={filterByStarred}
          setSelectedMessageId={setSelectedMessageId}
          onApply={handleApplyFilters}
          onMessageClick={handleMessageClick}
        />
<div className="message-box-container">
        {selectedMessageId !== null && <MessageBox id={selectedMessageId} />}
        </div>
      </div>
    </div>
  );
}
