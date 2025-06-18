using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;
using System.Linq;

namespace QuickHire.Application.Gigs.Statistics.CustomerFeedback.ReviewResponseRate;

public class ReviewResponseRateStatisticsQueryHandler : IQueryHandler<ReviewResponseRateStatisticsQuery, PieChartDataModel>
{
    private readonly IRepository _repository;

    public ReviewResponseRateStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PieChartDataModel> Handle(ReviewResponseRateStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var orders = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);
        var ordersList = await _repository.ToListAsync(orders);

        int totalOrders = ordersList.Count();

        if (totalOrders == 0)
        {
            return new PieChartDataModel
            {
                Data = new List<PieChartDataPointModel>
            {
                new PieChartDataPointModel { Label = "Yes", Value = "0", Percentage = "0" },
                new PieChartDataPointModel { Label = "No", Value = "0", Percentage = "0" },
            }
            };
        }

        int respondedCount = ordersList.Count(x => x.Reviews.Any());
        int notRespondedCount = totalOrders - respondedCount;

        var yesPercentage = (respondedCount * 100 / totalOrders).ToString();
        var noPercentage = (notRespondedCount * 100 / totalOrders).ToString();

        var pieChartDataModel = new PieChartDataModel
        {
            Data = new List<PieChartDataPointModel>
        {
            new PieChartDataPointModel
            {
                Label = "Yes",
                Value = respondedCount.ToString(),
                Percentage = yesPercentage
            },
            new PieChartDataPointModel
            {
                Label = "No",
                Value = notRespondedCount.ToString(),
                Percentage = noPercentage
            }
        }
        };

        return pieChartDataModel;
    }

}

