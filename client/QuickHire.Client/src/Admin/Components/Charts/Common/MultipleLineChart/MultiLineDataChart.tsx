import {ResponsiveContainer,
  LineChart, Line, XAxis,
  YAxis, CartesianGrid, Tooltip,
  Legend
} from "recharts";

interface LineDefinition {
  key: string;
  label: string;
  color: string;
}

interface MultiLineChartProps {
  data: { date: string; [key: string]: number | string }[];
  lines: LineDefinition[];
}

export function MultiLineChartComponent({ data, lines }: MultiLineChartProps) {
  return (
    <ResponsiveContainer width="100%" height={500}>
      <LineChart data={data}>
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="date" />
        <YAxis />
        <Tooltip />
        <Legend />
        {lines.map((line) => (
          <Line key={line.key} type="monotone" dataKey={line.key}
            stroke={line.color} name={line.label}
            activeDot={{ r: 6 }}/>
        ))}
      </LineChart>
    </ResponsiveContainer>
  );
}
