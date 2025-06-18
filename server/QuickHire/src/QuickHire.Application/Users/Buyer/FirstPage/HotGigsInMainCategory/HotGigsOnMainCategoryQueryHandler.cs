using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Shared;
using QuickHire.Application.Users.Models.Gigs;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Users.Buyer.FirstPage.HotGigsInMainCategory;

public class HotGigsOnMainCategoryQueryHandler : IQueryHandler<HotGigsOnMainCategoryQuery, HotGigsModel>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public HotGigsOnMainCategoryQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }
    public async Task<HotGigsModel> Handle(HotGigsOnMainCategoryQuery request, CancellationToken cancellationToken)
    {
        var buyerId = await _userService.GetBuyerIdByUserIdAsync();
        var browsingHistoryQueryable = _repository.GetAllIncluding<QuickHire.Domain.Users.BrowsingHistory>(x => x.Gig).Where(x => x.BuyerId == buyerId);
        var browsingHistoryList = await _repository.ToListAsync(browsingHistoryQueryable);
        var mainCategoryId = 0;

        if (browsingHistoryList.Any())
        {
            var subSubCategoryId = browsingHistoryList.OrderByDescending(x => x.ViewedAt).Select(x => x.Gig.SubSubCategoryId).FirstOrDefault();
            var subCategoryQueryable = _repository.GetAllIncluding<Domain.Categories.SubSubCategory>(x => x.SubCategory).Where(x => x.Id == subSubCategoryId);
            var subCategory = await _repository.FirstOrDefaultAsync(subCategoryQueryable);
            if (subCategory != null)
            {
                mainCategoryId = subCategory.SubCategory.MainCategoryId;
            }
        }
        else
        {
            var mainCategoryQueryable = _repository.GetAllIncluding<Domain.Categories.MainCategory>();
            var mainCategoriesList = await _repository.ToListAsync(mainCategoryQueryable);
            mainCategoryId = mainCategoriesList.OrderByDescending(x => x.Clicks).Select(x => x.Id).FirstOrDefault();
        }

        var favouriteGigsQueryable = _repository.GetAllReadOnly<FavouriteGig>().Where(x => x.BuyerId == buyerId);
        var favouriteGigsIdsList = await _repository.ToListAsync(favouriteGigsQueryable);

        var gigsQueryable = _repository.GetAllIncluding<Domain.Gigs.Gig>(x => x.Seller).Where(x => x.SubSubCategory.SubCategory.MainCategoryId == mainCategoryId && x.ModerationStatus != Domain.Moderation.Enums.ModerationStatus.PendingReview);
        var gigsList = await _repository.ToListAsync(gigsQueryable);
        gigsList = gigsList.OrderByDescending(x => x.Clicks).Take(10).ToList();
        var result = new List<GigCardModel>();

        foreach (var bh in gigsList)
        {
            var gig = bh;
            var gigSellerDetails = await _userService.GetSellerDetailsForGigCardByIdAsync(gig.SellerId);
            var gigReviews = gig.Orders.Select(x => x.Reviews).SelectMany(x => x).ToList();

            var gigCardModel = new GigCardModel
            {
                Id = gig.Id,
                Title = gig.Title,
                FromPrice = gig.PaymentPlans.Any() ? gig.PaymentPlans.Min(x => x.Price) : 0,
                ImageUrls = gig.ImageUrls,
                SellerName = gigSellerDetails.name,
                SellerId = gig.SellerId,
                SellerProfileImageUrl = gigSellerDetails.profileImageUrl,
                TopRatedSeller = gigSellerDetails.topRated,
                ReviewsCount = gigReviews.Count(),
                AverageRating = gigReviews.Any() ? gigReviews.Average(x => x.Rating) : 0,
                Liked = favouriteGigsIdsList.Any(x => x.GigId == gig.Id)
            };

            result.Add(gigCardModel);
        }
        var mainCategoryName = string.Empty;
        if (mainCategoryId > 0)
        {
            var mainCategory = await _repository.GetByIdAsync<Domain.Categories.MainCategory, int>(mainCategoryId);
            mainCategoryName = mainCategory?.Name ?? string.Empty;
        }

        return new HotGigsModel
        {
            Gigs = result,
            MainCategory = mainCategoryName
        };
    }
}

