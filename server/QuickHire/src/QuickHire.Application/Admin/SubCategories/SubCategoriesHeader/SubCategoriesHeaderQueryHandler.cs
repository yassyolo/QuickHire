using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;

namespace QuickHire.Application.Admin.SubCategories.SubCategoriesHeader;

public class SubCategoriesHeaderQueryHandler : IQueryHandler<SubCategoriesHeaderQuery, IEnumerable<SubCategoriesHeaderResponseModel>>
{
    private readonly IRepository _repository;

    public SubCategoriesHeaderQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubCategoriesHeaderResponseModel>> Handle(SubCategoriesHeaderQuery request, CancellationToken cancellationToken)
    {
        var subCategoriesQuery = _repository.GetAllReadOnly<QuickHire.Domain.Categories.SubCategory>().Where(x => x.MainCategoryId == request.Id);
        var subCategoriesList1 = await _repository.ToListAsync(subCategoriesQuery);
        subCategoriesQuery = _repository.GetAllIncluding<QuickHire.Domain.Categories.SubCategory>(x => x.SubSubCategories, x => x.MainCategoryId);
        var subCategoriesList = await _repository.ToListAsync(subCategoriesQuery);

        return subCategoriesList.Adapt<List<SubCategoriesHeaderResponseModel>>();
    }
}

