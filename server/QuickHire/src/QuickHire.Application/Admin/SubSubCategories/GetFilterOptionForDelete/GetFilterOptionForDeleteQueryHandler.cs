using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.GetFilterOptionForDelete;

public class GetFilterOptionForDeleteQueryHandler : IQueryHandler<GetFilterOptionForDeleteQuery, string>
{
    private readonly IRepository _repository;

    public GetFilterOptionForDeleteQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<string> Handle(GetFilterOptionForDeleteQuery request, CancellationToken cancellationToken)
    {
        var gigsWithOptionQueryable = _repository.GetAllIncluding<FilterOption>(x => x.GigFilter.SubSubCategory.Gigs).Where(x => x.Id == request.Id);
        var gigFilterOption = await _repository.FirstOrDefaultAsync<FilterOption>(gigsWithOptionQueryable);
        if (gigFilterOption == null)
        {
            throw new NotFoundException(nameof(FilterOption), request.Id);
        }

        if (gigFilterOption.GigFilter.SubSubCategory.Gigs.Any())
        {
            return gigFilterOption.GigFilter.SubSubCategory.Gigs.Count().ToString();
        }

        return string.Empty;
    }
}
