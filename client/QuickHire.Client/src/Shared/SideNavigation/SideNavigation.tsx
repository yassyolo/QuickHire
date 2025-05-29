import { useCallback, useState } from 'react';
import './SideNavigation.css';
export interface SideNavigationItem{
  label: string;
  onClick: () => void;
}

interface SideNavigationProps {
  items: SideNavigationItem[];
}

export function SideNavigation({ items }: SideNavigationProps) {
  const [selectedIndex, setSelectedIndex] = useState<number | null>(null);

  const handleClick = useCallback((index: number, onClick: () => void) => {
    setSelectedIndex(index); 
    onClick();  
  }, []);
  
    return (
        <div className="side-page-navigation" aria-label="side-navigation">
            {items.map((item, index) => (
                <div key={index} className={`side-page-navigation-item ${selectedIndex === index ? 'selected' : ''}`} onClick={() => handleClick(index, item.onClick)} aria-label="side-navigation-item">
                    {item.label}
                </div>
            ))}
        </div>
    );
}