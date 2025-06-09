using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.FavouriteLists.UnfavouriteGig;

public record UnfavouriteGigCommand(int Id) : ICommand<Unit>;

