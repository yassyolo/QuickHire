using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.FavouriteLists;

namespace QuickHire.Application.Gigs.FavouriteLists.GetFavouriteListItems;

public record GetFavouriteListItemsQuery(int Id) : IQuery<FavouriteListItemModel>;