import { Modal } from "../../../Admin/Components/Modals/Common/Modal";
import "./AuthenticationCard.css";
import { ImageSubTextItem } from "../ImageSubTextItem";

interface AuthenticationCardProps {
    children: React.ReactNode;
}

export function AuthenticationCard({ children }: AuthenticationCardProps) {
    return (
        <Modal className="register-modal">
        <div className="authentication-card">
            <div className="image-card">
                <img src="https://fiverr-res.cloudinary.com/npm-assets/layout-service/standard.0638957.png" alt="QuickHire Logo" className="logo-image" />
                <div className="image-text">Success starts here</div>
                <div className="image-subtext-items">
                 <ImageSubTextItem text="Quality work done faster" />
                <ImageSubTextItem text="Join millions that use QuickHire" />
                <ImageSubTextItem text="Get started in minutes" />
                </div>

            </div>
            <div className="card-content">           
                {children}
               <div className="card-bottom">By joining, you agree to the Fiverr Terms of Service and to occasionally receive emails from us.</div>   
            </div>   
        </div>
        </Modal>
    );
}