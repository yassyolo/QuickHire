using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteGigFilterCommand;

public class DeleteGigFilterCommandHandler : ICommandHandler<DeleteGigFilterCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteGigFilterCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteGigFilterCommand request, CancellationToken cancellationToken)
    {
        var gigFilterQuerysble = _repository.GetAllIncluding<Domain.Categories.GigFilter>(x => x.Options).Where(x => x.Id == request.Id);
        var gigFilter = await _repository.GetByIdAsync<Domain.Categories.GigFilter, int>(request.Id)!;
        if (gigFilter == null)
        {
            throw new NotFoundException(nameof(Domain.Categories.GigFilter), request.Id);
        }

        if (gigFilter.Options.Any())
        {
            throw new BadRequestException("This filter has options, please delete the options first.", "");
        }

        await _repository.DeleteAsync(gigFilter);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

