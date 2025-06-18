using MediatR;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.FAQ.AddFAQ;

public record AddFAQCommand(string Question, string Answer, int? GigId, int? MainCategoryId) : ICommand<FAQResponseModel>;
