using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Buyer.Categories.SubCategoryPage;

public class SubCategoryPageDataQueryHandler : IQueryHandler<SubCategoryPageDataQuery, SubCategoryPageDataModel>
{
    private readonly IRepository _repository;

    public SubCategoryPageDataQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubCategoryPageDataModel> Handle(SubCategoryPageDataQuery request, CancellationToken cancellationToken)
    {
        var subCategoryQueryable = _repository.GetAllIncluding<Domain.Categories.SubCategory>(x => x.MainCategory).Where(x => x.Id == request.Id);
        var subCategory = await _repository.FirstOrDefaultAsync(subCategoryQueryable);

        if (subCategory == null)
        {
            throw new NotFoundException(nameof(Domain.Categories.SubCategory), request.Id);
        }

        subCategory.Clicks++;
        await _repository.UpdateAsync(subCategory);
        await _repository.SaveChangesAsync();

        return new SubCategoryPageDataModel
        {
            MainCategoryId = subCategory.MainCategoryId,
            MainCategoryName = subCategory.MainCategory.Name,
            MainCategoryDescription = subCategory.MainCategory.Description,
            SubCategoryName = subCategory.Name,
        };
    }
}
