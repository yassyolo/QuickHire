import './StatisticsCardRow.css';
interface StatisticsCardRowProps {
    children: React.ReactNode;
}

export function StatisticsCardRow({ children }: StatisticsCardRowProps) {
    return <div className="statistics-card-row"> {children} </div>
}