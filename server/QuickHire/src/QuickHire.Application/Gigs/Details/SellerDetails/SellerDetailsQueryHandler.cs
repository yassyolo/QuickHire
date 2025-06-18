using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Gigs.Models.Details;
using QuickHire.Application.Orders.Models.Reviews;
using QuickHire.Application.Users.Models.Profile;
using QuickHire.Application.Users.Models.SellerDetails;
using QuickHire.Domain.Shared.Exceptions;
using QuickHire.Domain.Users;

namespace QuickHire.Application.Gigs.Details.SellerDetails;

public class SellerDetailsQueryHandler : IQueryHandler<SellerDetailsQuery, GigSellerDetailsModel>
{
    private readonly IUserService _userService;
    private readonly IRepository _repository;

    public SellerDetailsQueryHandler(IUserService userService, IRepository repository)
    {
        _userService = userService;
        _repository = repository;
    }

    public async Task<GigSellerDetailsModel> Handle(SellerDetailsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Domain.Gigs.Gig, int>(request.Id);
        if( gig == null)
        {
            throw new NotFoundException(nameof(Domain.Gigs.Gig), request.Id) ;
        }

        var ordersQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Order>().Where(x => x.SellerId == gig.SellerId);
        ordersQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Order>(x => x.Reviews);
        var ordersList = await _repository.ToListAsync<QuickHire.Domain.Orders.Order>(ordersQueryable);

        var averageRating = ordersList.Any() ? ordersList.Average(x => x.Reviews.Any() ? x.Reviews.Average(r => r.Rating) : 0) : 0;
        var topRated = ordersList.Any() ? ordersList.All(x => x.Reviews.Any() && x.Reviews.Average(r => r.Rating) >= 4.5) : false;
        var totalReviews = ordersList.Sum(x => x.Reviews.Count());
        var portfoliosQueryable = _repository.GetAllReadOnly<Domain.Users.Portfolio>().Where(x => x.SellerId == gig.SellerId && !x.IsDeleted);

        var portfolios = await _repository.ToListAsync<Domain.Users.Portfolio>(portfoliosQueryable);
        var portfoliomodels = portfolios
            .Select(x => new PortfolioModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                MainCategoryId = x.MainCategoryId,
                ImageUrl = x.ImageUrl
            }).ToList();
        var userId = await _userService.GetUserIdBySellerIdAsync(gig.SellerId);
        var sellerDetailsForBuyer = await _userService.GetSellerDetailsForBuyer(gig.SellerId);
        var sellerUser = await _userService.GetSellerProfileDetails(userId);

        var userLanguagesQueryable = _repository.GetAllIncluding<Domain.Users.UserLanguage>(x => x.Language).Where(x => x.UserId == userId);
        var userLanguages = await _repository.ToListAsync<Domain.Users.UserLanguage>(userLanguagesQueryable);
        var languages = userLanguages.Select(x => x.Language.Name).ToList();

        var reviewsList = ordersList.SelectMany(x => x.Reviews).ToList();

        var reviewModels = await Task.WhenAll(reviewsList.Select(async x =>
        {
            var userDetails = await _userService.GetBuyerReviewDetailsAsync(x.Order.BuyerId, x.Order.SellerId);
            return new ReviewsForUserModel
            {
                FullName = userDetails.fullName,
                Date = x.CreatedOn.ToString("dd MMMM yyyy"),
                Rating = x.Rating,
                ProfileImageUrl = userDetails.profileImageUrl,
                Comment = x.Comment,              
            };
        }));

        return new GigSellerDetailsModel
        {
            UserId = userId,
            ProfileImageUrl = sellerUser.ProfilePictureUrl,
            FullName = sellerUser.FullName,
            Location = sellerUser.Country,
            Description = sellerUser.Description,
            Languages = languages,
            Portfolios = portfoliomodels,
            Rating = averageRating,
            TopRated = topRated,
            Industry = sellerDetailsForBuyer.Industry,
            MemberSince = sellerDetailsForBuyer.MemberSince,
            TotoalReviews = totalReviews,
            Id = gig.SellerId,
            LastDelivery = ordersList.Any() ? ordersList.Max(x => x.CreatedAt).ToString("yyyy-MM-dd") : "N/A",
            Reviews = reviewModels.ToList(),
        }; 
        
    }
}
