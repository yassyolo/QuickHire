using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Users.Buyer.Categories.SubCategories;

public class PopularSubCategoriesQueryHandler : IQueryHandler<PopularSubcategoriesQuery, IEnumerable<PopularSubCategoriesResponseModel>>
{
    private readonly IRepository _repository;

    public PopularSubCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PopularSubCategoriesResponseModel>> Handle(PopularSubcategoriesQuery request, CancellationToken cancellationToken)
    {
        var subCategoriesQuery = _repository.GetAllReadOnly<SubCategory>().Where(x => x.MainCategoryId == request.Id);
        var subSubCategoriesQuery = _repository.GetAllIncluding<SubSubCategory>(x => x.Gigs);
        var subSubCategories = await _repository.ToListAsync(subSubCategoriesQuery);
        var subCategoriesList = await _repository.ToListAsync(subCategoriesQuery);

        var gigsCountPerSubCategory = subSubCategories.GroupBy(x => x.SubCategoryId).ToDictionary(x => x.Key, x => x.Sum(x => x.Gigs.Count()));

        var sorted = subCategoriesList
            .Select(x => new
            {
                SubCategory = x,
                GigsCount = gigsCountPerSubCategory.TryGetValue(x.Id, out var count) ? count : 0
            })
            .OrderByDescending(x => x.GigsCount).Select(x => x.SubCategory).Take(5).ToList();

        return sorted.Adapt<IEnumerable<PopularSubCategoriesResponseModel>>();
    }
}

