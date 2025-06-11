import './ProfileDropdown.css';
interface ProfileDropdownProps {
    children: React.ReactNode;
}

export function ProfileDropdown({ children }: ProfileDropdownProps) {
    return <div className="profile-dropdown">{children}</div>
}