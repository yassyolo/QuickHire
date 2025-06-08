using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Seller.Gigs.ToggleActivationStatus;

public record ToggleActivationStatusCommand(bool Paused, int Id) : ICommand<Unit>;

