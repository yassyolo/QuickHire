using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.AverageOrderValue;

public record AverageOrderValueStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;

