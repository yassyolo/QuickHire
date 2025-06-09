using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Users.GetGigsForUser;

public record GetGigsForUserQuery(int CurrentPage, int ItemsPerPage) : IQuery<PaginatedResultModel<SearchGigsForAdminModel>>;
