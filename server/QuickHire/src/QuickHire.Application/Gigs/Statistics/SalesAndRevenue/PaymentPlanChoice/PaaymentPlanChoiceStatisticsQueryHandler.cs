using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Common.Interfaces.Repository;
using QuickHire.Application.Gigs.Models.Statistics;
using QuickHire.Domain.Gigs;
using QuickHire.Domain.Orders;
using QuickHire.Domain.Shared.Exceptions;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.PaymentPlanChoice;

public class PaaymentPlanChoiceStatisticsQueryHandler : IQueryHandler<PaaymentPlanChoiceStatisticsQuery, PieChartDataModel>
{
    private readonly IRepository _repository;

    public PaaymentPlanChoiceStatisticsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PieChartDataModel> Handle(PaaymentPlanChoiceStatisticsQuery request, CancellationToken cancellationToken)
    {
        var gig = await _repository.GetByIdAsync<Gig, int>(request.Id);
        if (gig == null)
        {
            throw new NotFoundException(nameof(Gig), request.Id);
        }

        var orders = _repository.GetAllReadOnly<Order>().Where(x => x.GigId == request.Id);
        orders = _repository.GetAllIncluding<Order>(x => x.SelectedPaymentPlan, x => x.Reviews);
        var ordersList = await _repository.ToListAsync(orders);
        var groupedData = ordersList.GroupBy(x => x.SelectedPaymentPlan.Id)
    .Select(x => new PieChartDataPointModel
    {
        Label = x.First().SelectedPaymentPlan.Name,
        Value = x.Count().ToString(),
        Percentage = ((double)x.Count() / ordersList.Count() * 100).ToString("F0")
    }).ToList();

        return new PieChartDataModel
        {
            Data = groupedData
        };
    }
}
