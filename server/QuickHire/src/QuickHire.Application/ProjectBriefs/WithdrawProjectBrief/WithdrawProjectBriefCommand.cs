using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.ProjectBriefs.WithdrawProjectBrief;

public record WithdrawProjectBriefCommand(int Id) : ICommand<Unit>;

