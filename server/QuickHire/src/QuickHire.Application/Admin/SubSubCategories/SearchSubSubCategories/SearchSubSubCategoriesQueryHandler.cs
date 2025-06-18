using Mapster;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;

namespace QuickHire.Application.Admin.SubSubCategories.SearchSubSubCategories;

public class SearchSubSubCategoriesQueryHandler : IQueryHandler<SearchSubSubCategoriesQuery, PaginatedResultModel<SubSubCategoryRowModel>>
{
    private readonly IRepository _repository;

    public SearchSubSubCategoriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResultModel<SubSubCategoryRowModel>> Handle(SearchSubSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var subSubCategoriesQuery = _repository.GetAllIncluding<SubSubCategory>(x => x.Gigs!, x => x.GigFilters);

        if (!string.IsNullOrEmpty(request.Keyword))
        {
            subSubCategoriesQuery = subSubCategoriesQuery.Where(x => x.Name.ToLower().Contains(request.Keyword.ToLower()));
        }

        if (request.Id != null)
        {
            subSubCategoriesQuery = subSubCategoriesQuery.Where(x => x.Id == request.Id);
        }

        if (request.SubCategoryId != null)
        {
            subSubCategoriesQuery = subSubCategoriesQuery.Where(x => x.SubCategoryId == request.SubCategoryId);
        }

        var totalCount = subSubCategoriesQuery.Count();

        IEnumerable<SubSubCategory> subSubCategoriesList;

        if (totalCount <= request.ItemsPerPage)
        {
            subSubCategoriesList = await _repository.ToListAsync(subSubCategoriesQuery.OrderBy(x => x.Id).ThenBy(x => x.Name));
        }
        else
        {
            var pagedQuery = subSubCategoriesQuery.Skip((request.CurrentPage - 1) * request.ItemsPerPage).Take(request.ItemsPerPage);

            subSubCategoriesList = await _repository.ToListAsync(pagedQuery);
        }

        return new PaginatedResultModel<SubSubCategoryRowModel>()
        {
            Data = subSubCategoriesList.Select(x => x.Adapt<SubSubCategoryRowModel>()).ToArray(),
            TotalPages = (int)Math.Ceiling(totalCount / (double)request.ItemsPerPage)
        };
    }
}

