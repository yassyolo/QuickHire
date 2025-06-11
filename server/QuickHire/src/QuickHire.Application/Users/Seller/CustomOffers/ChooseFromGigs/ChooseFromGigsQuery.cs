using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.CustomOffers;

namespace QuickHire.Application.Users.Seller.CustomOffers.ChooseFromGigs;

public record ChooseFromGigsQuery() : IQuery<List<ChooseFromGigsModel>>;