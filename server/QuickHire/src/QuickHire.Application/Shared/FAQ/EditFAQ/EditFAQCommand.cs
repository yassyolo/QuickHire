using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.FAQ.EditFAQ;

public record EditFAQCommand(int Id, string Question, string Answer) : ICommand<Unit>;

