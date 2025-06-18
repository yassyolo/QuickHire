using MediatR;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.DeleteFilterOption;

public class DeleteFilterOptionCommandHandler : ICommandHandler<DeleteFilterOptionCommand, Unit>
{
    private readonly IRepository _repository;

    public DeleteFilterOptionCommandHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<Unit> Handle(DeleteFilterOptionCommand request, CancellationToken cancellationToken)
    {
        var filterOptionQueryable = _repository.GetAllIncluding<FilterOption>(x => x.GigFilter.SubSubCategory!.Gigs!).Where(x => x.Id == request.Id);
        var filterOption = await _repository.FirstOrDefaultAsync(filterOptionQueryable);
        if (filterOption == null)
        {
            throw new NotFoundException(nameof(FilterOption), request.Id);
        }

        if (filterOption.GigFilter.SubSubCategory!.Gigs!.Any())
        {
            throw new BadRequestException("Cannot delete filter options that has gigs.", $"{nameof(FilterOption.Name)} has gigs and cannot be deleted.");
        }

        await _repository.DeleteAsync(filterOption);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}

