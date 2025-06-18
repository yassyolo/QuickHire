using QuickHire.Application.Admin.Models.Filters;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;

namespace QuickHire.Application.Shared.Filters.RoleFilter;

public class RoleFilterQueryHandler : IQueryHandler<RoleFilterQuery, RoleFilterItemModel[]>
{
    private readonly IUserService _userService;

    public RoleFilterQueryHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<RoleFilterItemModel[]> Handle(RoleFilterQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetRolesAsync();
    }
}

