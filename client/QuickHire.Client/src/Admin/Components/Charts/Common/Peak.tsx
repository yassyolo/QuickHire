import './Peak.css';
export interface PeakProps{
    date: string;
}

export function Peak({ date }: PeakProps) {
    return (
        <div className="peak" aria-label="peak">
            <div id={"peak-date"}className="peak-date">Peak was: {date}</div>
        </div>
    );
}