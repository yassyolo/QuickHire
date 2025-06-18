using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Gigs.Statistics.OrderFulfillment.Revisions;

public class RevisionsStatisticsQueryHandler : IQueryHandler<RevisionsStatisticsQuery, PieChartDataModel>
{
    private readonly IRepository _repository;

    public RevisionsStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<PieChartDataModel> Handle(RevisionsStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var orders = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);
        orders = _repository.GetAllIncluding<Order>(x => x.Revisions);
        var groupedData = orders
    .SelectMany(o => o.Revisions)
    .GroupBy(x => x.IsAccepted)
    .Select(x => new PieChartDataPointModel
    {
        Label = x.Key ? "Accepted" : "Not Accepted",
        Value = x.Count().ToString(),
        Percentage = ((double)x.Count() / orders.SelectMany(o => o.Revisions).Count() * 100).ToString("F0")
    })
    .ToList();

        if (!groupedData.Any())
        {
            return new PieChartDataModel
            {
                Data = new List<PieChartDataPointModel>
        {
            new PieChartDataPointModel
            {
                Label = "No Revisions",
                Value = "0",
                Percentage = "0"
            }
        }
            };
        }

        return new PieChartDataModel
        {
            Data = groupedData
        };
    }
}

