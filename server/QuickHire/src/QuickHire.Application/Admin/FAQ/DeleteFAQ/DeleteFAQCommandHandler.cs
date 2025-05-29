using MediatR;
using QuickHire.Application.Admin.Models.FAQ;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.FAQ.DeleteFAQ;

public class DeleteFAQCommandHandler : ICommandHandler<DeleteFAQCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteFAQCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = await _repository.GetByIdAsync<QuickHire.Domain.Categories.FAQ, int>(request.Id);
        if (faq == null)
        {
            throw new NotFoundException(nameof(FAQ), request.Id);
        }

        faq.IsDeleted = true;
        faq.DeletedAt = DateTime.Now;

        await _repository.UpdateAsync(faq);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

