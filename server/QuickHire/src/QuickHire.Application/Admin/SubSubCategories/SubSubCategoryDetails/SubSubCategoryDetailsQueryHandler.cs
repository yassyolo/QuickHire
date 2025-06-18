using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.SubSubCategoryDetails;

public class SubSubCategoryDetailsQueryHandler : IQueryHandler<SubSubCategoryDetailsQuery, SubSubCategoryDetailsModel>
{
    private readonly IRepository _repository;

    public SubSubCategoryDetailsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubSubCategoryDetailsModel> Handle(SubSubCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var subSubCategory = await _repository.GetByIdAsync<Domain.Categories.SubSubCategory, int>(request.Id)!;
        if (subSubCategory == null)
        {
            throw new NotFoundException(nameof(Domain.Categories.SubSubCategory), request.Id);
        }

        var model = new SubSubCategoryDetailsModel
        {
            Id = subSubCategory.Id,
            Name = subSubCategory.Name,            
            Clicks = subSubCategory.Clicks,
            CreatedOn = subSubCategory.CreatedOn.ToString("dd MMM, yyyy"),
        };

        var gigFiltersQueryable = _repository.GetAllIncluding<Domain.Categories.GigFilter>(x => x.Options).Where(x => x.SubSubCategoryId == subSubCategory.Id);
        var gigFiltersModel = new List<GigFilterModel>();
        foreach (var gigFilter in await _repository.ToListAsync(gigFiltersQueryable))
        {
            var gigFilterModel = new GigFilterModel
            {
                Id = gigFilter.Id,
                Title = gigFilter.Title!,
                Items = gigFilter.Options.Select(x => new FilterOptionModel
                {
                    Id = x.Id,
                    Value = x.Name
                }).ToList()
            };
            gigFiltersModel.Add(gigFilterModel);
        }

        model.GigFilters = gigFiltersModel;
        return model;
    }
}
