using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.Reviews;

public record ReviewsStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;

