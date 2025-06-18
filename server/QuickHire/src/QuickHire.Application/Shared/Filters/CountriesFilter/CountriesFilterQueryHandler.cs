using Mapster;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Shared.Filters.CountriesFilter;

public class CountriesFilterQueryHandler : IQueryHandler<CountriesFilterQuery, FilterItemModel[]>
{
    private readonly IRepository _repository;

    public CountriesFilterQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<FilterItemModel[]> Handle(CountriesFilterQuery request, CancellationToken cancellationToken)
    {
        var countries = _repository.GetAllReadOnly<Country>();

        var countriesList = await _repository.ToListAsync(countries!);

        return countriesList.Adapt<FilterItemModel[]>();

    }
}

