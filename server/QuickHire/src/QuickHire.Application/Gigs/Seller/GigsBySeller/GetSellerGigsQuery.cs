using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Gigs.Seller.SellerGigs;

public record GetSellerGigsQuery(int Id) : IQuery<IEnumerable<GigCardModel>>;

