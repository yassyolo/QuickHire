using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.FavouriteLists.AddFavouriteList;

public record AddFavouriteListCommand(string Name, int? GigId, string? Description) : ICommand<Unit>;

