using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Filters.OrderStatusFilter;

public record OrderStatusQuery : IQuery<FilterItemModel[]>;
