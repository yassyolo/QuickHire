using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.MainCategories.AddMainCategory;

internal class AddMainCategoryCommandHandler : ICommandHandler<AddMainCategoryCommand, Unit>
{
    private readonly IRepository _repository;

    public AddMainCategoryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddMainCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var mainCategory = new MainCategory
            {
                Name = request.Name,
                Description = request.Description,
                Clicks = 0,
                CreatedOn = DateTime.Now,
            };

            await _repository.AddAsync(mainCategory);
            await _repository.SaveChangesAsync();

            foreach (var faq in request.Faqs)
            {
                var newFaq = new QuickHire.Domain.Categories.FAQ
                {
                    Question = faq.Question,
                    Answer = faq.Answer,
                    MainCategoryId = mainCategory.Id
                };
                await _repository.AddAsync(newFaq);
            }

            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new BadRequestException("An error occurred while adding the main category.", ex.Message);
        }
       
        return Unit.Value;
    }
}

