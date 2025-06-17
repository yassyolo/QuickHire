using MediatR;

using Microsoft.AspNetCore.Http;

using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.Logout;

public record GoogleLogoutCommand(HttpContext HttpContext) : ICommand<Unit>;

