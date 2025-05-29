using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.SubCategoriesHeader;

public record SubCategoriesHeaderQuery(int Id) : IQuery<IEnumerable<SubCategoriesHeaderResponseModel>>;
