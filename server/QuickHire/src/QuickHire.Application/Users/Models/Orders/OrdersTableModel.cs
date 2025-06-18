namespace QuickHire.Application.Users.Models.Orders;
public class OrdersTableModel
{
    public int Id { get; set; }
    public string? BuyerUsername { get; set; }
    public string? SellerUsername { get; set; }
    public string GigTitle { get; set; } = string.Empty;
    public string DueOn { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
}
