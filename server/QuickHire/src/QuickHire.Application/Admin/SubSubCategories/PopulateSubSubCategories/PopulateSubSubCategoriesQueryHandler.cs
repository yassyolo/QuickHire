using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Admin.SubSubCategories.PopulateSubSubCategories;

public class PopulateSubSubCategoriesQueryHandler : IQueryHandler<PopulateSubSubCategoriesQuery, PopulateSubCategoriesModel[]>
{
    private readonly IRepository _repository;

    public PopulateSubSubCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PopulateSubCategoriesModel[]> Handle(PopulateSubSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var subSubCategories = _repository.GetAllReadOnly<SubSubCategory>();

       var subSubCategoriesList = await _repository.ToListAsync(subSubCategories);

        return subSubCategoriesList.Adapt<List<PopulateSubCategoriesModel>>().ToArray();
    }
}

