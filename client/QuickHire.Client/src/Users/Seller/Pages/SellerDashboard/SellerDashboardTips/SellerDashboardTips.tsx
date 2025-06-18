import { useState } from "react";
import { NoPagesPagination } from "../../../../../Shared/PageItems/Pagination/NoPagesPagination/NoPagesPagination";
import { SellerDashboardTipItem } from "./SellerDashboardTipItem"; 

interface SellerDashboardTipProps {
  tips: Tip[];
}

interface Tip {
  tip: string;
  rightAligned: boolean;
  photoUrl: string;
}

export function SellerDashboardTips({ tips }: SellerDashboardTipProps) {
  const [currentTipIndex, setCurrentTipIndex] = useState(0);

  const currentTip = tips[currentTipIndex];

  return (
    <div className="seller-dashboard-tips-wrapper">
<NoPagesPagination 
  totalPages={tips.length} 
  currentPage={currentTipIndex + 1} 
  onPageChange={(page) => setCurrentTipIndex(page - 1)} 
/>      <div className="seller-dashboard-tips">
        <SellerDashboardTipItem tip={currentTip.tip} rightAligned={currentTip.rightAligned} photoUrl={currentTip.photoUrl} />
      </div>
    </div>
  );
}
