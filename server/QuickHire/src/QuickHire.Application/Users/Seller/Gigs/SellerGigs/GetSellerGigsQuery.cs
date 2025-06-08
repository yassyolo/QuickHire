using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Seller.Gigs.SellerGigs;

public record GetSellerGigsQuery(int Id) : IQuery<IEnumerable<GigCardModel>>;

