using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Users.Authentication.RefreshToken;

internal record RefreshTokenCommand(RefreshTokenModel model) : ICommand<Unit>;

