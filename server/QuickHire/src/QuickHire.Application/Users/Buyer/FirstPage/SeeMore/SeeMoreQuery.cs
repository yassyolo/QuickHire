using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Buyer.FirstPage.SeeMore;

public record SeeMoreQuery() : IQuery<List<GigCardModel>>;
