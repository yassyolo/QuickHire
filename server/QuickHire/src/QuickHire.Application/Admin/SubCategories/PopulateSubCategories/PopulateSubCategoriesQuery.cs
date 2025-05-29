using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.PopulateSubCategories;

public record PopulateSubCategoriesQuery() : IQuery<IEnumerable<PopulateSubCategoriesModel>>;

