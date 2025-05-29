using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubCategories.GetSubCategoryForDelete;
public record GetSubCategoryForDeleteQuery(int Id): IQuery<GetSubCategoryForDeleteModel>;
