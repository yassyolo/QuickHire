import { Link } from "react-router-dom";
import './Breadcrumb.css'; 
import React from "react";

export interface BreadcrumbItem {
  label: React.ReactNode;
  to?: string; 
}

interface BreadcrumbProps {
  items: BreadcrumbItem[];
}

export function Breadcrumb({ items } : BreadcrumbProps) {
  return (
    <nav className="breadcrumb-container" aria-label="breadcrumb">
      {items.map((item, index) => {
        const isLast = index === items.length - 1;
        return (
          <span key={index}>
            {item.to !== undefined ? 
            (<Link to={item.to} className={`breadcrumb-link ${isLast ? "last" : ""}`}>{item.label}</Link>) :
            (<span className="breadcrumb-label">{item.label}</span>)}           
            {!isLast && <span className="breadcrumb-separator"> / </span>}
          </span>
        );
      })}
    </nav>
  );
};
