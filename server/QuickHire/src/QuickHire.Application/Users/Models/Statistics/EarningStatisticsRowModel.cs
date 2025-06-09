namespace QuickHire.Application.Users.Models.Statistics;
public class EarningStatisticsRowModel
{
    public string Date { get; set; } = null!;
    public decimal TotalRevenue { get; set; }
    public decimal CompletedRevenue { get; set; }
    public decimal AverageOrderValue { get; set; }
    public decimal InProgressRevenue { get; set; }
}
