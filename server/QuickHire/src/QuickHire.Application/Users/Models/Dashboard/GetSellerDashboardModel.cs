namespace QuickHire.Application.Users.Models.Dashboard;

public class GetSellerDashboardModel
{
    public string ProfilePictureUrl { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Username { get; set; } = null!;
    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public int TotalOrders { get; set; }
    public string ThisMonth { get; set; } = null!;
    public string EarningsThisMonth { get; set; } = null!;
}
