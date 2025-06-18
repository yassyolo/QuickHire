using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.CustomOffers;

namespace QuickHire.Application.CustomOffers.Seller.ChooseFromGigs;

public record ChooseFromGigsQuery() : IQuery<List<ChooseFromGigsModel>>;