using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.SubSubCategories.GetGigFilterForDelete;

public record GetGigFilterForDeleteQuery(int Id) : IQuery<string[]>;

