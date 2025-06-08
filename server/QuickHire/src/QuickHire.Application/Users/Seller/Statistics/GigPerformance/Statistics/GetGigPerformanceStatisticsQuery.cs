using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.GigPerformance.Statistics;

public record GetGigPerformanceStatisticsQuery(string? Range) : IQuery<IEnumerable<GigPerformanceRowModel>>;

