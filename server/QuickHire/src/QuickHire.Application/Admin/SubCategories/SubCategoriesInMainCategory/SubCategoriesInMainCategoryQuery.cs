using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.SubCategoriesInMainCategory;

public record SubCategoriesInMainCategoryQuery(int Id) : IQuery<IEnumerable<SubCategoriesInMainCategoryResponseModel>>;

