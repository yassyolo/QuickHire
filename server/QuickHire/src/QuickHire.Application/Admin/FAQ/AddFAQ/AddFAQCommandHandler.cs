using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using FAQ = QuickHire.Domain.Categories;

namespace QuickHire.Application.Admin.FAQ.AddFAQ;

public class AddFAQCommandHandler : ICommandHandler<AddFAQCommand, Unit>
{
    private readonly IRepository _repository;

    public AddFAQCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(AddFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = new QuickHire.Domain.Categories.FAQ
        {
            Question = request.Question,
            Answer = request.Answer,
        };

        if (request.GigId.HasValue)
        {
            faq.GigId = request.GigId.Value;
        }

        if (request.MainCategoryId.HasValue)
        {
            faq.MainCategoryId = request.MainCategoryId.Value;
        }

        await _repository.AddAsync(faq);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}
