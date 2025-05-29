import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

interface LineChartComponentProps {
    data: { month: string; value: string }[];
}

export function LineChartComponent({ data } : LineChartComponentProps) {
    return (
        <ResponsiveContainer width="100%" height={300}>
            <LineChart data={data}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="month" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Line type="monotone" dataKey="value" stroke="#1DBF73" activeDot={{ r: 8 }} />
            </LineChart>
        </ResponsiveContainer>
    );
};

