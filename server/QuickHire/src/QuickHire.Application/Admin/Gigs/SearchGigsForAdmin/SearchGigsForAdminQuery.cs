using QuickHire.Application.Admin.Models.Gigs;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;

namespace QuickHire.Application.Admin.Gigs.SearchGigsForAdmin;

public record SearchGigsForAdminQuery(int? ModerationStatusId, int? PriceRangeId, int? SubCategoryId, int? SubSubCategoryId, int? Id, string? Keyword, int CurrentPage, int ItemsPerPage) : IQuery<PaginatedResultModel<SearchGigsForAdminModel>>;

