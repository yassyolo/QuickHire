using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Buyer.Categories.SubSubCategoryPageData;

public class SubSubCategoryPageDataQueryHandler : IQueryHandler<SubSubCategoryPageDataQuery, SubSubCategoryPageDataModel>
{
    private readonly IRepository _repository;

    public SubSubCategoryPageDataQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubSubCategoryPageDataModel> Handle(SubSubCategoryPageDataQuery request, CancellationToken cancellationToken)
    {
        var subSubCategoryQuery = _repository.GetAllIncluding<Domain.Categories.SubSubCategory>(x => x.SubCategory.MainCategory).Where(x => x.Id == request.Id);

        var subSubCategory = await _repository.FirstOrDefaultAsync(subSubCategoryQuery);
        if (subSubCategory == null)
        {
            throw new NotFoundException(nameof(Domain.Categories.SubSubCategory), request.Id);
        }

        subSubCategory.Clicks++;
        await _repository.UpdateAsync(subSubCategory);
        await _repository.SaveChangesAsync();

        return new SubSubCategoryPageDataModel
        {
            MainCategoryId = subSubCategory.SubCategory.MainCategoryId,
            MainCategoryName = subSubCategory.SubCategory.MainCategory.Name,
            MainCategoryDescription = subSubCategory.SubCategory.MainCategory.Description,
            SubSubCategoryName = subSubCategory.Name,
            SubCategoryId = subSubCategory.SubCategoryId,
            SubCategoryName = subSubCategory.SubCategory.Name,
        };
    }
}
