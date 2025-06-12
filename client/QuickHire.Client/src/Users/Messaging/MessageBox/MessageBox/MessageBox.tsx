import { useEffect, useRef, useState } from "react";
import axios from "../../../../axiosInstance";
import { MessageItem as TextMessageItem } from "../Messages/Common/MessageItem";
import { FileMessage } from "../Messages/File/FileMessage";
import { CustomOfferMessage, CustomOfferPayload } from "../Messages/CustomOfferMessage/CustomOfferMessage";
import "./MessageBox.css";
import { useAuth } from "../../../../AuthContext";

interface MessageBoxProps {
  id: number;
}

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
  payload?: CustomOfferPayload | null;
  fileUrl?: string | null;
}

interface ParticipantBInfo {
  profilePictureUrl: string;
  fullName: string;
  country: string;
  username: string;
  languages: string[];
  memberSince: string;
}

interface CurrentOrder {
  id: number;
  imageUrl: string;
  price: string;
  dueOn: string;
  status: string;
}

export interface NewMessageSignalRDto {
  conversationId: number;
  message: MessageItem;
  conversationPreview: {
    content: string;
    id: number;
    isRead: boolean;
    isStarred: boolean;
    senderProfilePictureUrl: string;
    senderUsername: string;
    timestamp: string;
  };
}

