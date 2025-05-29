namespace QuickHire.Application.Gigs.Models.Statistics;

public class StatisticsLineChartModel
{
    public TotalItemModel? TotalItem { get; set; } 
    public ThisMonthModel? ThisMonthItem { get; set; }
    public PeakModel? PeakItem { get; set; }
    public IEnumerable<LineChartDataPointModel> Data { get; set; } = new List<LineChartDataPointModel>();
}

