using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.Engagement.Likes;

public record LikesStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;
