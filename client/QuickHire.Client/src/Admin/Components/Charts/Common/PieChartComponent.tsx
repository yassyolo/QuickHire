import { useEffect } from "react";
import { PieChart, Pie, Cell, Tooltip, Legend } from "recharts";

export interface StatisticsPieChartProps {
  data: {label: string; value: string; percentage: string}[];
  colors?: string[];
}

export function PieChartComponent({
  data,
  colors = ["#1DBF73", "#FF8042", "#8884d8", "#0088FE", "#FFBB28"],
}: StatisticsPieChartProps) {
  const parsedData = data.map((item) => ({
    name: item.label,
    value: parseFloat(item.value),
  }));

  useEffect(() => {
    console.log("Data:", data);
  }, [data]);
useEffect(() => {
  console.log("Parsed Data:", parsedData);
}, [parsedData]);
  return (
    <div className="pie-chart" aria-label="pie-chart">
      <PieChart width={250} height={250}>
        <Pie data={parsedData} dataKey="value" nameKey="name" cx="50%" cy="50%" outerRadius={100} innerRadius={50} startAngle={90} endAngle={450}>
          {parsedData.map((_, index) => (
            <Cell key={`cell-${index}`} fill={colors[index % colors.length]} />
          ))}
        </Pie>
        <Tooltip />
        <Legend />
      </PieChart>
    </div>
  );
}
