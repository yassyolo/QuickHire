using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Shared.Filters.PopulateLanguages;

public class PoplateLanguagesQueryHAndler : IQueryHandler<PopulateLanguagesQuery, IEnumerable<PopulationModel>>
{
    private readonly IRepository _repository;

    public PoplateLanguagesQueryHAndler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PopulationModel>> Handle(PopulateLanguagesQuery request, CancellationToken cancellationToken)
    {
        var langagesQueryable = _repository.GetAllReadOnly<Domain.Users.Language>();
        var languagesList = await _repository.ToListAsync(langagesQueryable!);

        return languagesList.Adapt<List<PopulationModel>>();
    }
}

