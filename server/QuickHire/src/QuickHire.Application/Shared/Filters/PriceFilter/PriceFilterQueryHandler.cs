using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Categories.Enums;
using System.Security.Cryptography.X509Certificates;
using static QuickHire.Domain.Shared.Constants.FilterOptionsDescriptions;

namespace QuickHire.Application.Shared.Filters.PriceFilter;

public class PriceFilterQueryHandler : IQueryHandler<PriceFilterQuery, FilterItemModel[]>
{
    private readonly IRepository _repository;

    public PriceFilterQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<FilterItemModel[]> Handle(PriceFilterQuery request, CancellationToken cancellationToken)
    {
        var gigFilterOptions = _repository.GetAllReadOnly<GigFilter>()!.Where(x => x.Type == GigFilterType.PriceRange).SelectMany(x => x.Options);

        var gigFilterOptionsList = await _repository.ToListAsync(gigFilterOptions);
        return gigFilterOptionsList.Select(x => new FilterItemModel
        {
            Id = x.Id,
            Name = x.Name,
            Description = GetPriceRangeDescription(x.Name)
        }).ToArray();
    }

    private string GetPriceRangeDescription(string name)
    {
        if (!Enum.TryParse<PriceRangeFilterOptions>(name, out var parsedEnum))
            return name;

        return parsedEnum switch
        {
            PriceRangeFilterOptions.Under => PriceRange.Under,
            PriceRangeFilterOptions.MidRange => PriceRange.MidRange,
            PriceRangeFilterOptions.HighRange => PriceRange.HighEnd,
            _ => name
        };
    }
}

