using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Buyer.FirstPage.RecentlyViewed;

public record RecentlyViewedQuery() : IQuery<List<GigCardModel>>;
