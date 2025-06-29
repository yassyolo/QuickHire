using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Common.Interfaces.Services;
using QuickHire.Application.Orders.Models.Reviews;
using QuickHire.Application.Orders.Reviews.GetRatings;

namespace QuickHire.Application.Orders.Reviews.GetReview;

public class GetReviewsQueryHandler : IQueryHandler<GetReviewsQuery, List<ReviewResponseRowModel>>
{
    private readonly IRepository _repository;
    private readonly IUserService _userService;

    public GetReviewsQueryHandler(IRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<List<ReviewResponseRowModel>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
{
    var reviewsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Review>(x => x.Order.Buyer, x => x.Order.Seller, x => x.Order.Gig, x => x.Order.SelectedPaymentPlan);

    if (request.UserId.HasValue)
    {
        reviewsQueryable = reviewsQueryable.Where(x => x.Order.SellerId == request.UserId.Value);
    }

    if (request.GigId.HasValue)
    {
        reviewsQueryable = reviewsQueryable.Where(x => x.Order.GigId == request.GigId.Value);
    }

    if (string.IsNullOrEmpty(request.SortBy) == false && request.SortBy == "Price")
    {
        reviewsQueryable = reviewsQueryable.OrderBy(x => x.Order.TotalPrice);
    }
    else if (string.IsNullOrEmpty(request.SortBy) == false && request.SortBy == "Duration")
    {
        reviewsQueryable = reviewsQueryable.OrderBy(x => x.Order.SelectedPaymentPlan.DeliveryTimeInDays);
    }
    else
    {
        reviewsQueryable = reviewsQueryable.OrderByDescending(x => x.CreatedOn);
    }

    if(request.ShowMore == false)
    {
        reviewsQueryable = reviewsQueryable.Take(5);
    }

        var reviewsList = await _repository.ToListAsync<QuickHire.Domain.Orders.Review>(reviewsQueryable);

        var reviewModels = new List<ReviewResponseRowModel>();

        foreach (var x in reviewsList)
        {
            var userDetails = await _userService.GetBuyerReviewDetailsAsync(x.Order.BuyerId, x.Order.SellerId);

            reviewModels.Add(new ReviewResponseRowModel
            {
                FullName = userDetails.fullName,
                Date = x.CreatedOn.ToString("dd MMMM yyyy"),
                Rating = x.Rating,
                ProfileImageUrl = userDetails.profileImageUrl,
                Comment = x.Comment,
                Duration = x.Order.SelectedPaymentPlan.DeliveryTimeInDays.ToString(),
                Price = x.Order.TotalPrice.ToString("C"),
                CountryName = userDetails.countryName,
                RepeatBuyer = userDetails.repeatBuyer
            });
        }

        return reviewModels;
    }
   
}
