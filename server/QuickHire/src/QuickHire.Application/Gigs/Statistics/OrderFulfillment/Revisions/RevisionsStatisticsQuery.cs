using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.Revisions;

public record RevisionsStatisticsQuery(int Id) : IQuery<PieChartDataModel>;
