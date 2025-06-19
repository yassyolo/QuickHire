using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Orders.Models.Reviews;

namespace QuickHire.Application.Orders.Reviews.RatingDistribution;

public class GetRatingDistributionQueryHandler : IQueryHandler<GetRatingDistributionQuery, RatingDistributionResponseModel>
{
    private readonly IRepository _repository;

    public GetRatingDistributionQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<RatingDistributionResponseModel> Handle(GetRatingDistributionQuery request, CancellationToken cancellationToken)
    {
        var reviewsQueryable = _repository.GetAllReadOnly<QuickHire.Domain.Orders.Review>();
        reviewsQueryable = _repository.GetAllIncluding<QuickHire.Domain.Orders.Review>(x => x.Order.Gig, x => x.Order.Seller);

        if (request.GigId.HasValue)
        {
            reviewsQueryable = reviewsQueryable.Where(x => x.Order.GigId == request.GigId.Value);
        }

        if (request.UserId.HasValue)
        {
            reviewsQueryable = reviewsQueryable.Where(x => x.Order.SellerId == request.UserId.Value);
        }

        var reviewsList = await _repository.ToListAsync<QuickHire.Domain.Orders.Review>(reviewsQueryable);
        
        if (!reviewsList.Any())
        {
            return new RatingDistributionResponseModel
            {
                Total = 0,
                Average = 0,
                Ratings = new List<RatingDistributionModel>()
            };
        }

        var totalReviews = reviewsList.Count();
        var averageRating = reviewsList.Average(x => x.Rating);
        var ratingDistribution = reviewsList
            .GroupBy(x => x.Rating)
            .Select(g => new RatingDistributionModel
            {
                Stars = g.Key,
                Count = g.Count()
            })
            .OrderBy(x => x.Stars)
            .ToList();
        return new RatingDistributionResponseModel
        {
            Total = totalReviews,
            Average = averageRating,
            Ratings = ratingDistribution
        };
    }
}

