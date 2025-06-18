using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.FAQ.DeleteFAQ;

public record DeleteFAQCommand(int Id, string Reason) : ICommand<Unit>;

