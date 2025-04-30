using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Users.Authentication.Login;

public record LoginBuyerCommand(LoginBuyerModel model) : ICommand<Unit>;
