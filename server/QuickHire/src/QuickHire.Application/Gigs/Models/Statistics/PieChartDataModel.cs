namespace QuickHire.Application.Gigs.Models.Statistics;

public class PieChartDataModel
{
    public IEnumerable<PieChartDataPointModel> Data { get; set; } = new List<PieChartDataPointModel>();
}
