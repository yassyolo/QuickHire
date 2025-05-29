using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.MainCategoriesForLinks;

public record MainCategoryForLinksQuery() : IQuery<IEnumerable<MainCategoryForLinksModel>>;
