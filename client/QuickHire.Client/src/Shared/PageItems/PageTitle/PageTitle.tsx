import React from "react";
import { Breadcrumb } from "../Breadcrumb/Breadcrumb";
import './PageTitle.css';

export interface PageTitleProps {
  title: string;
  description: string;
  breadcrumbs: { label: React.ReactNode; to?: string }[];
}

export function PageTitle({ title, description, breadcrumbs }: PageTitleProps) {
  return (
    <div className="page-title">
      <Breadcrumb items={breadcrumbs} />
      <h1 className="page-title" aria-label={title}>{title}</h1>
      <div className="page-description">{description}</div>      
    </div>
  );
}