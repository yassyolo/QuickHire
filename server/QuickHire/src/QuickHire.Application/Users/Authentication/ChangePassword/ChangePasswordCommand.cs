using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.ChangePassword;

public record ChangePasswordCommand (string NewPassword, string ConfirmPassword) : ICommand<Unit>;

