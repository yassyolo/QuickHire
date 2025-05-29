using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Users.Authentication.RefreshToken;

public record RefreshTokenCommand(RefreshTokenModel Model) : ICommand<Unit>;

