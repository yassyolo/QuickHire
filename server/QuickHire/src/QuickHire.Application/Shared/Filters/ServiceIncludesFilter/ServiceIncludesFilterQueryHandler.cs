using Microsoft.Extensions.Options;
using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Shared.Filters.ServiceIncludesFilter;

public class ServiceIncludesFilterQueryHandler : IQueryHandler<ServiceIncludesFilterQuery, List<ServiceIncludesFilterModel>>
{
    private readonly IRepository _repository;

    public ServiceIncludesFilterQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ServiceIncludesFilterModel>> Handle(ServiceIncludesFilterQuery request, CancellationToken cancellationToken)
    {
        var gigFiltersQuery = _repository.GetAllIncluding<GigFilter>(x => x.Options).Where(x => x.Type != Domain.Categories.Enums.GigFilterType.DeliveryTime && x.Type != Domain.Categories.Enums.GigFilterType.PriceRange && x.Type != Domain.Categories.Enums.GigFilterType.SellerDetails);

        if (request.Id.HasValue)
        {
            gigFiltersQuery = gigFiltersQuery.Where(x => x.SubSubCategoryId == request.Id.Value);
        }

        var gigFilters = await _repository.ToListAsync(gigFiltersQuery);

        return gigFilters
            .Select(x => new ServiceIncludesFilterModel
            {
                Id = x.Id,
                Name = x.Title!,
                Options = x.Options.Select(option => new FilterOptionModel
                {
                    Id = option.Id,
                    Value = option.Name,
                }).ToList()
            })
            .ToList();
    }
}
