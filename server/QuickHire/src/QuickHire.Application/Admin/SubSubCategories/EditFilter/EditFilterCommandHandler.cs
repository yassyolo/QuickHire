using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.EditFilter;

public class EditFilterCommandHandler : ICommandHandler<EditFilterCommand, Unit>
{
    private readonly IRepository _repository;

    public EditFilterCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(EditFilterCommand request, CancellationToken cancellationToken)
    {
        var gigFilter =await  _repository.GetByIdAsync<Domain.Categories.GigFilter, int>(request.Id);
        if(gigFilter == null)
        {
            throw new NotFoundException(nameof(GigFilter), request.Id);
        }

        gigFilter.Title = request.Name;

        await _repository.UpdateAsync(gigFilter);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}
