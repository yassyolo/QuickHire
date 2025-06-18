using Mapster;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Shared.Filters.CategoriesPopulate.MainCategories;

public class PopulateMainCategoriesQueryHandler : IQueryHandler<PopulateMainCategoriesQuery, PopulateMainCategoriesModel[]>
{
    private readonly IRepository _repository;

    public PopulateMainCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PopulateMainCategoriesModel[]> Handle(PopulateMainCategoriesQuery request, CancellationToken cancellationToken)
    {
        var mainCategories = _repository.GetAllReadOnly<MainCategory>();
        var mainCategoriesList = await _repository.ToListAsync(mainCategories!);

        return mainCategoriesList.Select(x => x.Adapt<PopulateMainCategoriesModel>()).ToArray();
    }
}

