using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Gigs.DeactivateGigAdmin;

public record DeactivateGigAdminCommand(int Id, string Reason): ICommand<Unit>;

