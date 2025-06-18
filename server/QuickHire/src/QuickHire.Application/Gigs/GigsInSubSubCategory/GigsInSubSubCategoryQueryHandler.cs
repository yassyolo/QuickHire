using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Domain.Categories;
using QuickHire.Domain.Users;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static QuickHire.Domain.Shared.Constants.FilterOptionsDescriptions;

namespace QuickHire.Application.Gigs.GigsInSubSubCategory;

public class GigsInSubSubCategoryQueryHandler : IQueryHandler<GigsInSubSubCategoryQuery, PaginatedResultModel<GigCardModel>>
{
    private readonly IGigScoringService _gigScoringService;
    private readonly IUserService _userService;

    public GigsInSubSubCategoryQueryHandler(IGigScoringService gigScoringService, IUserService userService)
    {
        _gigScoringService = gigScoringService;
        _userService = userService;
    }

    public async Task<PaginatedResultModel<GigCardModel>> Handle(GigsInSubSubCategoryQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        return await _gigScoringService.GetTopScoringGigsByKeywordAsync(request.SubCategoryId, request.Keyword, request.SubSubCategoryId, buyerId, request.PriceRangeId, request.DeliveryTimeId, request.CountryIds, request.LanguageIds, request.SelectedOptionIds, request.CurrentPage, request.ItemsPerPage);
    }
}
