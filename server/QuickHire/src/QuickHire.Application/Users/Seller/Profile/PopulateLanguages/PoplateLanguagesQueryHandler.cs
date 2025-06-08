using Mapster;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Application.Users.Seller.Profile.PopulateLAnguages;

namespace QuickHire.Application.Users.Seller.Profile.PopulateLanguages;

public class PoplateLanguagesQueryHAndler : IQueryHandler<PopulateLanguagesQuery, IEnumerable<PopulationModel>>
{
    private readonly IRepository _repository;

    public PoplateLanguagesQueryHAndler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PopulationModel>> Handle(PopulateLanguagesQuery request, CancellationToken cancellationToken)
    {
        var langagesQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Users.Language>();
        var languagesList = await _repository.ToListAsync<QuickHire.Domain.Users.Language>(langagesQueryable);

        return languagesList.Adapt<List<PopulationModel>>();
    }
}

