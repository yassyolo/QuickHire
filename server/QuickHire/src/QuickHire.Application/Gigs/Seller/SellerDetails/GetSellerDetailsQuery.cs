using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.SellerDetails;

namespace QuickHire.Application.Gigs.Seller.SellerDetails;

public record GetSellerDetailsQuery(int? Id, string? UserId) : IQuery<SellerDetailsForBuyerModel>;
