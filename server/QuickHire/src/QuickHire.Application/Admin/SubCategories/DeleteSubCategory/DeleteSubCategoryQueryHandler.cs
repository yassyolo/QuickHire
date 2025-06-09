using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubCategories.DeleteSubCategory;

public class DeleteSubCategoryQueryHandler : IQueryHandler<DeleteSubCategoryQuery, Unit>
{
    private readonly IRepository _repository;

    public DeleteSubCategoryQueryHandler(IRepository subCategoryRepository)
    {
        _repository = subCategoryRepository;
    }

    public async Task<Unit> Handle(DeleteSubCategoryQuery request, CancellationToken cancellationToken)
    {
        var subCategoruQueryable = _repository.GetAllIncluding<SubCategory>(x => x.SubSubCategories).Where(x => x.Id == request.Id);
        var subCategory = await _repository.FirstOrDefaultAsync<SubCategory>(subCategoruQueryable);

        if (subCategory == null)
        {
            throw new NotFoundException(nameof(SubCategory), request.Id);
        }

        if (subCategory.SubSubCategories.Any())
        {
            throw new BadRequestException("Cannot delete a sub category that has subsubcategories.", $"{nameof(SubCategory.Name)} has subsubcategories and cannot be deleted.");
        }

        await _repository.DeleteAsync(subCategory);

        return Unit.Value;
    }
}

