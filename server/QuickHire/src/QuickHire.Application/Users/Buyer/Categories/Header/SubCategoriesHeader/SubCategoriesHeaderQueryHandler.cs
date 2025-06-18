using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;

namespace QuickHire.Application.Users.Buyer.Categories.Header.SubCategoriesHeader;

public class SubCategoriesHeaderQueryHandler : IQueryHandler<SubCategoriesHeaderQuery, IEnumerable<SubCategoriesHeaderResponseModel>>
{
    private readonly IRepository _repository;

    public SubCategoriesHeaderQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SubCategoriesHeaderResponseModel>> Handle(SubCategoriesHeaderQuery request, CancellationToken cancellationToken)
    {
        var subCategoriesQuery = _repository.GetAllIncluding<Domain.Categories.SubCategory>(x => x.SubSubCategories).Where(x => x.MainCategoryId == request.Id);

        var subCategoriesList = await _repository.ToListAsync(subCategoriesQuery);

        return subCategoriesList.Adapt<List<SubCategoriesHeaderResponseModel>>();
    }
}

