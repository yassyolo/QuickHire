import { Breadcrumb } from "../../Shared/Breadcrumb/Breadcrumb";
import { useAuth } from "../../AuthContext";
import { useEffect, useState } from "react";
import axios from "../../axiosInstance";
import { AllMessages } from "./AllMessages/AllMessages";
import { MessageBox } from "./MessageBox/MessageBox/MessageBox";

interface AllMessages{
    messages: AllMessagesItem[];
}

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
    const [allMessages, setAllMessages] = useState<AllMessages | null>(null);
    const [selectedOrderStatusIds, setSelectedOrderStatusIds] = useState<number[]>([]);
const [filterByCustomOffer, setFilterByCustomOffer] = useState(false);
const [filterByStarred, setFilterByStarred] = useState(false);

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

   const fetchAllMessages = async (filters: {
  selectedOrderStatusIds: number[];
  filterByCustomOffer: boolean;
  filterByStarred: boolean;
}) => {
  try {
    const response = await axios.post<AllMessages>(
      `/messages/all`,
      {
        orderStatusIds: filters.selectedOrderStatusIds,
        hasCustomOffer: filters.filterByCustomOffer,
        isStarred: filters.filterByStarred,
      }
    );
    setAllMessages(response.data);
  } catch (error) {
    console.error("Error fetching filtered messages:", error);
  }
};

    useEffect(() => {

        const mock = {
  "messages": [
    {
      "id": 1,
      "content": "Hi there! Are you available for a quick project discussion?",
      "timestamp": "06-09",
      "isRead": false,
      "senderUsername": "CreativeMarketer",
      "senderProfilePictureUrl": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAM1BMVEXk5ueutLeqsbTn6eqpr7PJzc/j5ebf4eLZ3N2wtrnBxsjN0NLGysy6v8HT1tissra8wMNxTKO9AAAFDklEQVR4nO2d3XqDIAxAlfivoO//tEOZWzvbVTEpic252W3PF0gAIcsyRVEURVEURVEURVEURVEURVEURVEURVEURVEURflgAFL/AirAqzXO9R7XNBVcy9TbuMHmxjN6lr92cNVVLKEurVfK/zCORVvW8iUBnC02dj+Wpu0z0Y6QlaN5phcwZqjkOkK5HZyPAjkIjSO4fIdfcOwFKkJlX4zPu7Ha1tIcwR3wWxyFhRG6g4Je0YpSPDJCV8a2Sv2zd1O1x/2WMDZCwljH+clRrHfWCLGK8REMiql//2si5+DKWKcWeAGcFMzzNrXC/0TUwQ2s6+LhlcwjTMlYsUIQzPOCb7YBiyHopyLXIEKPEkI/TgeuiidK/R9FniUDOjRDpvm0RhqjMyyXNjDhCfIMYl1gGjIMIuYsnGEYRMRZOMMunaLVwpWRW008v6fYKDIzxCwVAeNSO90BJW6emelYBRF/kHpYGVaoxTDAaxOFsfP9y8hpJ4xd7gOcij7JNGQ1EYFgkPJa1jQEiYZXRaRINKxSDUW9n+FT82lSKadkiru9/4XPqSLWOekGPoY05TAvLm9orm+YWuwHoBHkZKijNBJGmeb61eL6Ff/6q7bLr7yvv3vKGhpDRjvgjGaPz+gUg6YgcvpyAR2FIZ9U6nEEyZRTovmEU32KichpGn7C17XrfyH9gK/c0CMP05HZIM2uf9sEveizKveBy9/6Qt7o89ne33D525cfcIMW6ab+TMEukQbQbu+xu7X3A9bChmWaCeAkG17bpntwXgWxHaMzGPmUaR5dQZiKqRVeUZ3047fi3nAu28h4CHxCsZAgmEH8Y27jJAhm8c+5RQzRQNVGhVFSfxOYIjp/pP7RxzjevYXVGf4eLt+BJ1vCuLuLkrgABgCGXZ2wik5uty+oBvNirI6mkzhAf4Gsb58Hcm67Jzd+KwD10BYPLL3e0MjvKrgAULnOfveF/O4N2Xb9BZom3gJes3F9X5Zze8/6Yt09b4CrqsEjUv8oFBaR2rl+6CZr2xVrp24o/WitBKuGrrpl1+bFkmK2qXTON4VpbdfLa7o7y/WdLxG7lm2Lqh2clOwTegbvc/vj2U78CwhA87Bn8G5Nk3eOb0Nsr9flz3sG78UUtue4kpv1xvjg3TMay62BMlTlP+vrOMnJsRmt/ze0jsfkPPYdAH57hK+34PeOyc8XIXu5xT2HsUkdZz+adwg8HGFfQ3K5jtDvbUiO4Di9/ywHGrL88pDizZ++oTp+an+SMX/ndymUCwmHMdO7yuOx83pUx/eEMU0AvxWndwgidAqOZ8ypCwdEfvvEo6D9HwpA8wzvmOJEqAg9ySu8g4x0Hb9hSB/BANEKJ+LbPBU0lzbAJs4xt1AoshKkUGQmiH8/jJ0gdhTTLmSegHlPE0oOdXALnqDjKYh3px//fSgSWG8UqfrrIICzYYSJXRr9BSPbpNzw7gBjKjKOYI7ReIGqQRIap5+5MdjyvuDkExvGeXSlONWZAP3/AZBwJohU7QJRGU+cTVH18ELmRPNBmibW6MT/k1b0XhdkRBvyT6SB6EYv/GvhSmRNpGngRULsAlxMCGNXp7w3FfdEbTEEDdLI9TdIKRUzUesa3I461ER8cpNT7gMRhpKmYVS9ELOgCUQsa4SsulciKiLbY+AnHD8cpuhISsnxpamI84sbDq9qYJgf8wiiOBrC7Ml7M7ZECCqKoiiKoiiKoiiKoijv5AvJxlZRyNWWLwAAAABJRU5ErkJggg==",
      "isStarred": true
    },
    {
      "id": 2,
      "content": "Thanks for the update! Looking forward to the draft.",
      "timestamp": "2025-06-08T14:30:00Z",
      "isRead": true,
      "senderUsername": "TechGuru88",
      "senderProfilePictureUrl": "https://example.com/avatars/user2.jpg",
      "isStarred": false
    },
    {
      "id": 3,
      "content": "Can you please provide an estimate for this task?",
      "timestamp": "2025-06-07T09:00:00Z",
      "isRead": false,
      "senderUsername": "DesignQueen",
      "senderProfilePictureUrl": "https://example.com/avatars/user3.jpg",
      "isStarred": false
    },
    {
      "id": 4,
      "content": "Hey! Just submitted the first version. Let me know your feedback.",
      "timestamp": "2025-06-05T18:45:00Z",
      "isRead": true,
      "senderUsername": "WebDevPro",
      "senderProfilePictureUrl": "https://example.com/avatars/user4.jpg",
  
      "isStarred": true
    }
  ]
}

setAllMessages(mock);
        /*fetchAllMessages({
            selectedOrderStatusIds,
            filterByCustomOffer,
            filterByStarred
        });*/
    }, []);
  return (
        <div style={{padding: '10px 20px 20px 20px'}}><Breadcrumb items={[{ label: <i className="bi bi-house-door"></i>, to: `/${user?.user?.mode}/dashboard` }, { label: "Inbox messages" }]} />
        <div className="inbox-messages-all-preview d-flex flex-row" style={{ width: "100%", height: "70vh" }}>
      <AllMessages messages={allMessages?.messages || []} selectedOrderStatusIds={selectedOrderStatusIds} filterByCustomOffer={filterByCustomOffer} filterByStarred={filterByStarred} onApply={handleApplyFilters} />
      <MessageBox />
            </div>


    </div>
  );
}