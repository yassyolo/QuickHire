using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.FavouriteLists.DeleteGigList;

public record DeleteGigListCommand(int Id) : ICommand<Unit>;
