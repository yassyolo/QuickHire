using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.Engagement.Views;

public record ViewStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;
