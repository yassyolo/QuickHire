using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.FavouriteLists.SaveGigToOldList;

public record SaveGigToOldListCommand(int GigId, int FavouriteListId) : ICommand<Unit>;

