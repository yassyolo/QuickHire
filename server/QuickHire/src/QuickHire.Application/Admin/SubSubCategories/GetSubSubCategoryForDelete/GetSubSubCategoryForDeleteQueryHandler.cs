using Mapster;
using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.GetSubSubCategoryForDelete;

public class GetSubSubCategoryForDeleteQueryHandler : IQueryHandler<GetSubSubCategoryForDeleteQuery, SubSubCategoryForDeleteModel>
{
    private readonly IRepository _repository;

    public GetSubSubCategoryForDeleteQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubSubCategoryForDeleteModel> Handle(GetSubSubCategoryForDeleteQuery request, CancellationToken cancellationToken)
    {
        var subSubCategoryQueryable = _repository.GetAllIncluding<SubSubCategory>(x => x.Gigs, x => x.GigFilters).Where(x => x.Id == request.Id);
        var subSubCategory = await _repository.FirstOrDefaultAsync(subSubCategoryQueryable);

        if (subSubCategory == null)
        {
            throw new NotFoundException(nameof(SubSubCategory), request.Id);
        }

        var result = new SubSubCategoryForDeleteModel
        {
            Id = subSubCategory.Id,
            Gigs = subSubCategory.Gigs.Count(),
            SubSubCategoryFilters = subSubCategory.GigFilters.Adapt<List<FilterForSubSubCategoryModel>>()
        };

        return result;
    }
}

