import './Label.css';
export interface LabelProps {
    label: string;
}

export function Label({ label }: LabelProps) {
    return<div className="statistics-data-label" aria-label="statistics-data-label">{label}</div>
}