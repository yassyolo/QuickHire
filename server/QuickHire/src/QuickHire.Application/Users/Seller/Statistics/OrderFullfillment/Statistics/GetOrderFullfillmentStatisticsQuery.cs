using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.OrderFullfillment.Statistics;

public record GetOrderFullfillmentStatisticsQuery(string? Range) : IQuery<IEnumerable<OrderFullfillmentRowModel>>;
