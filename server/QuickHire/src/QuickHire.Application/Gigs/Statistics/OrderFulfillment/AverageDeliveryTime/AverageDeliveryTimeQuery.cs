using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.AverageDeliveryTime;

public record AverageDeliveryTimeQuery(int Id) : IQuery<StatisticsLineChartModel>;
