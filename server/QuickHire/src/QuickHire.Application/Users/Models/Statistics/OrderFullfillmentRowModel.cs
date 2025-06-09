namespace QuickHire.Application.Users.Models.Statistics;

public class OrderFullfillmentRowModel
{
    public string Date { get; set; } = null!;
    public int NewOrders { get; set; }
    public int CompletedOrders { get; set; }
    public int Sales { get; set; }
    public double AverageRating { get; set; }
    public decimal Revenue { get; set; }
}
