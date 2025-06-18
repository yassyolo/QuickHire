using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.MainCategories.DeleteMainCategory;

public class DeleteMainCategoryCommandHandler : ICommandHandler<DeleteMainCategoryCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteMainCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
    {
        var mainCategoryQueryable = _repository.GetAllIncluding<MainCategory>(x => x.SubCategories, x => x.FAQs).Where(x => x.Id == request.Id);
        var mainCategory = await _repository.FirstOrDefaultAsync(mainCategoryQueryable);

        if (mainCategory == null)
        {
            throw new NotFoundException(nameof(MainCategory), request.Id);
        }

        if (mainCategory.SubCategories.Any())
        {
            throw new BadRequestException("Cannot delete a main category that has subcategories.", $"{nameof(MainCategory.Name)} has subcategories and cannot be deleted.");
        }

        try
        {
            if (mainCategory.FAQs.Any())
            {
                foreach (var faq in mainCategory.FAQs)
                {
                    faq.IsDeleted = true;
                    faq.DeletedAt = DateTime.Now;
                    await _repository.UpdateAsync(faq);
                }
            }

            mainCategory.IsDeleted = true;
            mainCategory.DeletedAt = DateTime.Now;
            await _repository.UpdateAsync(mainCategory);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while deleting the main category.", ex.Message);
        }       

        return Unit.Value;
    }
}

