using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.FavouriteLists.RemoveGigFromList;

public record RemoveGigFromListCommand(int FavouriteGigId) : ICommand<Unit>;

