using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Statistics;

namespace QuickHire.Application.Users.Seller.Statistics.RepeatBusiness.Statistics;

public record GetRepeatBusinessStatisticsQuery(string? Range) : IQuery<IEnumerable<RepeatBusinessRowModel>>;

