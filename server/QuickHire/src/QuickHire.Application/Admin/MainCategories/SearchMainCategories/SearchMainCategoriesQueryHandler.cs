using Mapster;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Admin.MainCategories.SearchMainCategories;

public class SearchMainCategoriesQueryHandler : IQueryHandler<SearchMainCategoriesQuery, PaginatedResultModel<MainCategoryRowModel>>
{
    private readonly IRepository _repository;

    public SearchMainCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResultModel<MainCategoryRowModel>> Handle(SearchMainCategoriesQuery request, CancellationToken cancellationToken)
    {
        var mainCategoriesQuery = _repository.GetAllReadOnly<MainCategory>();
        mainCategoriesQuery = _repository.GetAllIncluding<MainCategory>(x => x.SubCategories);

        if (request.Id != null)
        {
            mainCategoriesQuery = mainCategoriesQuery.Where(x => x.Id == request.Id);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            mainCategoriesQuery = mainCategoriesQuery.Where(x => x.Name.ToLower().Contains(request.Keyword.ToLower()));
        }

        var totalCount = mainCategoriesQuery.Count();

        IEnumerable<MainCategory> mainCategoriesList;

        if (totalCount <= request.ItemsPerPage)
        {
            mainCategoriesList = await _repository.ToListAsync(mainCategoriesQuery.OrderBy(x => x.Id).ThenBy(x => x.Name));
        }
        else
        {
            var pagedQuery = mainCategoriesQuery
                .Skip((request.CurrentPage - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage);

            mainCategoriesList = await _repository.ToListAsync(pagedQuery);
        }

        return new PaginatedResultModel<MainCategoryRowModel>()
        {
            Data = mainCategoriesList.Adapt<List<MainCategoryRowModel>>(),
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.ItemsPerPage)
        };
    }
}

