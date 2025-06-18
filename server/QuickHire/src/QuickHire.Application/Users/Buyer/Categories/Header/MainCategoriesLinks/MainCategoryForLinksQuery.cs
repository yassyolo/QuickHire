using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.Header.MainCategoriesForLinks;

public record MainCategoryForLinksQuery() : IQuery<IEnumerable<MainCategoryForLinksModel>>;
