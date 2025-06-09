using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.Users;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Admin.Users.SearchUsers;

public class SearchUsersQueryHandler : IQueryHandler<SearchUsersQuery, PaginatedResultModel<UserForAdminModel>>
{
    private readonly IUserService _userService;

    public SearchUsersQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<PaginatedResultModel<UserForAdminModel>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
    {
         return await _userService.GetUsersForAdminAsync(request);       
    }
}

