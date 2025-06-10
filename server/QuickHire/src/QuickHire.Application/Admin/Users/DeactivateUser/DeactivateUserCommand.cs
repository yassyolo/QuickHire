using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Users.DeactivateUser;

public record DeactivateUserCommand(string Id, string Reason) : ICommand<Unit>;
