import './StatisticsTable.css';

export interface StatisticsData{
    totalItem: TotalItemModel;
    thisMonthItem: ThisMonthModel;
    peakItem: PeakModel;
    data: { month: string; value: string }[];
}

export interface TotalItemModel{
    label: string;
    count: string;
}

export interface ThisMonthModel{
    count: string;
    percentage: string;
}

export interface PeakModel{
    date: string;
}

export interface PieChartData{
    data: {label: string; value: string; percentage: string}[];
}

export interface StatisticsTableProps {
    children: React.ReactNode;
}

export function StatisticsTable({children}: StatisticsTableProps) {
    return <div className="statistics-table" aria-label="statistics-table">{children}</div>
}