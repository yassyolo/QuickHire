using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Filters.PriceFilter;

public record PriceFilterQuery() : IQuery<FilterItemModel[]>;
