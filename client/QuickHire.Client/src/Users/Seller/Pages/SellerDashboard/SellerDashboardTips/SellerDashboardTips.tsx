import { useState } from "react";
import { NoPagesPagination } from "../../../../../Shared/Pagination/NoPagesPagination/NoPagesPagination";
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

  const handlePageChange = (page: number) => {
    setCurrentTipIndex(page);
  };

  const currentTip = tips[currentTipIndex];

  return (
    <div className="seller-dashboard-tips-wrapper">
              <NoPagesPagination totalPages={tips.length} currentPage={currentTipIndex}  onPageChange={handlePageChange} />
      <div className="seller-dashboard-tips">
        <SellerDashboardTipItem tip={currentTip.tip} rightAligned={currentTip.rightAligned} photoUrl={currentTip.photoUrl} />
      </div>
    </div>
  );
}
