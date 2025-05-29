using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Filters.CountriesFilter;

public record CountriesFilterQuery : IQuery<FilterItemModel[]>;
