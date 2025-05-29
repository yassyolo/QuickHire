using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.PopularSubcategories;

public record PopularSubcategoriesQuery(int Id) : IQuery<IEnumerable<PopularSubCategoriesResponseModel>>;

