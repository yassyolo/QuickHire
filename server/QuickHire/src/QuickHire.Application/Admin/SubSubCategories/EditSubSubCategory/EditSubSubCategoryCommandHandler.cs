using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.EditSubSubCategory;

public class EditSubSubCategoryCommandHandler : ICommandHandler<EditSubSubCategoryCommand, Unit>
{
    private readonly IRepository _repository;

    public EditSubSubCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(EditSubSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subSubCategory = await _repository.GetByIdAsync<SubSubCategory, int>(request.Id);
        if (subSubCategory == null)
        {
            throw new NotFoundException(nameof(SubSubCategory), request.Id);
        }

        if (subSubCategory.Name != request.Name)
        {
            subSubCategory.Name = request.Name;
        }       

        await _repository.UpdateAsync(subSubCategory);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

