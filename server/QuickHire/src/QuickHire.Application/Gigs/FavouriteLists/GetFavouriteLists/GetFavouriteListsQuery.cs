using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.FavouriteLists;

namespace QuickHire.Application.Gigs.FavouriteLists.GetFavouriteLists;

public record GetFavouriteListsQuery() : IQuery<IEnumerable<FavouriteListModel>>;
