using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.Filters.DeliveryTimeFilter;

public record DeliveryTimeFilterQuery : IQuery<FilterItemModel[]>;
