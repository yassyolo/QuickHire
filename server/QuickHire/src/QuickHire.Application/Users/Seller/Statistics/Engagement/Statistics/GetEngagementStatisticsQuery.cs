using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.Engagement.Statistics;

public record GetEngagementStatisticsQuery(string? Range) : IQuery<IEnumerable<EngagementStatisticsRowModel>>;

