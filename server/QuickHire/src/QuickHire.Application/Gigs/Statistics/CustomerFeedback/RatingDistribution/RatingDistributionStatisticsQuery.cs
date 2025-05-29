using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.RatingDistribution;

public record RatingDistributionStatisticsQuery(int Id) : IQuery<PieChartDataModel>;

