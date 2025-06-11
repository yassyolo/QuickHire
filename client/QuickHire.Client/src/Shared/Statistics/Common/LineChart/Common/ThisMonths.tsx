import { DetailsRow } from '../../../../../Admin/Components/Details/Common/CategoryDetails/DetailsRow/DetailsRow';
import './ThisMonths.css';
export interface ThisMonthProps {
    count: string;
    percentage: string;
    icon: React.ReactNode;
}

export function ThisMonth({ count, percentage, icon }: ThisMonthProps) {
    return (
        <div className="this-month" aria-label="this-month">
            <DetailsRow label={`${count} This Month`} value={`+${percentage}% since last month`} icon={icon} />
        </div>
    );
}