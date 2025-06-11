using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.CustomOffers;

namespace QuickHire.Application.Users.Seller.CustomOffers.ChooseFromInclusives;

public record ChooseFromInclusivesQuery() : IQuery<List<InclusivesModel>>;
