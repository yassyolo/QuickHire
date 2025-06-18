using MediatR;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Shared.FAQ.EditFAQ;

public class EDitFAQCommandHandler : ICommandHandler<EditFAQCommand, Unit>
{
    private readonly IRepository _repository;

    public EDitFAQCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(EditFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = await _repository.GetByIdAsync<Domain.Categories.FAQ, int>(request.Id);
        if (faq == null)
        {
            throw new NotFoundException(nameof(FAQ), request.Id);
        }

        faq.Question = request.Question;
        faq.Answer = request.Answer;

        await _repository.UpdateAsync(faq);

        return Unit.Value;
    }
}

