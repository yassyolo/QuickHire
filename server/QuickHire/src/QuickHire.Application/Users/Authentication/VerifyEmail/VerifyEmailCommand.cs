using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.VerifyEmail;

public record VerifyEmailCommand(string UserId, string Token) : ICommand<Unit>;
