using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Buyer.FirstPage.RecentlyLiked;

public record RecentlyLikedQuery() : IQuery<List<GigCardModel>>;
