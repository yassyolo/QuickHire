using MediatR;
using Microsoft.AspNetCore.Http;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.GoogleLogin;

public record GoogleLoginCommand(HttpContext HttpContext, string ReturnUrl) : ICommand<Unit>;

