import './SellerDashboardTipItem.css';

interface SellerDashboardTipProps {
    tip: string;
    rightAligned: boolean;
    photoUrl: string;
}
export function SellerDashboardTipItem({ tip, rightAligned, photoUrl }: SellerDashboardTipProps) {
    return (
        <div className={`seller-dashboard-tip ${rightAligned ? 'right-aligned' : ''}`}>
            <div className="tip-content">{tip} </div>
            {photoUrl && (
                <div className="tip-photo"> <img src={photoUrl} alt="Tip" /></div>
            )}
        </div>
    );
}