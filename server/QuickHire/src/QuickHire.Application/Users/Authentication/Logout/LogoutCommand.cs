using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.Logout;

public record LogoutCommand() : ICommand<Unit>;

