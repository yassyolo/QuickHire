using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.FAQ.AddFAQ;

public record AddFAQCommand(string Question, string Answer, int? GigId, int? MainCategoryId) : ICommand<Unit>;
