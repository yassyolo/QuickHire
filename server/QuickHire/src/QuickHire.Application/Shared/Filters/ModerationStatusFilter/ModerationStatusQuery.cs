using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Shared.Filters.ModerationStatusFilter;

public record ModerationStatusQuery : IQuery<FilterItemModel[]>;

