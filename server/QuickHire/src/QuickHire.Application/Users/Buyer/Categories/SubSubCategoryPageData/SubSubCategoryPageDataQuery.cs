using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.SubSubCategoryPageData;

public record SubSubCategoryPageDataQuery(int Id) : IQuery<SubSubCategoryPageDataModel>;

