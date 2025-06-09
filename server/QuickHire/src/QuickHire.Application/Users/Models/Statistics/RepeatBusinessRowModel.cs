namespace QuickHire.Application.Users.Models.Statistics;

public class RepeatBusinessRowModel
{
    public string Date { get; set; } = string.Empty;
    public double ReturningBuyers { get; set; }
    public double AverageRepeatOrders { get; set; }
    public decimal Revenue { get; set; }
}
