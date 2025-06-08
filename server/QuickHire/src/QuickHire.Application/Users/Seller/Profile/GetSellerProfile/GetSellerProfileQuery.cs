using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.GetSellerProfile;

public record GetSellerProfileQuery() : IQuery<SellerProfileModel>;

