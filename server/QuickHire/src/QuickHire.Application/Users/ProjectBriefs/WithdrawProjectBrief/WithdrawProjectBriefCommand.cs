using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.ProjectBriefs.WithdrawProjectBrief;

public record WithdrawProjectBriefCommand(int Id) : ICommand<Unit>;

