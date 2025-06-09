using Mapster;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Admin.SubCategories.SearchSubCategories;

public class SearchSubCategoriesQueryHandler : IQueryHandler<SearchSubCategoriesQuery, PaginatedResultModel<SubCategoryRowModel>>
{
    private readonly IRepository _repository;

    public SearchSubCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResultModel<SubCategoryRowModel>> Handle(SearchSubCategoriesQuery request, CancellationToken cancellationToken)
   {
        var subCategoriesQuery = _repository.GetAllIncluding<SubCategory>(x => x.SubSubCategories, x => x.MainCategory);

        if (request.Id != null)
        {
            subCategoriesQuery = subCategoriesQuery.Where(x => x.Id == request.Id);
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            subCategoriesQuery = subCategoriesQuery.Where(x => x.Name.ToLower().Contains(request.Keyword.ToLower())
            || x.MainCategory.Name.ToLower().Contains(request.Keyword.ToLower()));
        }

        if (request.MainCategoryId != null)
        {
            subCategoriesQuery = subCategoriesQuery.Where(x => x.MainCategoryId == request.MainCategoryId);
        }

        var totalCount = subCategoriesQuery.Count();

        IEnumerable<SubCategory> subCategoriesList;

        if (totalCount <= request.ItemsPerPage)
        {
            subCategoriesList = await _repository.ToListAsync(subCategoriesQuery.OrderBy(x => x.Id).ThenBy(x => x.Name));
        }
        else
        {
            var pagedQuery = subCategoriesQuery.OrderBy(x => x.Name).Skip((request.CurrentPage - 1) * request.ItemsPerPage)
                .Take(request.ItemsPerPage);

            subCategoriesList = await _repository.ToListAsync(pagedQuery);
        }


        return new PaginatedResultModel<SubCategoryRowModel>()
        {
            Data = subCategoriesList.Adapt<List<SubCategoryRowModel>>(),
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.ItemsPerPage)
        };
    }
}

