using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.SubSubCategoriesInSubCategory;

public record SubSubCategoriesInSubCategoryQuery(int Id) : IQuery<List<SubSubCategoriesInSubCategoryModel>>;
