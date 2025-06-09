using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Admin.Models.Users;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Users.SearchUsers;

public record SearchUsersQuery(int? Id, string? Keyword, string? RoleId, int CurrentPage, int ItemsPerPage, int? ModerationStatusId) : IQuery<PaginatedResultModel<UserForAdminModel>>;
