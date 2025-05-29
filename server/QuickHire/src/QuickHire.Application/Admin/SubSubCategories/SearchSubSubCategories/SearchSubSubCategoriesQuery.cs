using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.SearchSubSubCategories;

public record SearchSubSubCategoriesQuery(int? Id, string? Keyword, int? SubCategoryId, int CurrentPage, int ItemsPerPage) : IQuery<PaginatedResultModel<SubSubCategoryRowModel>>;

