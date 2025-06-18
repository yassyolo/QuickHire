using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Shared;

namespace QuickHire.Application.Users.Buyer.FirstPage.ExploreHotGigs;

public record ExploreHotGigsQuery : IQuery<List<GigCardModel>>;