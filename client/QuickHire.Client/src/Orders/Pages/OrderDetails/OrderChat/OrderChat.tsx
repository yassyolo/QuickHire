import { useEffect, useRef, useState } from "react";
import { useAuth } from "../../../../AuthContext";
import { RevisionMessage } from "../../../../Users/Messaging/MessageBox/Messages/Revision/RevisionMessage";
import { FileMessage } from "../../../../Users/Messaging/MessageBox/Messages/File/FileMessage";
import { MessageItem } from "../../../../Users/Messaging/MessageBox/Messages/Common/MessageItem";
import { CustomOfferPayload } from "../../../../Users/Messaging/MessageBox/Messages/CustomOfferMessage/CustomOfferMessage";
import { DeliveryMessage } from "../../../../Users/Messaging/MessageBox/Messages/Delivery/DeliveryMessage";
import axios from "../../../../axiosInstance";
import { SendWorkModal } from "../../../SendWork/SendWorkModal";
import { uploadFile } from "../../../../Users/Messaging/MessageBox/MessageBox/Message";
import { useNavigate } from "react-router-dom";

export interface RevisionPayload {
    attachment: string;
    description: string;
    revisionNumber: number;
    revisionId: number;
}

export interface DeliveryPayload {
    attachment: string;
    description: string;
    sourceFileUrl: string;
}

interface SendWorkResponse{
    delivery?: DeliveryPayload;
    revision?: RevisionPayload;
}

interface MessageBoxProps {
  id: number;
  orderId: number;
}

export interface Conversation {
  id: number;
  messages: MessageItem[];
}

export interface MessageItem {
  id: number;
  senderProfilePictureUrl: string;
  content: string;
  timestamp: string;
  senderUsername: string;
  messageType: "text" | "customoffer" | "fileinclude" | "revision" | "delivery";
  payload?: CustomOfferPayload | RevisionPayload | DeliveryPayload | null;
  fileUrl?: string | null;
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

export function OrderChat({ id, orderId }: MessageBoxProps) {
    const user = useAuth().user;
    const navigate = useNavigate();
  const [conversation, setConversation] = useState<Conversation | null>(null);
  const [message, setMessage] = useState("");
  const [file, setFile] = useState<File | null>(null);
  const messagesEndRef = useRef<HTMLDivElement>(null);
  const auth = useAuth();
  const [showSendWorkModal, setShowSendWorkModal] = useState(false);
  const handleSendWorkModalVisibility = () => {
    setShowSendWorkModal(!showSendWorkModal);
    }

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  const loadConversation = async () => {
    try {
  const { data } = await axios.get<Conversation>(`https://localhost:7267/messages/order/${id}`);
     const filteredMessages = Array.isArray(data.messages)
        ? (data.messages as MessageItem[])
            .filter(
              (msg: MessageItem) =>
                msg.messageType !== "customoffer"
            )
        : [];
      setConversation({
        ...data,
        messages: filteredMessages,
      });
    } catch (error) {
      console.error("Error loading conversation:", error);
    }
  };

  const handleSendWork = (response: SendWorkResponse, type: "delivery" | "revision") => {
  if (!conversation) return;

  const payload = type === "delivery" ? response.delivery : response.revision;
  if (!payload) return;

  const payloadJson = JSON.stringify(payload);

  const payloadType = type === "delivery" ? 4 : 3; 
  
  auth.signalRConnection?.invoke(
    "SendMessage",
    "New Work Submitted",
    conversation.id,
    null, 
    payloadJson,
    payloadType, 
    null
  );

  if (type === "delivery") {
    if( user?.mode === "buyer") {
      navigate(`/buyer/orders/${orderId}`);
    }
    if( user?.mode === "seller") {
      navigate(`/seller/orders/${orderId}`);
    }
  }

};

  const handleSendMessage = async () => {
    if (!message && !file) return;

    try {
      const fileUrl = file ? await uploadFile(file) : null;

      if (conversation) {
        auth.signalRConnection?.invoke("SendMessage", message, conversation.id, fileUrl, null, null, null);
      }

      setMessage("");
      setFile(null);
    } catch (error) {
      console.error("Error sending message:", error);
    }
  };



  useEffect(() => {
    loadConversation();
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

          setConversation(prev =>
            prev
              ? {
                  ...prev,
                  messages: [...prev.messages, { ...dto.message }],
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
    <div className="message-box d-flex flex-row" style={{ height: "70%", width: "70%" }}>
      <div className="message-box-contents d-flex flex-column">
        <div className="message-box-top flex-row d-flex justify-content-between" style={{fontSize: "16px", fontWeight: "bold"}}>
            Order activity
        </div>

        <div className="messages">
          {conversation?.messages.map(msg => {
            switch (msg.messageType) {
              case "text":
                return (
                  <MessageItem
                                      key={msg.id}
                                      senderProfilePictureUrl={msg.senderProfilePictureUrl}
                                      senderUsername={msg.senderUsername}
                                      content={msg.content}
                                      timestamp={msg.timestamp}
                                    />
                );
              case "delivery":
                return (
                  <DeliveryMessage
                    key={msg.id}
                    senderProfilePictureUrl={msg.senderProfilePictureUrl}
                    senderUsername={msg.senderUsername}
                    content={msg.content}
                    timestamp={msg.timestamp}
                    payload={
                      (msg.payload as DeliveryPayload) ?? {
                        attachment: "",
                        description: "",
                        sourceFileUrl: ""
                      }
                    }
                  />
                );
                case "revision":   
    return (
      <RevisionMessage
        key={msg.id}
        senderProfilePictureUrl={msg.senderProfilePictureUrl}
        senderUsername={msg.senderUsername}
        content={msg.content}
        timestamp={msg.timestamp}
        payload={
          (msg.payload as RevisionPayload) ?? {
            attachment: "",
            description: "",
            revisionNumber: 0,
            revisionId: 0,
          }
        }
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
{file && (
  <div className="image-preview" style={{ margin: "10px 0" }}>
    <img
      src={URL.createObjectURL(file)}
      alt="Preview"
      style={{ width: "250px", maxHeight: "150px", borderRadius: "8px", marginLeft: "20px" , marginRight: "10px"}}
    />
    <button onClick={() => setFile(null)} className="btn btn-sm btn-danger" style={{ marginTop: "5px" }}>
      X
    </button>
  </div>
)}
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

        <button onClick={handleSendWorkModalVisibility} className="send-work-button"  style={{
    backgroundColor: 'white',
    color: '#1DBF73',
    border: '1px solid #1DBF73 !important',
    padding: '8px 20px',
    borderRadius: '5px',
    fontSize: '16px',
    boxShadow: 'none !important',
    fontWeight: 600,
    cursor: 'pointer',
    width: '150px',
    marginTop: '10px',
    marginBottom: '10px',
    marginLeft: '10px',
    
  }} >Send work</button>
      </div>
{showSendWorkModal && (
  <SendWorkModal
    onClose={handleSendWorkModalVisibility}
    orderId={orderId}
    onWorkSubmitted={handleSendWork} 
  />
)}
  
    </div>
  );
}
