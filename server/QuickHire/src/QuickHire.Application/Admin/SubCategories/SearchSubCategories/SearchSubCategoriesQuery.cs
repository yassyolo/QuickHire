using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.SearchSubCategories;

public record SearchSubCategoriesQuery(int? Id, string? Keyword, int? MainCategoryId, int CurrentPage, int ItemsPerPage) : IQuery<PaginatedResultModel<SubCategoryRowModel>>;

