using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.SearchMainCategories;

public record SearchMainCategoriesQuery(int? Id, string? Keyword, int CurrentPage, int ItemsPerPage) : IQuery<PaginatedResultModel<MainCategoryRowModel>>;

