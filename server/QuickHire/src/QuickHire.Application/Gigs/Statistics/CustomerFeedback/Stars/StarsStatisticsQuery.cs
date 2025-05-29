using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.Stars;

public record StarsStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;

