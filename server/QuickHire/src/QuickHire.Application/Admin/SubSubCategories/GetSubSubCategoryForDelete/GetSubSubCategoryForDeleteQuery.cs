using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.GetSubSubCategoryForDelete;

public record GetSubSubCategoryForDeleteQuery(int Id) : IQuery<SubSubCategoryForDeleteModel>;
