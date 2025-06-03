using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.FavouriteLists.EditFavouriteList;

public record EditFavouriteListCommand(int Id, string Name, string Description) : ICommand<Unit>;
