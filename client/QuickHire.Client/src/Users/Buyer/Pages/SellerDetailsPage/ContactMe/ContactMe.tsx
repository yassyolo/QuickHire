import { useState } from "react";
import "../SellerDetailsPage.css";
import "./ContactMe.css";
import { useAuth } from "../../../../../AuthContext";
import { ActionButton } from "../../../../../Shared/Buttons/ActionButton/ActionButton";
import { MessageItem } from "../../../../Messaging/MessageBox/Messages/Common/MessageItem";
import { IconButton } from "../../../../../Shared/Buttons/IconButton/IconButton";
import axios from "../../../../../axiosInstance";

interface ContactMeProps {
  userId: string;
}

export function ContactMe({ userId }: ContactMeProps) {
  const [showMessageSellerModal, setShowMessageSellerModal] = useState(false);
  const auth = useAuth();
  const [message, setMessage] = useState("");
  const [file, setFile] = useState<File | null>(null);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);

  const handleShowMessageSellerModal = () => {
    setShowMessageSellerModal((prev) => !prev);
    if (showMessageSellerModal) {
      setMessage("");
      setFile(null);
      setPreviewUrl(null);
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const selectedFile = e.target.files ? e.target.files[0] : null;
    setFile(selectedFile);
    if (selectedFile) {
      const reader = new FileReader();
      reader.onloadend = () => setPreviewUrl(reader.result as string);
      reader.readAsDataURL(selectedFile);
    } else {
      setPreviewUrl(null);
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

      await auth.signalRConnection?.invoke(
  "SendMessage",
  message,          
  null,             
  fileUrl,          
  null,             
  null,             
  userId           
);


      setMessage("");
      setFile(null);
      setPreviewUrl(null);
      setShowMessageSellerModal(false);
    } catch (error) {
      console.error("Error sending message:", error);
    }
  };

  return (
    <>
      <div className="contact-me-container">
        <div className="contact-me-description">
          If you have any questions or want to discuss a project, feel free to reach out!
        </div>
        <ActionButton
          text={<><i className="bi bi-send"></i> Contact me</>}
          onClick={handleShowMessageSellerModal}
          className="contact-me-button"
          ariaLabel="Contact Buyer button"
        />
      </div>

      {showMessageSellerModal && (
        <div className="message-seller-modal-wrapper">
          <div className="message-seller-modal-content d-flex flex-row justify-content-between">

            <div className="d-flex flex-column">
              {(previewUrl || message) && (
              <div className="message-preview-section">
                <MessageItem
                  senderProfilePictureUrl={auth.user?.profilePictureUrl || ""}
                  senderUsername={auth.user?.email || "You"}
                  content={message}
                  timestamp={"Now"}
                >
                  {previewUrl && (
                    <div className="file-message">
                      <img className="file-icon" src={previewUrl} alt="Image Preview" />
                    </div>
                  )}
                </MessageItem>
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
                onChange={handleFileChange}
                style={{ display: "none" }}
              />

              <input
                type="text"
                className="form-control"
                placeholder="Type your message..."
                value={message}
                onChange={(e) => setMessage(e.target.value)}
              />

              <button className="send-button" onClick={handleSendMessage}>
                Send
              </button>
            </div>
            </div>
 <IconButton
              icon={<i className="bi bi-x"></i>}
              onClick={handleShowMessageSellerModal}
              className="close-button"
              ariaLabel="Close message modal"
            />
           
          </div>
          
        </div>
      )}
    </>
  );
}
