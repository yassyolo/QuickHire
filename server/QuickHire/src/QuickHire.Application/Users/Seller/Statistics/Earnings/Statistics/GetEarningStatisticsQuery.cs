using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.Earnings.Statistics;

public record GetEarningStatisticsQuery(string? Range) : IQuery<IEnumerable<EarningStatisticsRowModel>>;
