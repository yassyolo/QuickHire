using Mapster;
using QuickHire.Application.Admin.Models.MainCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Buyer.Categories.MainCategoryDetails.MainCategoryPageDetails;

public class MainCategoryPageDeatilsQueryHandler : IQueryHandler<MainCategoryPageDeatilsQuery, MainCategoryPageDeatilsModel>

{
    private readonly IRepository _repository;

    public MainCategoryPageDeatilsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<MainCategoryPageDeatilsModel> Handle(MainCategoryPageDeatilsQuery request, CancellationToken cancellationToken)
    {
        var mainCategoryQueryable = _repository.GetAllIncluding<MainCategory>(x => x.SubCategories).Where(x => x.Id == request.Id);
        var mainCategory = await _repository.FirstOrDefaultAsync(mainCategoryQueryable);

        if (mainCategory == null)
        {
            throw new NotFoundException(nameof(MainCategory), request.Id);
        }

        mainCategory.Clicks += 1;
        await _repository.UpdateAsync(mainCategory);
        await _repository.SaveChangesAsync();

        return mainCategory.Adapt<MainCategoryPageDeatilsModel>();
    }
}