export function MessageBox({ id }: MessageBoxProps) {
  const [conversation, setConversation] = useState<Conversation | null>(null);
  const [message, setMessage] = useState("");
  const [file, setFile] = useState<File | null>(null);
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const auth = useAuth();

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  const fetchConversation = async () => {
    try {
      const { data } = await axios.get<Conversation>(`https://localhost:7267/messages/${id}`);
      setConversation(data);
      console.log("Fetched conversation:", data);
    } catch (error) {
      console.error("Error fetching conversation:", error);
    }
  };

  const handleSendMessage = async () => {
    if (!message && !file) return;

    try {
      let fileUrl = null;

      if (file) {
        const formData = new FormData();
        formData.append("file", file);

        const { data } = await axios.post<{ fileUrl: string }>(
          "https://localhost:7267/files/upload",
          formData,
          { headers: { "Content-Type": "multipart/form-data" } }
        );

        fileUrl = data.fileUrl;
      }

      if (conversation) {
        auth.signalRConnection?.invoke("SendMessage", message, conversation.id, fileUrl, null, null);
      }

      setMessage("");
      setFile(null);
    } catch (error) {
      console.error("Error sending message:", error);
    }
  };

  const handleStarConversation = async () => {
    if (!conversation) return;

    try {
      const url = `https://localhost:7267/messages/star/${conversation.id}`;
      const response = await axios.post(url);
      if (response.status === 200) {
        setConversation(prev => prev ? { ...prev, isStarred: !prev.isStarred } : prev);
      }
    } catch (error) {
      console.error("Error starring conversation:", error);
    }
  };

  useEffect(() => {
    fetchConversation();
  }, [id]);

  useEffect(() => {
    scrollToBottom();
  }, [conversation?.messages]);

  useEffect(() => {
    let unsubscribe: (() => void) | undefined;

    if (typeof auth.registerOnReceiveMessage === "function") {
      const maybeUnsub = auth.registerOnReceiveMessage((incoming: unknown) => {
        if (
          typeof incoming === "object" &&
          incoming !== null &&
          "conversationId" in incoming &&
          "message" in incoming &&
          "conversationPreview" in incoming
        ) {
          const dto = incoming as NewMessageSignalRDto;
          console.log("Received new message:", dto.message);

           setConversation(prev =>
  prev
    ? {
        ...prev,
        messages: [...prev.messages, { ...dto.message }]
      }
    : prev
);
          
        }
      });

      if (typeof maybeUnsub === "function") unsubscribe = maybeUnsub;
    }

    return () => {
      if (typeof unsubscribe === "function") unsubscribe();
    };
  }, [auth, id]);

return (
  <div className="message-box d-flex flex-row">
    <div className="message-box-contents d-flex flex-column">
      <div className="message-box-top flex-row d-flex justify-content-between">
        <div className="sender-username">{conversation?.participantBInfo.username}</div>
        <button onClick={handleStarConversation} className="star-button">
          <i className={`bi ${conversation?.isStarred ? "bi-star-fill" : "bi-star"}`} />
        </button>
      </div>

      <div className="messages">
        {conversation?.messages.map(msg => {
          switch (msg.messageType) {
            case "text":
              return (
                <TextMessageItem
                  key={msg.id}
                  senderProfilePictureUrl={msg.senderProfilePictureUrl}
                  senderUsername={msg.senderUsername}
                  content={msg.content}
                  timestamp={msg.timestamp}
                />
              );
            case "customoffer":
              return (
                <CustomOfferMessage
                  key={msg.id}
                  senderProfilePictureUrl={msg.senderProfilePictureUrl}
                  senderUsername={msg.senderUsername}
                  content={msg.content}
                  timestamp={msg.timestamp}
                  payload={msg.payload ?? {
                    gigTitle: "",
                    gigId: 0,
                    offerAmount: "",
                    includes: [],
                    offerId: 0,
                    senderUsername: ""
                  }}
                />
              );
            case "fileinclude":
              return (
                <FileMessage
                  key={msg.id}
                  fileUrl={msg.fileUrl ?? ""}
                  senderProfilePictureUrl={msg.senderProfilePictureUrl}
                  senderUsername={msg.senderUsername}
                  content={msg.content}
                  timestamp={msg.timestamp}
                />
              );
            default:
              return null;
          }
        })}
        <div ref={messagesEndRef} />
      </div>

      <div className="message-input-container d-flex flex-row align-items-center">
        <label htmlFor="photo-upload" className="upload-icon-label">
          <i className="bi bi-image" style={{ fontSize: "1.3rem" }}></i>
        </label>
        <input
          id="photo-upload"
          type="file"
          accept="image/*"
          onChange={e => setFile(e.target.files ? e.target.files[0] : null)}
          style={{ display: "none" }}
        />

        <input
          type="text"
          className="form-control"
          placeholder="Type your message..."
          value={message}
          onChange={e => setMessage(e.target.value)}
        />
        <button className="send-button" onClick={handleSendMessage}>
          Send
        </button>
      </div>
    </div>

    <div className="participant-b-info-order d-flex flex-column">
      {conversation?.currentOrder && (
        <div className="current-order d-flex flex-column">
          <div className="current-order-header">Order</div>
          <div className="current-order-status">{conversation.currentOrder.status}</div>
          <div className="d-flex flex-row current-order-item">
            <img
              src={conversation.currentOrder.imageUrl}
              alt="Order"
              className="current-order-image"
            />
            <div className="current-order-details d-flex flex-column">
              <div className="current-order-price">{conversation.currentOrder.price}</div>
              <div className="current-order-due">Due on: {conversation.currentOrder.dueOn}</div>
            </div>
          </div>
        </div>
      )}

      <div className="participant-info d-flex flex-column justify-content-center align-items-center">
        <img src={conversation?.participantBInfo.profilePictureUrl || ""} alt="Profile" className="participant-profile-picture" style={{marginBottom: '10px' }}/>
        <div className="partificpant-names-country-languages d-flex flex-column">
          <div className="partifipant-username-full-name d-flex flex-column" style={{fontSize:'15px', fontWeight:'bold'}}>
            {conversation?.participantBInfo.fullName || ""}
            <div className="seller-username">@{conversation?.participantBInfo.username || ""}</div>
          </div>

          <div className="country-languages-participant d-flex flex-column justify-content-center align-items-center">
            {conversation?.participantBInfo.country && (
              <div className="seller-country-participant" style={{ fontSize:'14px'}}>
                <i className="bi bi-geo-alt"></i> {conversation?.participantBInfo.country}
              </div>
            )}
            {conversation?.participantBInfo.languages && conversation.participantBInfo.languages.length > 0 && (
              <div className="seller-languages d-flex ">
                {conversation.participantBInfo.languages.map((language) => (
                              <span className="language-name-participant" style={{marginLeft:'10px', marginTop: '5px', fontSize:'14px'}}> <i className="bi bi-chat"></i> {language}</span>

                ))}
              </div>
            )}
            
          </div>
        </div>
      </div>
    </div>
  </div>
)
}
