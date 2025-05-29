using Mapster;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Admin.Filters.CountriesFilter;

public class CountriesFilterQueryHandler : IQueryHandler<CountriesFilterQuery, FilterItemModel[]>
{
    private readonly IRepository _repository;

    public CountriesFilterQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<FilterItemModel[]> Handle(CountriesFilterQuery request, CancellationToken cancellationToken)
    {
        /*var countries = _repository.GetAllReadOnly<Country>();

        var countriesList = await _repository.ToListAsync(countries);

        return countriesList.Adapt<FilterItemModel[]>();*/

        return new FilterItemModel[]
        {
            new() { Id = 1, Name = "United States" },
            new() { Id = 2, Name = "Canada" },
            new() { Id = 3, Name = "United Kingdom" },
            new() { Id = 4, Name = "Australia" },
            new() { Id = 5, Name = "Germany" }
        };
    }
}

