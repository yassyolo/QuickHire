using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Users.Authentication.Register;

public record RegisterBuyerCommand(string Email, string Password) : ICommand<RegisterUserResponseModel>;

