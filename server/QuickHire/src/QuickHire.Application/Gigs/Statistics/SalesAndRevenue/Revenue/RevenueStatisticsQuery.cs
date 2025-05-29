using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.Revenue;

public record RevenueStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;

