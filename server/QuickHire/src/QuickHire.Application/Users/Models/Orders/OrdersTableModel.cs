namespace QuickHire.Application.Users.Models.Orders;
public class OrdersTableModel
{
    public int Id { get; set; }
    public string BuyerUsername { get; set; } = string.Empty;
    public string GigTitle { get; set; } = string.Empty;
    public string DueOn { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
}
