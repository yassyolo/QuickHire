using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Authentication.SwitchMode;

public record SwitchModeCommand(string Mode) : ICommand<Unit>;
