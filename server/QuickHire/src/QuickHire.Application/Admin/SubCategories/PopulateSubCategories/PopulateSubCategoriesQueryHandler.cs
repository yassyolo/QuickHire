using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Admin.SubCategories.PopulateSubCategories;

public class PopulateSubCategoriesQueryHandler : IQueryHandler<PopulateSubCategoriesQuery, IEnumerable<PopulateSubCategoriesModel>>
{
    private readonly IRepository _repository;
    public PopulateSubCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<PopulateSubCategoriesModel>> Handle(PopulateSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var subCategories = _repository.GetAllReadOnly<SubCategory>();

        var subCategoriesList = await _repository.ToListAsync(subCategories);

        return subCategoriesList.Adapt<List<PopulateSubCategoriesModel>>();
    }
}

