import { Breadcrumb } from "../../Shared/Breadcrumb/Breadcrumb";
import { useAuth } from "../../AuthContext";
import { useEffect, useState } from "react";
import axios from "../../axiosInstance";
import { AllMessages } from "./AllMessages/AllMessages";
import { MessageBox } from "./MessageBox/MessageBox/MessageBox";

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
    const user = useAuth(); 
    const [allMessages, setAllMessages] = useState<AllMessagesItem[]>([]);
    const [selectedOrderStatusIds, setSelectedOrderStatusIds] = useState<number[]>([]);
const [filterByCustomOffer, setFilterByCustomOffer] = useState(false);
const [filterByStarred, setFilterByStarred] = useState(false);
const [selectedMessageId, setSelectedMessageId] = useState<number | null>(null);

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

const auth = useAuth();

useEffect(() => {
  if (allMessages.length > 0) {
    setSelectedMessageId(allMessages[0].id);
}
}
, [allMessages]);

useEffect(() => {
  auth.registerOnReceiveMessage?.((message) => {
    setAllMessages(prev => {
      if (!prev) return prev;
      return [...prev, message as AllMessagesItem];
    });
  });
}, [auth]);

   const fetchAllMessages = async (filters: {
  selectedOrderStatusIds: number[];
  filterByCustomOffer: boolean;
  filterByStarred: boolean;
}) => {
  try {
    const url = 'https://localhost:7267/messages';
    const response = await axios.get<AllMessagesItem[]>(
      url,
      {
        params: {
          orderStatusIds: filters.selectedOrderStatusIds,
          hasCustomOffer: filters.filterByCustomOffer,
          isStarred: filters.filterByStarred,
        }
      }
    );
    setAllMessages(response.data);
    console.log("Fetched messages:", response.data);
  } catch (error) {
    console.error("Error fetching filtered messages:", error);
  }
};

const handleMessageClick = (messageId: number) => {
    setSelectedMessageId(messageId);
    setAllMessages(prev => {
        if (!prev) return prev;
        return prev.map(message => 
            message.id === messageId ? { ...message, isRead: true } : message
        );
    }
    );
}

    useEffect(() => {
        fetchAllMessages({
            selectedOrderStatusIds,
            filterByCustomOffer,
            filterByStarred
        });
    }, []);
  return (
        <div style={{padding: '10px 20px 20px 20px'}}><Breadcrumb items={[{ label: <i className="bi bi-house-door"></i>, to: `/${user?.user?.mode}/dashboard` }, { label: "Inbox messages" }]} />
        <div className="inbox-messages-all-preview d-flex flex-row" style={{ width: "100%", height: "70vh" }}>
      <AllMessages setSelectedMessageId={setSelectedMessageId} messages={allMessages || []} selectedOrderStatusIds={selectedOrderStatusIds} filterByCustomOffer={filterByCustomOffer} filterByStarred={filterByStarred} onApply={handleApplyFilters} onMessageClick={handleMessageClick} />
      {selectedMessageId !== null && <MessageBox id={selectedMessageId} />}
            </div>


    </div>
  );
}