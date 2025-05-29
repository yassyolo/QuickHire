interface AdminPageProps {
    children: React.ReactNode;
}
export function AdminPage({children}: AdminPageProps) {
    return <div className="admin-page" style={{ padding: '10px 50px' }}> {children}</div>
}