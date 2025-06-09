namespace QuickHire.Application.Users.Models.Dashboard;

public class SellerDashboardOrderModel
{
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string DueIn { get; set; } = null!;
    public string Price { get; set; } = null!;
    public int Id { get; set; }
}
