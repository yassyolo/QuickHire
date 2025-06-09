using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;

namespace QuickHire.Application.Admin.SubCategories.SubCategoriesInMainCategory;

public class SubCategoriesInMainCategoryQueryHandler : IQueryHandler<SubCategoriesInMainCategoryQuery, IEnumerable<SubCategoriesInMainCategoryResponseModel>>
{
    private readonly IRepository _repository;

    public SubCategoriesInMainCategoryQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubCategoriesInMainCategoryResponseModel>> Handle(SubCategoriesInMainCategoryQuery request, CancellationToken cancellationToken)
    {
        var subCategoriesQuery= _repository.GetAllIncluding<QuickHire.Domain.Categories.SubCategory>(x => x.SubSubCategories).Where(x => x.MainCategoryId == request.Id);

        var subCategoriesList = await _repository.ToListAsync(subCategoriesQuery);

        return subCategoriesList.Adapt<IEnumerable<SubCategoriesInMainCategoryResponseModel>>();
    }
}

