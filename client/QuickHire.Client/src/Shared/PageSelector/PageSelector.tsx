import "./PageSelector.css";


export interface PageSelectorProps {
  data: PageOption[];
}

export interface PageOption{
    name: string;
    onClick: () => void;
    isActive: boolean;
}

export function PageSelector({data}: PageSelectorProps) {

  return (
    <div className="page-selector">
      {data && data.length > 0 && (
        <div className="page-items">
          {data.map((item, index) => (
            <div key={index}  className={`page-pill ${item.isActive ? 'active' : ''}`} onClick={item.onClick}>  {item.name} </div>
          ))}
        </div>
      )}
    </div>   
  );
}
