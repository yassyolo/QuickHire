using Mapster;
using QuickHire.Application.Admin.Models.SubCategories;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubCategories.SubCategoryDetails;

public class SubCategoryDetailsQueryHandler : IQueryHandler<SubCategoryDetailsQuery, SubCategoryDetailsModel?>
{
    private readonly IRepository _repository;

    public SubCategoryDetailsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<SubCategoryDetailsModel?> Handle(SubCategoryDetailsQuery request, CancellationToken cancellationToken)
    {
        var subCategoryQueryable = _repository.GetAllIncluding<SubCategory>(x => x.SubSubCategories).Where(x => x.Id == request.Id);
        var subCategory = await _repository.FirstOrDefaultAsync<SubCategory>(subCategoryQueryable);

        if (subCategory == null)
        {
            throw new NotFoundException(nameof(SubCategory), request.Id);
        }

        var subCategoryDetails = subCategory.Adapt<SubCategoryDetailsModel>();
        subCategoryDetails.SubSubCategories = subCategory.SubSubCategories.Adapt<List<SubSubCategoryForSubCategoryModel>>();
        return subCategoryDetails;
    }
}
