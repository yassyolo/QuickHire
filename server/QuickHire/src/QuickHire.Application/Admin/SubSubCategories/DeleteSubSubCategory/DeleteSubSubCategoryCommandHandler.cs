using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteSubSubCategory;

public class DeleteSubSubCategoryCommandHandler : ICommandHandler<DeleteSubSubCategoryCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteSubSubCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteSubSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subSubCategoryQueryable = _repository.GetAllIncluding<SubSubCategory>(x => x.GigFilters, x => x.Gigs).Where(x => x.Id == request.Id);
        var subSubCategory = await _repository.FirstOrDefaultAsync(subSubCategoryQueryable);
        if (subSubCategory == null)
        {
            throw new NotFoundException(nameof(SubSubCategory), request.Id);
        }

        if (subSubCategory.Gigs!.Any())
        {
            throw new BadRequestException("Cannot delete a sub sub category that has gigs.", $"{nameof(SubCategory.Name)} has gigs and cannot be deleted.");
        }

        try
        {
            var gigFilters = _repository.GetAllIncluding<GigFilter>().Where(x => x.SubSubCategoryId == subSubCategory.Id);
            var gigFiltersList = await _repository.ToListAsync(gigFilters);

            if (gigFiltersList.Any())
            {
                foreach (var filter in gigFiltersList)
                {
                    filter.IsDeleted = true;
                    filter.DeletedAt = DateTime.Now;
                    await _repository.UpdateAsync(filter);
                }
            }

            subSubCategory.IsDeleted = true;
            subSubCategory.DeletedAt = DateTime.Now;

            await _repository.UpdateAsync(subSubCategory);
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Error while deleting sub sub category", ex.Message);
        }

        return Unit.Value;
    }
}

