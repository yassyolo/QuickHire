using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.Users.ProjectBriefs.BuyerProjectBriefs;

public record GetBuyerProjectBriefsQuery(string? Keyword, string? FromDate, string? ToDate) : IQuery<IEnumerable<BuyerProjectBriefModel>>;
