using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.GetFilterOptionForDelete;

public record GetFilterOptionForDeleteQuery(int Id) : IQuery<string>;
