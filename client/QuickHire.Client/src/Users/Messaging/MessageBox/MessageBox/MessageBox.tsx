import { useEffect, useRef, useState } from "react";
import axios from "../../../../axiosInstance";
import { SellerInfoCard } from "../../../Seller/Pages/SellerProfile/SellerProfileCard/SellerInfoCard";
import { UserLanguage } from "../../../Seller/Pages/SellerProfile/SellerProfile";
import { MessageItem } from "../Messages/Common/MessageItem";
import { FileMessage } from "../Messages/FileMessage";
import { CustomOfferMessage, CustomOfferPayload } from "../Messages/CustomOfferMessage/CustomOfferMessage";
import "./MessageBox.css";
/*interface MessageBoxProps {
    id: number;
}*/

interface Conversation {
    id: number;
    messages: MessageItem[];
    isStarred: boolean;
    participantBInfo: ParticipantBInfo;
    currentOrder?: CurrentOrder;
}

export interface MessageItem {
    id: number;
    senderProfilePictureUrl: string;
    content: string;
    timestamp: string;
    senderUsername: string;
    messageType: "text" | "customoffer" | "fileinclude";
    payload?: CustomOfferPayload;
    fileUrl?: string;
}

interface ParticipantBInfo {
        profilePictureUrl: string;
        fullName: string;
        country: string;
        username: string;
        languages: UserLanguage[];
        memberSince: string;
}

interface CurrentOrder{
    id: number;
    imageUrl: string;
    price: string;
    dueOn: string;
    status: string;
}



export function MessageBox(/*{ id }: MessageBoxProps*/) {
    const [conversation, setConversation] = useState<Conversation | null>(null);
    const messagesEndRef = useRef<HTMLDivElement>(null);

const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
};

