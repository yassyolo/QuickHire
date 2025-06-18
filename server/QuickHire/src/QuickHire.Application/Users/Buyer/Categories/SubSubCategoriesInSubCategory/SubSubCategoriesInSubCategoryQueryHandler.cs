using QuickHire.Application.Admin.Models.SubSubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Users.Buyer.Categories.SubSubCategoriesInSubCategory;

public class SubSubCategoriesInSubCategoryQueryHandler : IQueryHandler<SubSubCategoriesInSubCategoryQuery, List<SubSubCategoriesInSubCategoryModel>>
{
    private readonly IRepository _repository;

    public SubSubCategoriesInSubCategoryQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SubSubCategoriesInSubCategoryModel>> Handle(SubSubCategoriesInSubCategoryQuery request, CancellationToken cancellationToken)
    {
        var subCategoryQueryable = _repository.GetAllIncluding<Domain.Categories.SubCategory>(x => x.SubSubCategories).Where(x => x.Id == request.Id);
        var subCategory = await _repository.FirstOrDefaultAsync(subCategoryQueryable);

        if (subCategory == null)
        {
            throw new NotFoundException(nameof(Domain.Categories.SubCategory), request.Id);
        }

        return subCategory.SubSubCategories.Select(x => new SubSubCategoriesInSubCategoryModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
    }
}
