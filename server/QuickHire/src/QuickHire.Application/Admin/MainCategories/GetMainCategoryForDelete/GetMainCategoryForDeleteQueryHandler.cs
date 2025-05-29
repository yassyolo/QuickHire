using Mapster;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.MainCategories.GetMainCategoryForDelete;

public class GetMainCategoryForDeleteQueryHandler : IQueryHandler<GetMainCategoryForDeleteQuery, GetMainCategoryForDeleteModel>
{
    private readonly IRepository _repository;

    public GetMainCategoryForDeleteQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetMainCategoryForDeleteModel> Handle(GetMainCategoryForDeleteQuery request, CancellationToken cancellationToken)
    {
        var mainCategoryQueryable = _repository.GetAllIncluding<MainCategory>(x => x.SubCategories).Where(x => x.Id == request.Id);
        var mainCategory = await _repository.FirstOrDefaultAsync<MainCategory>(mainCategoryQueryable);

        if (mainCategory == null)
        {
            throw new NotFoundException(nameof(MainCategory), request.Id);
        }

        return new GetMainCategoryForDeleteModel
        {
            Id = mainCategory.Id,
            SubCategories = mainCategory.SubCategories.Select(x => x.Adapt<SubCategoriesInMainCategoryModel>()).ToList()
        };
    }
}

