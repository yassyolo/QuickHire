import './SellerPage.css';
interface SellerPageProps {
    children: React.ReactNode;
}
export function SellerPage({ children }: SellerPageProps) {
    return <div className="seller-page-container">{children}</div>
}