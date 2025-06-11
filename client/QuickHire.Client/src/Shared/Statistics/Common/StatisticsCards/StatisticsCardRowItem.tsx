import './StatisticsCardRowItem.css';
interface StatisticsCardRowItemProps {
    label: string;
    value: string | number;
}

export function StatisticsCardRowItem({ label, value }: StatisticsCardRowItemProps) {
    return (
        <div className="statistics-card-row-item">
            <div className="statistics-card-label">{label}</div>
            <div className="statistics-card-value">{value}</div>
        </div>
    );
}