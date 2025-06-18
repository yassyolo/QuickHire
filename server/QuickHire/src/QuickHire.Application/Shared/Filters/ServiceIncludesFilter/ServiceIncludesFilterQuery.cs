using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.Filters.ServiceIncludesFilter;

public record ServiceIncludesFilterQuery(int? Id) : IQuery<List<ServiceIncludesFilterModel>>;