useEffect(() => {
    scrollToBottom();
}, [conversation?.messages]);

    /*const fetchConversation = async () => {
        try {
            const url = `https://localhost:7267/messages/${id}`;
            const response = await axios.get<Conversation>(url);
            setConversation(response.data);
        }
        catch (error) {
            console.error("Error fetching conversation:", error);
        }
    }

    useState(() => {
        fetchConversation();
    });*/

   useEffect(() => {
    const mockConversation: Conversation = {
        id: 1,
        isStarred: false,
        participantBInfo: {
            profilePictureUrl: "https://randomuser.me/api/portraits/men/45.jpg",
            fullName: "John Doe",
            country: "USA",
            username: "johndoe",
            languages: [
                { languageName: "English", languageId: 1 },
                { languageName: "Spanish", languageId: 2 }
            ],
            memberSince: "2020-05-12"
        },
        currentOrder: {
            id: 101,
            imageUrl: "https://via.placeholder.com/150",
            price: "$250",
            dueOn: "2025-06-20",
            status: "In Progress"
        },
        messages: [
            {
                id: 1,
                senderProfilePictureUrl: "https://randomuser.me/api/portraits/men/32.jpg",
                senderUsername: "client123",
                content: "Hello, I wanted to ask about your web development services.",
                timestamp: "2025-06-09",
                messageType: "text"
            },
            {
                id: 2,
                senderProfilePictureUrl: "https://randomuser.me/api/portraits/women/44.jpg",
                senderUsername: "johndoe",
                content: "Of course! What type of website are you looking for?",
                timestamp: "2025-06-09T10:16:00Z",
                messageType: "text"
            },
            {
                id: 3,
                senderProfilePictureUrl: "https://randomuser.me/api/portraits/men/32.jpg",
                senderUsername: "client123",
                content: "I need an e-commerce site with payment integration.",
                timestamp: "2025-06-09",
                messageType: "text"
            },
            {
                id: 4,
                senderProfilePictureUrl: "https://randomuser.me/api/portraits/women/44.jpg",
                senderUsername: "johndoe",
                content: "Here's a custom offer for the project.",
                timestamp: "2025-06-09",
                messageType: "customoffer",
                payload: {
                    gigTitle: "Full-featured E-commerce Website",
                    gigId: 77,
                    offerAmount: "$1200",
                    includes: [
                        "Payment Integration",
                        "Admin Dashboard",
                        "Product Upload",
                        "Responsive Design"
                    ],
                    offerId: 501,
                    senderUsername: "johndoe"
                }
            },
            {
                id: 5,
                senderProfilePictureUrl: "https://randomuser.me/api/portraits/men/32.jpg",
                senderUsername: "client123",
                content: "Here's the logo for the site.",
                timestamp: "2025-06-09",
                messageType: "fileinclude",
                fileUrl: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAM1BMVEXk5ueutLeqsbTn6eqpr7PJzc/j5ebf4eLZ3N2wtrnBxsjN0NLGysy6v8HT1tissra8wMNxTKO9AAAFDklEQVR4nO2d3XqDIAxAlfivoO//tEOZWzvbVTEpic252W3PF0gAIcsyRVEURVEURVEURVEURVEURVEURVEURVEURVEURflgAFL/AirAqzXO9R7XNBVcy9TbuMHmxjN6lr92cNVVLKEurVfK/zCORVvW8iUBnC02dj+Wpu0z0Y6QlaN5phcwZqjkOkK5HZyPAjkIjSO4fIdfcOwFKkJlX4zPu7Ha1tIcwR3wWxyFhRG6g4Je0YpSPDJCV8a2Sv2zd1O1x/2WMDZCwljH+clRrHfWCLGK8REMiql//2si5+DKWKcWeAGcFMzzNrXC/0TUwQ2s6+LhlcwjTMlYsUIQzPOCb7YBiyHopyLXIEKPEkI/TgeuiidK/R9FniUDOjRDpvm0RhqjMyyXNjDhCfIMYl1gGjIMIuYsnGEYRMRZOMMunaLVwpWRW008v6fYKDIzxCwVAeNSO90BJW6emelYBRF/kHpYGVaoxTDAaxOFsfP9y8hpJ4xd7gOcij7JNGQ1EYFgkPJa1jQEiYZXRaRINKxSDUW9n+FT82lSKadkiru9/4XPqSLWOekGPoY05TAvLm9orm+YWuwHoBHkZKijNBJGmeb61eL6Ff/6q7bLr7yvv3vKGhpDRjvgjGaPz+gUg6YgcvpyAR2FIZ9U6nEEyZRTovmEU32KichpGn7C17XrfyH9gK/c0CMP05HZIM2uf9sEveizKveBy9/6Qt7o89ne33D525cfcIMW6ab+TMEukQbQbu+xu7X3A9bChmWaCeAkG17bpntwXgWxHaMzGPmUaR5dQZiKqRVeUZ3047fi3nAu28h4CHxCsZAgmEH8Y27jJAhm8c+5RQzRQNVGhVFSfxOYIjp/pP7RxzjevYXVGf4eLt+BJ1vCuLuLkrgABgCGXZ2wik5uty+oBvNirI6mkzhAf4Gsb58Hcm67Jzd+KwD10BYPLL3e0MjvKrgAULnOfveF/O4N2Xb9BZom3gJes3F9X5Zze8/6Yt09b4CrqsEjUv8oFBaR2rl+6CZr2xVrp24o/WitBKuGrrpl1+bFkmK2qXTON4VpbdfLa7o7y/WdLxG7lm2Lqh2clOwTegbvc/vj2U78CwhA87Bn8G5Nk3eOb0Nsr9flz3sG78UUtue4kpv1xvjg3TMay62BMlTlP+vrOMnJsRmt/ze0jsfkPPYdAH57hK+34PeOyc8XIXu5xT2HsUkdZz+adwg8HGFfQ3K5jtDvbUiO4Di9/ywHGrL88pDizZ++oTp+an+SMX/ndymUCwmHMdO7yuOx83pUx/eEMU0AvxWndwgidAqOZ8ypCwdEfvvEo6D9HwpA8wzvmOJEqAg9ySu8g4x0Hb9hSB/BANEKJ+LbPBU0lzbAJs4xt1AoshKkUGQmiH8/jJ0gdhTTLmSegHlPE0oOdXALnqDjKYh3px//fSgSWG8UqfrrIICzYYSJXRr9BSPbpNzw7gBjKjKOYI7ReIGqQRIap5+5MdjyvuDkExvGeXSlONWZAP3/AZBwJohU7QJRGU+cTVH18ELmRPNBmibW6MT/k1b0XhdkRBvyT6SB6EYv/GvhSmRNpGngRULsAlxMCGNXp7w3FfdEbTEEDdLI9TdIKRUzUesa3I461ER8cpNT7gMRhpKmYVS9ELOgCUQsa4SsulciKiLbY+AnHD8cpuhISsnxpamI84sbDq9qYJgf8wiiOBrC7Ml7M7ZECCqKoiiKoiiKoiiKoijv5AvJxlZRyNWWLwAAAABJRU5ErkJggg=="
            }
        ]
    };

    setConversation(mockConversation);
}, []);




    const handleStarMessage = async () => {
        if (!conversation) {
            console.error("No conversation loaded.");
            return;
        }
        try {
            const url = `https://localhost:7267/messages/star/${conversation.id}`;
            const response = await axios.post(url);
            if (response.status === 200) {

                console.log("Message starred successfully");
                setConversation(prev => {
                    if (!prev) return prev;
                    return {
                        ...prev,
                        isStarred: !prev.isStarred
                    };
                });
            }
        } catch (error) {
            console.error("Error starring message:", error);
        }
    };



    return (
        <div className="message-box d-flex flex-row">
            <div className="message-box-contents d-flex flex-column">
                 <div className="message-box-top flex-row d-flex justify-content-between">
                 <div className="sender-username">{conversation?.participantBInfo.username}</div>
                 <button onClick={handleStarMessage} className="star-button"><i className="bi bi-star"></i></button>
                </div>
                <div className="messages">
                    {conversation?.messages.map((message) => (
                        message.messageType === "text" ? (
                            <MessageItem
                                key={message.id}
                                senderProfilePictureUrl={message.senderProfilePictureUrl}
                                senderUsername={message.senderUsername}
                                content={message.content}
                                timestamp={message.timestamp}
                            />
                        ) : message.messageType === "customoffer" ? (
                            <CustomOfferMessage
                                key={message.id}
                                senderProfilePictureUrl={message.senderProfilePictureUrl}
                                senderUsername={message.senderUsername}
                                content={message.content}
                                timestamp={message.timestamp}
                                payload={message.payload ?? { gigTitle: "", gigId: 0, offerAmount: "", includes: [], offerId: 0, senderUsername: "" }}
                                />
                        ) : message.messageType === "fileinclude" ? (
                            <FileMessage fileUrl={message.fileUrl ?? ""} key={message.id}
                                senderProfilePictureUrl={message.senderProfilePictureUrl}
                                senderUsername={message.senderUsername}
                                content={message.content}
                                timestamp={message.timestamp}/>
                        ) : null
                    ))}
                        <div ref={messagesEndRef} />

                </div>
            </div>
           
            <div className="participant-b-info-order d-flex flex-column">
                {conversation?.currentOrder && 
                    <div className="current-order d-flex flex-column">
                        <div className="current-order-header">Order</div>
                        <div className="current-order-status">{conversation.currentOrder.status}</div>
                        <div className="d-flex flex-row current-order-item">
                            <img src={conversation.currentOrder.imageUrl} alt="Order" className="current-order-image" />
                            <div className="current-order-details d-flex flex-column">
                                <div className="current-order-price">{conversation.currentOrder.price}</div>
                                <div className="current-order-price">Due on: {conversation.currentOrder.dueOn}</div>
                            </div>
                        </div>
                    </div>
                }
                <SellerInfoCard sellerDetails={conversation && conversation.participantBInfo ? conversation.participantBInfo : null} showActions={false} memberSince={conversation?.participantBInfo?.memberSince}/>                
            </div>
        </div>
    );
}