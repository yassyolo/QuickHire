using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.EditFilterOption;

public class EditFilterOptionCommandHandler : ICommandHandler<EditFilterOptionCommand, Unit>
{
    private readonly IRepository _repository;

    public EditFilterOptionCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(EditFilterOptionCommand request, CancellationToken cancellationToken)
    {
       var filterOption = await _repository.GetByIdAsync<Domain.Categories.FilterOption, int>(request.Id);
        
        if (filterOption == null)
        {
            throw new NotFoundException(nameof(FilterOption), request.Id);
        }

        filterOption.Name = request.Name;

        await _repository.UpdateAsync(filterOption);

        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

