import './SideNavigation.css';

export interface SideNavigationItem {
  label: string;
  onClick: () => void;
  value: string; 
}

interface SideNavigationProps {
  items: SideNavigationItem[];
  active: string;  
}

export function SideNavigation({ items, active }: SideNavigationProps) {
  return (
    <div className="side-page-navigation" aria-label="side-navigation">
      {items.map((item) => (
        <div
          key={item.value}
          className={`side-page-navigation-item ${active === item.value ? 'selected' : ''}`}
          onClick={item.onClick}
          aria-label="side-navigation-item"
        >
          {item.label}
        </div>
      ))}
    </div>
  );
}
