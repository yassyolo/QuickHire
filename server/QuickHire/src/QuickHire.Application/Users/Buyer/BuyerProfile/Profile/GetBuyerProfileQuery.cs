using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.BuyerProfile.Profile;

public record GetBuyerProfileQuery() : IQuery<BuyerProfileModel>;
