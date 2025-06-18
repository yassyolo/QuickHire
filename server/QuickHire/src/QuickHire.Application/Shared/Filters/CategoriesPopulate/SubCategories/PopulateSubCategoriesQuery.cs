using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.Filters.CategoriesPopulate.SubCategories;

public record PopulateSubCategoriesQuery() : IQuery<IEnumerable<PopulateSubCategoriesModel>>;

