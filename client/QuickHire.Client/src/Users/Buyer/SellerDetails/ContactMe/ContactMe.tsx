import { useState } from "react";
import { ActionButton } from "../../../../Shared/Buttons/ActionButton/ActionButton";
import "../SellerDetailsPage.css";
import axios from "../../../../axiosInstance";
import { useAuth } from "../../../../AuthContext";


export function ContactMe() {
    const [showMessageSellerModal, setShowMessageSellerModal] = useState(false);
    const auth = useAuth();
    const [message, setMessage] = useState("");
  const [file, setFile] = useState<File | null>(null);
    const handleShowMessageSellerModal = () => {
        setShowMessageSellerModal(true);
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

    auth.signalRConnection?.invoke(
  "SendMessage",
  message,      
  null,          
  fileUrl,       
  null,         
  "1105754e-d70b-4a5c-9b62-da96e2038a5f" 
);

    setMessage("");
    setFile(null);

  } catch (error) {
    console.error("Error sending message:", error);
  }
};
    return (
        <>
            <div className="contact-me-container">
                <div className="contact-me-description">If you have any questions or want to discuss a project, feel free to reach out!</div>
                <ActionButton  text={<><i className="bi bi-send"></i> Contact me</>}
                    onClick={handleShowMessageSellerModal} className={"contact-me-button"} ariaLabel={"Contact Buyer button"}></ActionButton>
            </div>
            {showMessageSellerModal && 
                <div className="message-seller-modal-wrapper">
                     <input
            type="file"
            onChange={e => setFile(e.target.files ? e.target.files[0] : null)}
          />
          <input
            type="text"
            className="form-control"
            placeholder="Type your message..."
            value={message}
            onChange={e => setMessage(e.target.value)}
          />
          <button className="btn btn-primary" onClick={handleSendMessage}>Send</button>
                </div>
            }
        </>
    )
}