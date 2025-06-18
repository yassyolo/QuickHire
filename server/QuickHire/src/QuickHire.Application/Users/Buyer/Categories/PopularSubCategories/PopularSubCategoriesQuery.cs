using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.SubCategories;

public record PopularSubcategoriesQuery(int Id) : IQuery<IEnumerable<PopularSubCategoriesResponseModel>>;

