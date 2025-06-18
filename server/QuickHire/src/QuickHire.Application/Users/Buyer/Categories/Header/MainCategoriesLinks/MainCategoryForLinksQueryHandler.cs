using Mapster;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Users.Buyer.Categories.Header.MainCategoriesForLinks;

public class MainCategoryForLinksQueryHandler : IQueryHandler<MainCategoryForLinksQuery, IEnumerable<MainCategoryForLinksModel>>
{
    private readonly IRepository _repository;

    public MainCategoryForLinksQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MainCategoryForLinksModel>> Handle(MainCategoryForLinksQuery request, CancellationToken cancellationToken)
    {
        var mainCategories = _repository.GetAllReadOnly<MainCategory>().OrderBy(x => x.Clicks);
        var mainCategoriesList = await _repository.ToListAsync(mainCategories);

        return mainCategoriesList.Adapt<List<MainCategoryForLinksModel>>().ToList();
    }
}

