using QuickHire.Application.Common.Interfaces.Abstractions;
using QuickHire.Application.Gigs.Models.Statistics;

namespace QuickHire.Application.Gigs.Statistics.SalesAndRevenue.SalesVolume;

public record SalesVolumeStatisticsVolumeQuery(int Id) : IQuery<StatisticsLineChartModel>;

