using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Admin.SubSubCategories.GetGigFilterForDelete;

public class GetGigFilterForDeleteQueryHandler : IQueryHandler<GetGigFilterForDeleteQuery, string[]>
{
    private readonly IRepository _repository;

    public GetGigFilterForDeleteQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async  Task<string[]> Handle(GetGigFilterForDeleteQuery request, CancellationToken cancellationToken)
    {
        var gigFilterQueryable = _repository.GetAllIncluding<GigFilter>(x => x.Options);
        var gigFilter = await _repository.FirstOrDefaultAsync<GigFilter>(gigFilterQueryable);
        if (gigFilter == null)
        {
            throw new NotFoundException(nameof(GigFilter), request.Id);
        }

        if (gigFilter.Options.Any())
        {
            return gigFilter.Options.Select(x => x.Name).ToArray();
        }

        return Array.Empty<string>();
    }
}

