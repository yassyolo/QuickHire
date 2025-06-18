using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Gigs.Seller.ToggleActivationStatus;

public record ToggleActivationStatusCommand(int Id, bool Paused) : ICommand<Unit>;

