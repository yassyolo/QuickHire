using Microsoft.EntityFrameworkCore;
using QuickHire.Application.Admin.Models.Shared;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Application.Users.Models.ProjectBriefs;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Users;
using QuickHire.Infrastructure.Persistence.Identity;

namespace QuickHire.Infrastructure.Services;

public class GigScoringService : IGigScoringService
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GigScoringService(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<GigScoreModel>> GetTopScoringGigsAsync(int buyerId, string aboutBuyer, string description, int subSubCategoryId, decimal budget, int deliveryDays)
    {
        var inputKeywords = (aboutBuyer + " " + description).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLowerInvariant()).Distinct().ToList();

        var gigsWithScoresQuery = _repository.GetAllIncluding<Gig>(x => x.PaymentPlans, x => x.Tags, x => x.Seller.SoldOrders)
            .Where(x => x.ModerationStatus != Domain.Moderation.Enums.ModerationStatus.Deactivated && x.SellerId != buyerId && x.SubSubCategoryId == subSubCategoryId)
            .Select(x => new
            {
                Gig = x,
                Seller = x.Seller,
                TitleLower = x.Title.ToLower(),
                DescriptionLower = x.Description.ToLower(),
                ScoreClicks = x.Clicks > 10 ? Math.Min(15, x.Clicks * 0.1) : 0,
                ScoreBudget = x.PaymentPlans.Any(p => p.Price >= budget * 0.9m && p.Price <= budget * 1.1m) ? 10 : 0,
                ScoreDelivery = x.PaymentPlans.Any(p => p.DeliveryTimeInDays <= deliveryDays) ? 10 : 0,
                Tags = x.Tags.Select(t => t.Name),
                SellerClicks = x.Seller.Clicks
            });

        var gigsData = await gigsWithScoresQuery.ToListAsync();

        var results = new List<GigScoreModel>();

        foreach (var item in gigsData)
        {
            double score = item.ScoreClicks + item.ScoreBudget + item.ScoreDelivery;

            foreach (var keyword in inputKeywords)
            {
                if (item.TitleLower.Contains(keyword))
                {
                    score += 3; 
                }
            }

            foreach (var keyword in inputKeywords)
            {
                if (item.DescriptionLower.Contains(keyword))
                {
                    score += 1; 
                }
            }

            foreach (var tagName in item.Tags)
            {
                if ((aboutBuyer + " " + description).IndexOf(tagName, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    score += 5;
                }
            }

            var allReviews = item.Seller?.SoldOrders.SelectMany(o => o.Reviews).ToList();
            if (allReviews != null && allReviews.Count > 0)
            {
                var avgRating = allReviews.Average(r => r.Rating);
                if (avgRating > 4.5)
                {
                    score += 10;
                }
            }

            if (item.Seller.Skills.Any())
            {
                score += item.Seller.Skills.Count(skill => inputKeywords.Any(keyword => skill.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))) * 2;
            }

            if (item.SellerClicks > 0)
            {
                score += Math.Min(10, item.SellerClicks * 0.1);
            }

            results.Add(new GigScoreModel
            {
                GigId = item.Gig.Id,
                Gig = item.Gig,
                SellerId = item.Seller.Id,
                Score = score
            });
        }

        var distinctGigsBySeller = results.GroupBy(x => x.SellerId).Select(x => x.OrderByDescending(x => x.Score).First()).OrderByDescending(x => x.Score).Take(20).ToList();

        return distinctGigsBySeller;
    }

    public async Task<PaginatedResultModel<GigCardModel>> GetTopScoringGigsByKeywordAsync(int? subCategoryId, string? keyword, int? subSubCategoryId, int buyerId, int? priceRangeId, int? deliveryTimeId, List<int>? countryIds, List<int>? languageIds, List<int>? selectedOptionsIds, int currentPage, int itemsPerPage)
    {
        var gigsQuery = _repository.GetAllIncluding<Gig>(x => x.Tags, x => x.FAQs, x => x.Seller, x => x.PaymentPlans, x => x.SubSubCategory.SubCategory)
            .OrderByDescending(x => x.Clicks).Where(x => x.ModerationStatus != Domain.Moderation.Enums.ModerationStatus.Deactivated);

        if(subCategoryId.HasValue)
        {
            gigsQuery = gigsQuery.Where(x => x.SubSubCategory.SubCategoryId == subCategoryId.Value);
        }

        if (subSubCategoryId.HasValue)
        {
            gigsQuery = gigsQuery.Where(x => x.SubSubCategoryId == subSubCategoryId.Value);
        }

        if (countryIds != null && countryIds.Any())
        {
            var sellers = _repository.GetAllReadOnly<Seller>();
            var addresses = _repository.GetAllReadOnly<Address>();

            gigsQuery = gigsQuery
                .Join(sellers, gig => gig.SellerId, seller => seller.Id, (gig, seller) => new { gig, seller.UserId })
                .Join(addresses, gs => gs.UserId, address => address.UserId, (gs, address) => new { gs.gig, address })
                .Where(x => countryIds.Contains(x.address.CountryId))
                .Select(x => x.gig);
        }

        if (languageIds != null && languageIds.Any())
        {
            var sellers = _repository.GetAllReadOnly<Seller>();
            var users = _repository.GetAllReadOnly<ApplicationUser>();

            gigsQuery = gigsQuery
                .Join(sellers, gig => gig.SellerId, seller => seller.Id, (gig, seller) => new { gig, seller.UserId })
                .Join(users, gs => gs.UserId, user => user.Id, (gs, user) => new { gs.gig, user.Languages })
                .Where(x => x.Languages.Any(lang => languageIds.Contains(lang.LanguageId)))
                .Select(x => x.gig);
        }

        var gigMetadataQuery = _repository.GetAllReadOnly<GigMetadata>();

        if (selectedOptionsIds != null && selectedOptionsIds.Any())
        {
            var selectedGigs = gigMetadataQuery
                .Where(x => x.FilterOption.GigFilter.Type == Domain.Categories.Enums.GigFilterType.ServiceIncludes
                            && selectedOptionsIds.Contains(x.FilterOptionId))
                .Select(x => x.GigId).Distinct().ToList();

            gigsQuery = gigsQuery.Where(x => selectedGigs.Contains(x.Id));
        }


        if (priceRangeId.HasValue)
        {
            var priceRangeGigs = gigMetadataQuery.Where(x => x.FilterOptionId == priceRangeId.Value).Select(x => x.GigId).Distinct().ToList();

            gigsQuery = gigsQuery.Where(x => priceRangeGigs.Contains(x.Id));
        }

        if (deliveryTimeId.HasValue)
        {
            var deliveryTimeGigs = gigMetadataQuery.Where(x => x.FilterOptionId == deliveryTimeId.Value).Select(x => x.GigId).Distinct().ToList();

            gigsQuery = gigsQuery.Where(x => deliveryTimeGigs.Contains(x.Id));
        }

        var allGigs = await gigsQuery.ToListAsync();
        var pagedGigs = new List<Gig>();

        if (string.IsNullOrWhiteSpace(keyword))
        {
            var totalCount = allGigs.Count;

            pagedGigs = allGigs.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();

            return await MapToPaginatedGigCardModelsAsync(pagedGigs, buyerId, totalCount, currentPage, itemsPerPage);
        }

        var keywords = keyword.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Distinct().ToList();

        var results = new List<GigScoreModel>();

        foreach (var gig in allGigs)
        {
            double score = 0;
            var titleLower = gig.Title.ToLowerInvariant();
            var descriptionLower = gig.Description.ToLowerInvariant();
            var tagsLower = gig.Tags.Select(x => x.Name.ToLowerInvariant()).ToList();

            foreach (var kw in keywords)
            {
                if (titleLower.Contains(kw)) score += 5;
                if (descriptionLower.Contains(kw)) score += 3;
                if (tagsLower.Any(tag => tag.Contains(kw))) score += 4;
            }

            if (score > 0)
            {
                results.Add(new GigScoreModel
                {
                    GigId = gig.Id,
                    Gig = gig,
                    SellerId = gig.SellerId,
                    Score = score
                });
            }
        }

        var orderedResults = results.OrderByDescending(x => x.Score).ToList();

        var totalKeywordCount = orderedResults.Count;

        var pagedResults = orderedResults.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage).ToList();

        pagedGigs = pagedResults.Select(x => x.Gig).ToList();

        return await MapToPaginatedGigCardModelsAsync(pagedGigs, buyerId, totalKeywordCount, currentPage, itemsPerPage);
    }

    private async Task<PaginatedResultModel<GigCardModel>> MapToPaginatedGigCardModelsAsync(List<Gig> gigs, int? buyerId, int totalCount, int currentPage, int itemsPerPage)
    {
        var favouriteList = await _repository.GetAllReadOnly<FavouriteGigsList>().Where(x => x.BuyerId == buyerId).Include(x => x.FavouriteGigs).FirstOrDefaultAsync();

        var gigCardModels = new List<GigCardModel>();

        foreach (var gig in gigs)
        {
            var seller = await _userService.GetSellerDetailsForGigCardByIdAsync(gig.SellerId);
            var gigReviews = gig.Orders.SelectMany(x => x.Reviews).ToList();

            gigCardModels.Add(new GigCardModel
            {
                Id = gig.Id,
                Title = gig.Title,
                FromPrice = gig.PaymentPlans.Any() ? gig.PaymentPlans.Min(x => x.Price) : 0,
                ImageUrls = gig.ImageUrls,
                SellerName = seller.name ?? "",
                SellerId = gig.SellerId,
                SellerProfileImageUrl = seller.profileImageUrl,
                TopRatedSeller = seller.topRated,
                ReviewsCount = gigReviews.Count,
                AverageRating = gigReviews.Any() ? gigReviews.Average(x => x.Rating) : 0,
                Liked = favouriteList?.FavouriteGigs.Any(x => x.GigId == gig.Id) ?? false
            });
        }

        return new PaginatedResultModel<GigCardModel>
        {
            Data = gigCardModels,
            TotalPages = (int)Math.Ceiling(totalCount / (double)itemsPerPage)
        };
    }



}
