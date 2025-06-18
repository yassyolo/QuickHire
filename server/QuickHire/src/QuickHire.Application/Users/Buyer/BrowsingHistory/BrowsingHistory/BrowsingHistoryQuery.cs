using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Buyer.BrowsingHistory.BrowsingHistory;

public record BrowsingHistoryQuery() : IQuery<IEnumerable<GigCardModel>>;

