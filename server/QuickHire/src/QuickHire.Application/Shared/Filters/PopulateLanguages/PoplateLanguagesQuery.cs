using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Shared.Filters.PopulateLanguages;

public record PopulateLanguagesQuery() : IQuery<IEnumerable<PopulationModel>>;

