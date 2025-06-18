using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Application.Users.Models.ProjectBriefs;

namespace QuickHire.Application.Common.Interfaces.Services;

public interface IGigScoringService
{
    Task<PaginatedResultModel<GigCardModel>> GetTopScoringGigsByKeywordAsync(int? subCategoryId, string? keyword, int? subSubCategoryId, int buyerId, int? priceRangeId, int? deliveryTimeId, List<int>? countryIds, List<int>? languageIds, List<int>? selectedOptionsIds, int currentPage, int itemsPerPage);
    Task<List<GigScoreModel>> GetTopScoringGigsAsync(int buyerId, string title, string description, int subSubCategoryId, decimal budget, int deliveryDays);
}
