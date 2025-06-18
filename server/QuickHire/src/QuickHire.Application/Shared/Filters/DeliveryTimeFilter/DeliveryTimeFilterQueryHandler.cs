using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Categories.Enums;

namespace QuickHire.Application.Shared.Filters.DeliveryTimeFilter;

public class DeliveryTimeFilterQueryHandler : IQueryHandler<DeliveryTimeFilterQuery, FilterItemModel[]>
{
    private readonly IRepository _repository;
    public DeliveryTimeFilterQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<FilterItemModel[]> Handle(DeliveryTimeFilterQuery request, CancellationToken cancellationToken)
    {
        var gigFilterOptions = _repository.GetAllReadOnly<GigFilter>()!.Where(x => x.Type == GigFilterType.DeliveryTime).SelectMany(x => x.Options);

        var gigFilterOptionsList = await _repository.ToListAsync(gigFilterOptions);
        return gigFilterOptionsList.Select(x => new FilterItemModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToArray();
    }
}

