using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Application.Users.Seller.NewSeller.GetExistingUserInfo;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Models.NewSEller;

public class GetExistingUserInfoQueryHandler : IQueryHandler<GetExistingUserInfoQuery, GetExistingUserInfoModel>
{
    private readonly IUserService _userService;
    private readonly IRepository _reposiroty;

    public GetExistingUserInfoQueryHandler(IUserService userService, IRepository reposiroty)
    {
        _userService = userService;
        _reposiroty = reposiroty;
    }

    public async Task<GetExistingUserInfoModel> Handle(GetExistingUserInfoQuery request, CancellationToken cancellationToken)
    {
        //return await _userService.GetExistingUserInfoAsync();

        return new GetExistingUserInfoModel
        {
            FullName = "test",
            Username = "test",
            Description = "test",
            ProfilePictureUrl = "test",
            Languages = new List<UserLanguageModel>
            {
                new UserLanguageModel
                {
                    LanguageName = "English",
                    LanguageId = 1,
                },
                new UserLanguageModel
                {
                    LanguageName = "Spanish",
                    LanguageId = 2,
                }
            }
        };
    }
}
