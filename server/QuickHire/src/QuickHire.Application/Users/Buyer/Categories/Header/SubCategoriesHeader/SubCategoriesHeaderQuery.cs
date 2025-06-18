using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Users.Buyer.Categories.Header.SubCategoriesHeader;

public record SubCategoriesHeaderQuery(int Id) : IQuery<IEnumerable<SubCategoriesHeaderResponseModel>>;
