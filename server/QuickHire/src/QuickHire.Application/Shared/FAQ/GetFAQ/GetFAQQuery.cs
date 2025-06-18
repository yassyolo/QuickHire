using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.FAQ.GetFAQ;

public record GetFAQQuery(int? MainCategoryId, int? GigId, string? UserId) : IQuery<List<FAQResponseModel>>;

