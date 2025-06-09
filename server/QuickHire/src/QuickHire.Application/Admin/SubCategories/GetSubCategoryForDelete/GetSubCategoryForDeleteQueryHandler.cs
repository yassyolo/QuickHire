using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubCategories.GetSubCategoryForDelete;

public class GetSubCategoryForDeleteQueryHandler : IQueryHandler<GetSubCategoryForDeleteQuery, GetSubCategoryForDeleteModel>
{
    private readonly IRepository _repository;

    public GetSubCategoryForDeleteQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetSubCategoryForDeleteModel> Handle(GetSubCategoryForDeleteQuery request, CancellationToken cancellationToken)
    {
        var subCategoryQueryable = _repository.GetAllIncluding<QuickHire.Domain.Categories.SubCategory>(x => x.SubSubCategories).Where(x => x.Id == request.Id);
        var subCategory = await _repository.FirstOrDefaultAsync<QuickHire.Domain.Categories.SubCategory>(subCategoryQueryable);

        if (subCategory == null)
        {
            throw new NotFoundException(nameof(Domain.Categories.SubCategory), request.Id);
        }

        return new GetSubCategoryForDeleteModel
        {
            Id = subCategory.Id,
            SubSubCategories = subCategory.SubSubCategories.Select(x => x.Adapt<SubSubCategoryForSubCategoryModel>()).ToList()
        };
    }
}


