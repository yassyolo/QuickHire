using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.SubCategoryPage;

public record SubCategoryPageDataQuery(int Id) : IQuery<SubCategoryPageDataModel>;
