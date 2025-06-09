using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Users.DeactivateUser;

public record DeactivateUserCommand(string UserId, string Reason) : ICommand<Unit>;
