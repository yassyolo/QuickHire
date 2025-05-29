using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.FAQ.GetFAQ;

public record GetFAQQuery(int? MainCategoryId, int? GigId, string? UserId) : IQuery<List<FAQResponseModel>>;

