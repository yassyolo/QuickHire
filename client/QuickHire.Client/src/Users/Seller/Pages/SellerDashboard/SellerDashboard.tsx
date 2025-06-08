import { useEffect, useState } from "react";
import axios from "../../../../axiosInstance";
import { SellerPage } from "../Common/SellerPage";
import './SellerDashboard.css';
import { Link } from "react-router-dom";
import { SellerDashboardTips } from "./SellerDashboardTips/SellerDashboardTips";
import { SellerDashboardOrders } from "./SellerDashboardOrders/SellerDashboardOrders";

interface SellerDashboard {
  profilePictureUrl: string;
  name: string;
  username: string;
  averageRating: number;
  totalReviews: number;
  totalOrders: number;
  thisMonth: string;
  earningsThisMonth: string;
}

export interface SellerDashboardOrder{
  imageUrl: string;
  title: string;
  status: string;
  dueIn: string;
  price: string;
  id: number;
}

export function SellerDashboard() {
  const [sellerDashboard, setSellerDashboard] = useState<SellerDashboard | null>(null);
 const fetchSellerDashboard = async () => {
    try {
      const response = await axios.get<SellerDashboard>("https://localhost:7267/seller/dashboard");
      setSellerDashboard(response.data);
    } catch (error) {
      console.error("Error fetching seller dashboard:", error);
    }
  }

  useEffect(() => {
    fetchSellerDashboard();
  }, []);

  return (
    <SellerPage>
<div className="seller-dashboard-page d-flex flex-row">
    <div className="seller-dashboard-left d-flex flex-column">
          <div className="seller-dashboard-info">
        <div className="seller-dashboard-image-wrapper">
          <img src={sellerDashboard?.profilePictureUrl} alt="Profile" className="seller-dashboard-image"/>
        </div>
        <div className="seller-dashboard-info-name">{sellerDashboard?.name}</div>
        <div className="seller-dashboard-info-username">@{sellerDashboard?.username}</div>
        <Link to="/seller/profile" className="seller-dashboard-edit-profile-link">View profile</Link>
      </div>

      <div className="seller-dashboard-overview">
        <div className="seller-dashboard-overview-title">Overview</div>

        <div className="seller-dashboard-overview-item">
          <div className="seller-dashboard-overview-item-title">Rating</div>
          <div className="seller-dashboard-overview-item-value">
            {sellerDashboard?.averageRating !== undefined && sellerDashboard?.totalReviews > 0
              ? sellerDashboard.averageRating.toFixed(1)
              : "-"}
          </div>
        </div>

        <div className="seller-dashboard-overview-item">
          <div className="seller-dashboard-overview-item-title">Reviews</div>
          <div className="seller-dashboard-overview-item-value">
                            <i className="fa-solid fa-star"></i>

            {sellerDashboard?.totalReviews !== undefined && sellerDashboard.totalReviews > 0
              ? sellerDashboard.totalReviews
              : "-"}
          </div>
        </div>

        <div className="seller-dashboard-overview-item">
          <div className="seller-dashboard-overview-item-title">Orders</div>
          <div className="seller-dashboard-overview-item-value">{sellerDashboard?.totalOrders}</div>
        </div>
      </div>

      <div className="seller-dashboard-earnings d-flex flex-row justify-content-between align-items-center">
        <div className="seller-dashboard-earnings-title">Earnings in {sellerDashboard?.thisMonth}</div>
        <div className="seller-dashboard-earnings-item">
          ${sellerDashboard?.earningsThisMonth ?? "0"}
        </div>
      </div>

      <div className="seller-dashboard-inbox d-flex flex-row justify-content-between align-items-center">
        <div className="seller-dashboard-inbox-title">Inbox</div>
        <Link to="/seller/inbox" className="seller-dashboard-inbox-link">View all</Link>
      </div>
</div>
<div className="seller-dashboard-right">
    <div className="seller-dashboard-welcome">
        <div className="seller-dashboard-welcome-title">Welcome back, {sellerDashboard?.name}!</div>
        <div className="seller-dashboard-welcome-subtitle">Find important messages, tips, and links to helpful resources here:</div>
    </div>

    <div className="seller-dashboard-tips-container">
        <SellerDashboardTips tips={[
    {
        tip: "Remember to respond to customer inquiries promptly to maintain a good rating.",
        rightAligned: true,
        photoUrl: "https://cdn.builder.io/api/v1/image/assets%2F1269a57212df4631b866219ba2013fa8%2F34ab1ecc3d5f4981830efd9977d9c858?format=webp&width=2000"
    },
    {
        tip: "Keep your gig descriptions clear and concise. Well-written content builds trust and boosts conversions.",
        rightAligned: false,
        photoUrl: "https://cdn.builder.io/api/v1/image/assets%2F1269a57212df4631b866219ba2013fa8%2F97bfaa0cf24148808b1312f515241131?format=webp&width=2000"
    },
    {
        tip: "Update your profile picture and bio regularly to reflect your current skills and services.",
        rightAligned: false,
        photoUrl: "https://cdn.builder.io/api/v1/image/assets%2F1269a57212df4631b866219ba2013fa8%2F7fb76b02857d42afbdcfe9c568498e6d?format=webp&width=2000"
    }
]} />
        
    </div>
    <SellerDashboardOrders />

</div>
</div>
    </SellerPage>
  );
}
