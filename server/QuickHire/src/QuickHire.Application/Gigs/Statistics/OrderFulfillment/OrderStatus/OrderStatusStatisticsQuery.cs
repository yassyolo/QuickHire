using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.OrderStatus;

public record OrderStatusStatisticsQuery(int Id) : IQuery<PieChartDataModel>;
