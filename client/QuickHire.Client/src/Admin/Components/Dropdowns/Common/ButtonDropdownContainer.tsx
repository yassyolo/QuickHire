interface ButtonDropdownContainerProps {
    children: React.ReactNode;
}

export function ButtonDropdownContainer({children} : ButtonDropdownContainerProps) {
    return <div className="dropdown-container">{children}</div>
}