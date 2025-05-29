using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.ReviewResponseRate;

public record ReviewResponseRateStatisticsQuery(int Id) : IQuery<PieChartDataModel>;

