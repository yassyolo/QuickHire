using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.Orders;

public record OrdersStatisticsQuery(int Id) : IQuery<StatisticsLineChartModel>;
