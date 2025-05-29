using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.MainCategories.GetMainCategoryForDelete;

public record GetMainCategoryForDeleteQuery(int Id) : IQuery<GetMainCategoryForDeleteModel>;

