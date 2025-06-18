using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.Filters.ServiceIncludesFilter;

public class ServiceIncludesFilterQuery : IQuery<List<ServiceIncludesFilterModel>>
{
    public int? Id { get; init; }
}

