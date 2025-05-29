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

        if (subSubCategory.GigFilters.Any())
        {
            throw new BadRequestException("Cannot delete a sub sub category that has filters.", $"{nameof(SubCategory.Name)} has filters and cannot be deleted.");
        }

        if(subSubCategory.Gigs.Any())
        {
            throw new BadRequestException("Cannot delete a sub sub category that has gigs.", $"{nameof(SubCategory.Name)} has gigs and cannot be deleted.");
        }

        await _repository.DeleteAsync(subSubCategory);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

