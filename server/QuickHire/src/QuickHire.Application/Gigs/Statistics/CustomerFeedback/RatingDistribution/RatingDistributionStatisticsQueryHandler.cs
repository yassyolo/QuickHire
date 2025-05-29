using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.RatingDistribution;

public class RatingDistributionStatisticsQueryHandler : IQueryHandler<RatingDistributionStatisticsQuery, PieChartDataModel>
{
    private readonly IRepository _repository;

    public RatingDistributionStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PieChartDataModel> Handle(RatingDistributionStatisticsQuery request, CancellationToken cancellationToken)
    {
        /*var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var reviews = _repository.GetAllReadOnly<Review>().Where(x => x.Order.GigId == request.Id);
        var reviewsList = await _repository.ToListAsync(reviews);

        if (reviewsList.Count() == 0)
        {
            return new PieChartDataModel
            {
                Data = new List<PieChartDataPointModel>
                {
                    new PieChartDataPointModel { Label = "1 *", Value = "0", Percentage = "0" },
                    new PieChartDataPointModel { Label = "2 *", Value = "0", Percentage = "0" },
                    new PieChartDataPointModel { Label = "3 *", Value = "0", Percentage = "0" },
                    new PieChartDataPointModel { Label = "4 *", Value = "0", Percentage = "0" },
                    new PieChartDataPointModel { Label = "5 *", Value = "0", Percentage = "0" }
                }
            };
        }

        int totalReviews = reviewsList.Count();
        var oneStarCount = reviewsList.Count(x => x.Rating == 1);
        var twoStarCount = reviewsList.Count(x => x.Rating == 2);
        var threeStarCount = reviewsList.Count(x => x.Rating == 3);
        var fourStarCount = reviewsList.Count(x => x.Rating == 4);
        var fiveStarCount = reviewsList.Count(x => x.Rating == 5);

        var oneStarPercentage = (oneStarCount * 100 / totalReviews).ToString();
        var twoStarPercentage = (twoStarCount * 100 / totalReviews).ToString();
        var threeStarPercentage = (threeStarCount * 100 / totalReviews).ToString();
        var fourStarPercentage = (fourStarCount * 100 / totalReviews).ToString();
        var fiveStarPercentage = (fiveStarCount * 100 / totalReviews).ToString();

        /*return new PieChartDataModel
        {
            Data = new List<PieChartDataPointModel>
            {
                new PieChartDataPointModel { Label = "1 *", Value = oneStarCount.ToString(), Percentage = oneStarPercentage },
                new PieChartDataPointModel { Label = "2 *", Value = twoStarCount.ToString(), Percentage = twoStarPercentage },
                new PieChartDataPointModel { Label = "3 *", Value = threeStarCount.ToString(), Percentage = threeStarPercentage },
                new PieChartDataPointModel { Label = "4 *", Value = fourStarCount.ToString(), Percentage = fourStarPercentage },
                new PieChartDataPointModel { Label = "5 *", Value = fiveStarCount.ToString(), Percentage = fiveStarPercentage }
            }
        };*/

        return new PieChartDataModel
        {
            Data = new List<PieChartDataPointModel>
            {
                new PieChartDataPointModel { Label = "1 *", Value = "3", Percentage = "10" },
                new PieChartDataPointModel { Label = "2 *", Value = "3", Percentage = "10" },
                new PieChartDataPointModel { Label = "3 *", Value = "3", Percentage = "10" },
                new PieChartDataPointModel { Label = "4 *", Value = "3", Percentage = "10" },
                new PieChartDataPointModel { Label = "5 *", Value = "24", Percentage = "60" }
            }
        };
    }
}

