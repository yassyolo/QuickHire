import React from "react";

import { SortingChart } from "../../Common/MultipleLineChart/SortingChart";
import { StatisticsCardRow } from "../../Common/StatisticsCards/StatisticsCardRow";
import { StatisticsCardRowItem } from "../../Common/StatisticsCards/StatisticsCardRowItem";

export interface StatisticCard {
  title: string;
  value: string;
}

interface StatisticsSectionProps {
  statisticCards: StatisticCard[];
  lines: { key: string; label: string; color: string }[];
  data: { date: string; [key: string]: number | string }[];
  onRangeChange: (range: string) => void;
}

export function StatisticsSection({ statisticCards, lines, data, onRangeChange,}: StatisticsSectionProps) {
  return (
    <div className="statistics-section">
      <StatisticsCardRow>
        {statisticCards.map((card, index) => (
          <React.Fragment key={index}>
            <StatisticsCardRowItem value={card.value} label={card.title} />
            {index < statisticCards.length - 1 && (
              <div className="separator" style={{ width: 1,  height: 40, backgroundColor: "#ddd",  margin: "0 20px", }} />
            )}
          </React.Fragment>
        ))}
      </StatisticsCardRow>
      <SortingChart lines={lines} data={data} onRangeChange={onRangeChange} />
    </div>
  );
}
