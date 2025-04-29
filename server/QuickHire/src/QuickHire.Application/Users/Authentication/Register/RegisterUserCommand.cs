using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Authentication;

namespace QuickHire.Application.Users.Authentication.Register;

internal record RegisterUserCommand(RegisterUserModel model) : ICommand<RegisterUserResponseModel>;

