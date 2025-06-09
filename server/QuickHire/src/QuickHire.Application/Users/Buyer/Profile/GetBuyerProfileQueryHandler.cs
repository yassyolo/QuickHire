using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;

namespace QuickHire.Application.Users.Buyer.Profile;
public class GetBuyerProfileQueryHandler : IQueryHandler<GetBuyerProfileQuery, BuyerProfileModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetBuyerProfileQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<BuyerProfileModel> Handle(GetBuyerProfileQuery request, CancellationToken cancellationToken)
    {
        //return await _userService.GetBuyerProfileAsync();

        return new BuyerProfileModel
        {
            ProfilePictureUrl = "https://example.com/images/profile123.jpg",
            FullName = "John Doe",
            Username = "johndoe87",
            Description = "Creative buyer passionate about branding and visual storytelling.",
            MemberSince = DateTime.Now.AddYears(-2).ToString("MMMM yyyy"),
            Location = "New York, USA",
            Languages = new[]
    {
        new UserLanguageModel { LanguageId = 1, LanguageName = "English" },
        new UserLanguageModel { LanguageId = 2, LanguageName = "Spanish" }
    }
        };
    }
}

