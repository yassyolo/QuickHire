using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.FavouriteLists;

namespace QuickHire.Application.Gigs.FavouriteLists.PopulateFavouriteGigsList;

public record PopulateFavouriteGigsListQuery() : IQuery<IEnumerable<PopulateFavouriteGigListModel>>;