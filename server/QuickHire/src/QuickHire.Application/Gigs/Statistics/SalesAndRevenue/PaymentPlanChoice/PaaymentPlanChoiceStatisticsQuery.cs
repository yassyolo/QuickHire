using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.PaymentPlanChoice;

public record PaaymentPlanChoiceStatisticsQuery(int Id) : IQuery<PieChartDataModel>;

