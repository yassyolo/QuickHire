using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Seller.Profile.PopulateLAnguages;

public record PopulateLanguagesQuery() : IQuery<IEnumerable<PopulationModel>>;

